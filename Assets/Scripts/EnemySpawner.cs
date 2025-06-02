using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The enemy prefab to spawn.")]
    public GameObject enemyPrefab;

    [Tooltip("How often (in seconds) a new enemy spawns.")]
    public float spawnInterval = 1.5f;

    [Tooltip("Distance from the player at which enemies appear.")]
    public float spawnRadius = 8f;

    private Transform playerTransform;

    void Start()
    {
        // Find the player by tag at startup
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            playerTransform = playerGO.transform;
        else
            Debug.LogWarning("EnemySpawner: No GameObject tagged ‘Player’ found in the scene.");

        // Begin invoking SpawnEnemy() after 1 second, then every spawnInterval seconds
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (playerTransform == null)
            return; // no player to orbit around

        // Pick a random angle in radians from 0 to 2π
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Compute position on circle around the player
        Vector2 spawnPos = new Vector2(
            playerTransform.position.x + Mathf.Cos(angle) * spawnRadius,
            playerTransform.position.y + Mathf.Sin(angle) * spawnRadius
        );

        // Instantiate the enemy prefab at that world position
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
