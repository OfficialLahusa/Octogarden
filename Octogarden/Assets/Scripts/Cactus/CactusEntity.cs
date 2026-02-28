using TMPro;
using UnityEngine;

public class CactusEntity : MonoBehaviour
{
    [SerializeField]
    TMP_Text hpText;

    CactusData entityData;
    
    public uint columnIndex = 0;
    public uint rowIndex = 0;

    void Awake()
    {
        if(PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] != null)
        {
            entityData = PlayerInventory.Instance.placedCacti[columnIndex, rowIndex];
        }   
        else
        {
            entityData = CactusFactory.CreateCactus();
            PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] = entityData;
        }
    }

    void Update()
    {
        if (hpText != null)
            hpText.text = $"{entityData.CurrentHealth}/{entityData.MaxHealth}";
    }
}
