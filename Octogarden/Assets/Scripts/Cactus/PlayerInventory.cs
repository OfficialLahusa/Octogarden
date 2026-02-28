#nullable enable
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory? _instance;
    public static PlayerInventory Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject().AddComponent<PlayerInventory>();
                _instance.name = _instance.GetType().Name;
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public static void Reset()
    {
        if (_instance)
        {
            Destroy(_instance.gameObject);
            _instance = null;
        }
    }

    private PlayerInventory() { }

    public static readonly uint GRID_COLUMNS = 4;
    public static readonly uint GRID_ROWS = 5;

    public CactusData[,] placedCacti = new CactusData[GRID_COLUMNS, GRID_ROWS];
    public int Seaweed { get; private set; } = 250;
}
