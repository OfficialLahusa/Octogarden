using UnityEngine;

[CreateAssetMenu(fileName = "SpawnEntry", menuName = "Scriptable Objects/SpawnEntry")]
public class SpawnEntry : ScriptableObject
{
    public GameObject prefab;
    public float weight = 1f;
}