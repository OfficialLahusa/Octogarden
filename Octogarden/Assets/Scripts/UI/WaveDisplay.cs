using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    private TMP_Text _text;
    private WaveSpawner _waveSpawner;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _waveSpawner = FindFirstObjectByType<WaveSpawner>();
    }

    void FixedUpdate()
    {
        _text.text = $"Wave {_waveSpawner.WaveIndex + 1} / {_waveSpawner.spawnWaves.Length}";
    }
}
