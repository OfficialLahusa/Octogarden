using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    DialogueScreen dialogueScreen;

    [SerializeField]
    GameObject[] spawnPoints;

    [SerializeField]
    GameObject nextWaveButton;

    [SerializeField]
    GameObject dayBackground;
    [SerializeField]
    GameObject nightBackground;

    // Waves
    public SpawnWave[] spawnWaves;

    public bool IsCompleted { get { return WaveIndex >= spawnWaves.Length; } }
    public bool IsWaveActive { get; private set; } = false;

    private WeightedRandom<GameObject> _weightedEnemyPicker;
    public int WaveIndex { get; private set; } = -1;
    private uint _remainingKills = 0;
    private uint _remainingSpawns = 0;

    private float _spawnInterval = 1.35f;
    private float _spawnTimer = 0f;
    //private float _runtime = 0f;

    private void Awake()
    {
        dayBackground.SetActive(true);
        nightBackground.SetActive(false);
    }

    public void SetWave(int idx)
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

        dayBackground.SetActive(false);
        nightBackground.SetActive(true);

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
        nextWaveButton.SetActive(!IsWaveActive);

        if (IsCompleted || !IsWaveActive)
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

    public void NextWaveButtonPressed()
    {
        if (!IsWaveActive)
        {
            IsWaveActive = true;
            NextWave();
        }
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
            IsWaveActive = false;
            PlayerInventory.Instance.HealAllCacti();
            dayBackground.SetActive(true);
            nightBackground.SetActive(false);
        }
    }

    private void SpawnEnemy()
    {
        int laneIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(_weightedEnemyPicker.Draw(), spawnPoints[laneIndex].transform.position, Quaternion.identity);
    }
}
