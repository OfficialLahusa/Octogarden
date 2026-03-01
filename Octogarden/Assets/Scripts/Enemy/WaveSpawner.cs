using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    DialogueScreen dialogueScreen;

    [SerializeField]
    GameObject[] spawnPoints;

    // Waves
    public SpawnWave[] spawnWaves;

    public bool IsCompleted { get { return WaveIndex >= spawnWaves.Length; } }

    private WeightedRandom<GameObject> _weightedEnemyPicker;
    public uint WaveIndex { get; private set; } = 0;
    private uint _remainingKills = 0;
    private uint _remainingSpawns = 0;

    private float _spawnInterval = 1.35f;
    private float _spawnTimer = 0f;
    //private float _runtime = 0f;

    private void Awake()
    {
        SetWave(0);
    }

    public void SetWave(uint idx)
    {
        _weightedEnemyPicker = new WeightedRandom<GameObject>();
        foreach (SpawnEntry entry in spawnWaves[idx].spawnEntries)
        {
            _weightedEnemyPicker.Add(entry.prefab, entry.weight);
        }

        _remainingKills = spawnWaves[idx].totalCount;
        _remainingSpawns = spawnWaves[idx].totalCount;
        _spawnInterval = spawnWaves[idx].spawnInterval;
        _spawnTimer = 0f;

        PlayerInventory.Instance.HealAllCacti();

        if (spawnWaves[idx].sequenceBefore != null)
        {
            dialogueScreen.SetDialogueSequence(spawnWaves[idx].sequenceBefore);
        }
    }

    public void NextWave()
    {
        WaveIndex++;
        if (IsCompleted)
        {
            Debug.Log("All waves completed!");
            return;
        }
        else
        {
            
            SetWave(WaveIndex);
        }
    }

    void Update()
    {
        if(IsCompleted)
        {
            return;
        }

        if (_spawnTimer <= 0f && _remainingSpawns > 0)
        {
            SpawnEnemy();
            _spawnTimer = _spawnInterval;
            _remainingSpawns--;
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }

        //_spawnInterval = (1.35f*1.5f) / (1 + (_runtime / 60f * 0.1f));
    }

    public void RegisterKill()
    {
        _remainingKills--;
        if (_remainingKills <= 0)
        {
            if (spawnWaves[WaveIndex].sequenceAfter != null)
            {
                dialogueScreen.SetDialogueSequence(spawnWaves[WaveIndex].sequenceAfter);
            }

            // TODO: Don't start immediately, wait for player to click button
            NextWave();
        }
    }

    private void SpawnEnemy()
    {
        int laneIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(_weightedEnemyPicker.Draw(), spawnPoints[laneIndex].transform.position, Quaternion.identity);
    }
}
