using UnityEngine;
using UnityEngine.InputSystem;

public class CactusPlacementSpot : MonoBehaviour
{
    [SerializeField]
    uint columnIndex;
    [SerializeField]
    uint rowIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool wasHovered = false;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                wasHovered = true;
                break;
            }
        }

        if (wasHovered && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] == null)
            {
                // Trigger ShopHandler Event
                ShopHandler shopHandler = FindFirstObjectByType<ShopHandler>();
                shopHandler.OnClickEmptyCactusSpot(columnIndex, rowIndex);
            }
        }
    }
}
