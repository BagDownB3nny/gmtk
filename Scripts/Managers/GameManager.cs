using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int playerScore = 0;
    private bool _isGameOver;
    public bool IsGameOver
    {
        get { return _isGameOver; }
        set
        {
            _isGameOver = value;
            if (_isGameOver)
            {
                Debug.Log("Game Over!");
                Debug.Log("Final game score: " + playerScore);
                Reset();
            }
        }
    }

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

    void Reset()
    {
        playerScore = 0;
        _isGameOver = false;
        Player.instance.SetPlayerAlive(); // Reset player state
        TimeManager.instance.EndLoop();
        ReplayManager.instance.Reset();
        TimeManager.instance.StartLoop();
    }

    public void IncrementScore(int amount)
    {
        playerScore += amount;
        Debug.Log("Score incremented. Current score: " + playerScore);
    }
}
