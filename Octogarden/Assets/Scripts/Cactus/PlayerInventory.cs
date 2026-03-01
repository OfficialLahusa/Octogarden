#nullable enable
using System;
using Unity.VisualScripting;
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

    private PlayerInventory()
    {

    }

    public static readonly uint GRID_COLUMNS = 4;
    public static readonly uint GRID_ROWS = 5;

    public CactusData[,] placedCacti = new CactusData[GRID_COLUMNS, GRID_ROWS];
    public CactusEntity?[,] placedCactusEntities = new CactusEntity[GRID_COLUMNS, GRID_ROWS];
    public uint Seaweed { get; set; } = 0;
    public bool IsInitialized { get; private set; } = false;

    public static void CreateRandomInitialPlacement()
    {
        if(Instance.IsInitialized)
            throw new InvalidOperationException("PlayerInventory is already initialized.");

        for (uint col = 0; col < GRID_COLUMNS; col++)
        {
            for (uint row = 0; row < GRID_ROWS; row++)
            {
                Instance.placedCacti[col, row] = CactusFactory.CreateCactus();
            }
        }

        Instance.IsInitialized = true;
    }

    public static void CreateInitialPlacement()
    {
        if (Instance.IsInitialized)
            throw new InvalidOperationException("PlayerInventory is already initialized.");

        for (uint col = 0; col < 2; col++)
        {
            for (uint row = 0; row < GRID_ROWS; row++)
            {
                CactusData? cactusData = null;
                if (col == 0)
                {
                    cactusData = CactusFactory.CreateCactus(CactusClass.Ranged);
                }
                else
                {
                    cactusData = CactusFactory.CreateCactus(row % 2 == 0 ? CactusClass.Melee : CactusClass.Tank);
                }
                Instance.placedCacti[col, row] = cactusData;
            }
        }

        Instance.IsInitialized = true;
    }

    public void RegisterCactusEntity(CactusEntity entity, uint columnIndex, uint rowIndex)
    {
        if (placedCactusEntities[columnIndex, rowIndex] != null)
        {
            Debug.LogWarning($"Overwriting existing cactus entity at ({columnIndex}, {rowIndex})");
        }
        placedCactusEntities[columnIndex, rowIndex] = entity;
    }

    public void HealAllCacti()
    {
        for (uint col = 0; col < GRID_COLUMNS; col++)
        {
            for (uint row = 0; row < GRID_ROWS; row++)
            {
                CactusData? entityData = placedCacti[col, row];
                if (entityData != null)
                {
                    entityData.CurrentHealth = entityData.MaxHealth;
                }
            }
        }
    }
}
