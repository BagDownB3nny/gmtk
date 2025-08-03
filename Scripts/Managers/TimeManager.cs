using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float startTime;
    [SerializeField] private float loopDuration = 10f; // Example duration for a loop, can be adjusted

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Time.time - startTime >= loopDuration)
        {
            EndLoop();
            StartLoop();
        }
    }
    public void StartLoop()
    {
        print("LOOP START");
        startTime = Time.time;
        ReplayManager.instance.StartReplay();
        Player.instance.TeleportToRandomStart();
    }
    public void EndLoop()
    {
        print("LOOP END");
        ReplayManager.instance.EndReplay();
        RecorderManager.instance.SaveRecord();
        ReplayManager.instance.SavePlayerStartPositions();
    }
}