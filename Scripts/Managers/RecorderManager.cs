using System.Collections.Generic;
using UnityEngine;
public enum PlayerActionType { Move, Aim, Fire }

public struct PlayerAction
{
    public PlayerActionType actionType;
    public Vector2 direction; // only used for Move
    public Vector2 mousePosition; // only used for Aim

    public PlayerAction(PlayerActionType type, Vector2 dir = default, Vector2 mousePos = default)
    {
        actionType = type;
        mousePosition = mousePos;
        direction = dir;
    }
}
public class RecorderManager : MonoBehaviour
{
    public static RecorderManager instance;
    public SortedList<float, List<PlayerAction>> actions = new();
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


    public void RecordMove(Vector2 dir)
    {
        float currTime = Time.time - TimeManager.instance.startTime;
        if (actions.TryGetValue(currTime, out var existingActions))
        {
            existingActions.Add(new PlayerAction(PlayerActionType.Move, dir));
        }
        else
        {
            actions.Add(currTime, new List<PlayerAction> { new(PlayerActionType.Move, dir) });
        }
    }

    public void RecordAim(Vector2 mousePos)
    {
        float currTime = Time.time - TimeManager.instance.startTime;
        if (actions.TryGetValue(currTime, out var existingActions))
        {
            existingActions.Add(new PlayerAction(PlayerActionType.Aim, mousePos: mousePos));
        }
        else
        {
            actions.Add(currTime, new List<PlayerAction> { new(PlayerActionType.Aim, mousePos: mousePos) });
        }
    }

    public void RecordFire()
    {
        float currTime = Time.time - TimeManager.instance.startTime;
        if (actions.TryGetValue(currTime, out var existingActions))
        {
            existingActions.Add(new PlayerAction(PlayerActionType.Fire));
        }
        else
        {
            actions.Add(currTime, new List<PlayerAction> { new(PlayerActionType.Fire) });
        }
    }

    public void SaveRecord()
    {
        SortedList<float, List<PlayerAction>> copiedActions = new SortedList<float, List<PlayerAction>>(actions);
        ReplayManager.instance.PushPlayerActions(copiedActions);
        actions.Clear();
    }
}
