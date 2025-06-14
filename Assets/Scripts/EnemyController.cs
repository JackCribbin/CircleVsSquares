using UnityEngine;
using UnityEngine.UI;  // ← needed for Image type

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    [Tooltip("Movement speed toward the player.")]
    public float speed = 2f;

    [Header("Health Settings")]
    [Tooltip("Max health of this enemy.")]
    public int maxHealth = 100;

    [Header("XP Settings")]
    [Tooltip("How much XP this enemy awards.")]
    public int xpValue = 10;

    // Reference to the fill Image on the health bar
    [Header("UI")]
    [Tooltip("Drag the 'HealthBar_Fill' Image here (child of EnemyHealthBar_Canvas).")]
    public Image healthBarFill;

    private int currentHealth;
    private Transform playerTransform;
    private PlayerController playerController;

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Find the player by tag
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            playerTransform = playerGO.transform;
            playerController = playerGO.GetComponent<PlayerController>();
            if (playerController == null)
                Debug.LogWarning("PlayerController component not found on Player GameObject.");
        }

        // Initialize the health bar to full
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // Move toward the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Read the damage from the bullet
            BulletController bullet = other.GetComponent<BulletController>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            // Destroy the bullet
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            Die();
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Clamp to zero so we never go below 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Update the health bar fill (if assigned)
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Award XP to the player
        if (playerController != null)
        {
            playerController.GainXP(xpValue);
        }
        else
        {
            // Fallback: try finding again at runtime
            PlayerController pc = FindObjectOfType<PlayerController>();
            if (pc != null)
                pc.GainXP(xpValue);
        }

        // (Optional) spawn death VFX here, award points, etc.
        Destroy(gameObject);
    }
}
