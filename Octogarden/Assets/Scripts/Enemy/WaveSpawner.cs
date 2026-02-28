using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;

    // Enemy Prefabs
    [SerializeField]
    GameObject basicEnemy;

    private const float SPAWN_INTERVAL = 0.65f;
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
        Instantiate(basicEnemy, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
