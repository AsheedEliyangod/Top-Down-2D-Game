using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private GameObject enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField]
    private Transform[] spawnPoints;

    [Header("Settings")]
    [SerializeField]
    private int enemyCount = 3;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (enemyPrefab == null)
            return;

        if (spawnPoints.Length == 0)
            return;

        int enemiesToSpawn =
            Mathf.Min(
            enemyCount,
            spawnPoints.Length);

        for (int i = 0;
            i < enemiesToSpawn;
            i++)
        {
            Instantiate(
                enemyPrefab,

                spawnPoints[i].position,

                Quaternion.identity);
        }
    }
}