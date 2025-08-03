using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;
    private bool isDead = false;
    public Vector2 startPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void TeleportToRandomStart()
    {
        float minX = -7f;
        float maxX = 7f;
        float minY = -3f;
        float maxY = 3f;
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        transform.position = randomPosition;
        startPos = randomPosition;
    }
    public void SetPlayerAlive()
    {
        isDead = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Reset color to white
    }

    public void SetPlayerDead()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        GameManager.instance.IsGameOver = true; // Trigger game over logic
        // Additional logic for player death can be added here
    }   
}
