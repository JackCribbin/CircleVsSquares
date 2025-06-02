using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 2f;           // How fast this enemy moves toward the player

    private Transform playerTransform; // Reference to the player’s Transform

    void Start()
    {
        // Find the player GameObject by tag at startup
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            playerTransform = playerGO.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return; // No player found — don’t do anything

        // Compute a normalized direction vector toward the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Move the enemy a bit this frame
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destroy the bullet and this enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            // Enemy reached the player: you could do damage or end the game here.
            Debug.Log("Enemy hit the player!");
            Destroy(gameObject);
        }
    }
}
