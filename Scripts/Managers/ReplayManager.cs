using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance;
    private readonly List<SortedList<float, List<PlayerAction>>> cloneActions = new();
    private readonly List<GameObject> clones = new();
    private readonly List<Vector2> cloneStartPositions = new();
    private readonly List<Coroutine> activeCoroutines = new();
    [SerializeField]
    private GameObject clonePrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartReplay()
    {
        SpawnClones();
        for (int i = 0; i < cloneActions.Count; i++)
        {
            SortedList<float, List<PlayerAction>> actions = cloneActions[i];
            foreach (KeyValuePair<float, List<PlayerAction>> action in actions)
            {
                foreach (PlayerAction pa in action.Value)
                {
                    GameObject clone = clones[i];
                    var coroutine = StartCoroutine(CallFunctionAfterDelay(
                        action.Key,
                        () => ExecuteAction(clone, pa)
                    ));
                    activeCoroutines.Add(coroutine);
                }
            }
        }
    }

    public void SavePlayerStartPositions()
    {
        cloneStartPositions.Add(Player.instance.startPos);
    }

    IEnumerator CallFunctionAfterDelay(float seconds, Action callback)
    {
        // This is the key part. It pauses the coroutine for 1.5 seconds.
        yield return new WaitForSeconds(seconds);

        // After the delay, invoke the function that was passed in.
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public void PushPlayerActions(SortedList<float, List<PlayerAction>> actions)
    {
        cloneActions.Add(actions);
    }

    void ExecuteAction(GameObject clone, PlayerAction action)
    {
        switch (action.actionType)
        {
            case PlayerActionType.Move:
                if (clone.TryGetComponent<CloneMovement>(out var movement))
                {
                    movement.SetMovement(action.direction);
                }
                else
                {
                    Debug.LogWarning("CloneMovement component not found on player " + clone.name);
                }
                break;
            case PlayerActionType.Aim:
                if (clone.TryGetComponent<CloneMovement>(out var aimMovement))
                {
                    Debug.Log("Setting aim for player: " + clone.name + " with mouse position: " + action.mousePosition);
                    aimMovement.SetMousePosition(action.mousePosition);
                }
                else
                {
                    Debug.LogWarning("CloneMovement component not found on player " + clone.name);
                }
                break;
            case PlayerActionType.Fire:
                // Implement firing logic here
                if (clone.TryGetComponent<CloneShooting>(out var shooting))
                {
                    shooting.Shoot();
                }
                else
                {
                    Debug.LogWarning("Shooting component not found on player " + clone.name);
                }
                break;

        }
    }

    private void SpawnClones()
    {
        for (int i = 0; i < cloneActions.Count; i++)
        {
            GameObject clone = Instantiate(clonePrefab);
            clones.Add(clone);
            if (clone.TryGetComponent<CloneMovement>(out var cloneMovement))
            {
                cloneMovement.SetMovement(Vector2.zero);
                cloneMovement.SetMousePosition(Vector2.zero);
            }
            else
            {
                Debug.LogWarning("CloneMovement component not found on cloned player " + i);
            }
        }
    }
    public void EndReplay()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        activeCoroutines.Clear();

        foreach (GameObject player in clones)
        {
            Destroy(player);
        }
        clones.Clear();
    }

    public void Reset()
    {
        EndReplay();
        cloneActions.Clear();
    }
}