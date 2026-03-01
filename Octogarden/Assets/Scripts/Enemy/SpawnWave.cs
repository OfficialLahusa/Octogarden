using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWave", menuName = "Scriptable Objects/SpawnWave")]
public class SpawnWave : ScriptableObject
{
    public DialogueSequence sequenceBefore;
    public DialogueSequence sequenceAfter;

    public float spawnInterval = 1.35f;
    public uint totalCount = 50;

    public SpawnEntry[] spawnEntries;

    public bool isFallout;
}
