using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;

    // Enemy Prefabs
    [SerializeField]
    GameObject[] enemyPrefabs;

    private const float SPAWN_INTERVAL = 1.35f*10;
    private float spawnTimer = 0f;

    void Update()
    {
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = SPAWN_INTERVAL;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
