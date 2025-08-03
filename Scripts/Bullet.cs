using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Handle wall hit logic here
            Destroy(gameObject); // Destroy the bullet
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Clone"))
        {
            // Handle clone hit logic here
            if (collision.gameObject.TryGetComponent<Clone>(out var clone))
            {
                clone.SetCloneDead(); // Mark the clone as dead
                Destroy(gameObject); // Destroy the bullet
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Handle player hit logic here
            if (collision.gameObject.TryGetComponent<Player>(out var player))
            {
                player.SetPlayerDead(); // Mark the player as dead
                Destroy(gameObject); // Destroy the bullet
            }
        }

    }
}
