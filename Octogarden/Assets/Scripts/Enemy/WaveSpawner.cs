using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawnPoints;

    // Enemy Prefabs
    [SerializeField]
    GameObject[] enemyPrefabs;

    private float _spawnInterval = 1.35f*1;
    private float _spawnTimer = 0f;
    private float _runtime = 0f;

    void Update()
    {
        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer = _spawnInterval;
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }

        _spawnInterval = (1.35f*1.5f) / (1 + (_runtime / 60f * 0.1f));
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
