using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CactusEntity : MonoBehaviour
{
    [SerializeField]
    TMP_Text hpText;

    [SerializeField]
    SpriteRenderer flowerRenderer;

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

        // Set flower sprite color based on cactus class
        if (flowerRenderer != null)
        {
            flowerRenderer.color = entityData.FlowerColor.ToColor();
        }
    }

    void Update()
    {
        if (hpText != null)
            hpText.text = $"{entityData.Name}\n{entityData.CurrentHealth}/{entityData.MaxHealth}";

        bool wasHovered = false;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            wasHovered = true;
        }

        // TODO: Show tooltip, etc.
        if (wasHovered)
        {
            TooltipManager.ShowTooltip(entityData);
        }

        if (entityData.FlowerColor.Equals(CactusFlowerColor.Rainbow) && flowerRenderer != null)
        {
            flowerRenderer.color = Color.HSVToRGB((Time.time * 0.1f) % 1f, 1f, 1f);
        }
    }

    public void Damage(uint damageAmount)
    {
        if (damageAmount >= entityData.CurrentHealth)
        {
            entityData.CurrentHealth = 0;
            PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] = null;
            Destroy(gameObject);
        }
        else
        {
            entityData.CurrentHealth -= damageAmount;
        }
    }
}
