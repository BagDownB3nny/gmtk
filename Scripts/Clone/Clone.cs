using UnityEngine;

public class Clone : MonoBehaviour
{
    private bool isDead = false;
    // Update is called once per frame
    public void SetCloneDead()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        float timeToScore = Time.time - TimeManager.instance.startTime;
        GameManager.instance.IncrementScore((int)(10f - timeToScore) * 100);
        // Possible combo score?
    }

    public bool IsDead()
    {
        return isDead;
    }
}
