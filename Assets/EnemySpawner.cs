using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy to spawn
    public Transform spawnPoint; // The position where enemies will spawn
    public float spawnInterval = 3f; // The time interval between enemy spawns
    public int maxEnemies = 5; // Maximum number of enemies to spawn
    private int currentEnemies = 0; // Current number of spawned enemies

    void Start()
    {
        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Check if the maximum number of enemies has been reached
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        // Spawn enemy at the spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        
        // Set the layer of the enemy object
        enemy.layer = LayerMask.NameToLayer("Enemy");

        // Increment the current number of enemies
        currentEnemies++;
    }
}
