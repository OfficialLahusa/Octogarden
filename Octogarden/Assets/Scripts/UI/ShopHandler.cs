using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopHandler : MonoBehaviour
{
    [SerializeField]
    TMP_Text stateText;
    public enum ShopState
    {
        Default,
        PickMoveSource,
        PickMoveTarget,
        PickMutationTarget,
        PickRepotTarget,
        PickCrossBreedSourceOne,
        PickCrossBreedSourceTwo,
        PickCrossBreedTarget,
    }
    public ShopState CurrentShopState { get; private set; } = ShopState.Default;

    private List<Vector2Int> _clickedPositions = new List<Vector2Int>();

    void Awake()
    {
        stateText.text = string.Empty;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CancelShopInteraction();
        }
    }

    private void UpdateStateText()
    {
        stateText.text = CurrentShopState switch
        {
            ShopState.Default => string.Empty,
            ShopState.PickMoveSource => "Select a cactus to move",
            ShopState.PickMoveTarget => "Select a spot to move the cactus to",
            ShopState.PickMutationTarget => "Select a cactus to mutate",
            ShopState.PickRepotTarget => "Select a cactus to repot",
            ShopState.PickCrossBreedSourceOne => "Select the first cactus to crossbreed",
            ShopState.PickCrossBreedSourceTwo => "Select the second cactus to crossbreed",
            ShopState.PickCrossBreedTarget => "Select a spot to plant the new cactus",
            _ => ""
        };
    }

    public void CancelShopInteraction()
    {
        CurrentShopState = CurrentShopState switch
        {
            _ => ShopState.Default
        };

        _clickedPositions.Clear();
        UpdateStateText();
    }

    public void OnClickExistingCactus(uint column, uint row)
    {
        if (CurrentShopState == ShopState.PickMoveTarget)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
            PerformMove();
        }
        else if (CurrentShopState == ShopState.PickMutationTarget)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
            PerformMutation();
        }
        else if (CurrentShopState == ShopState.PickRepotTarget)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
            PerformRepot();
        }
        else if (CurrentShopState == ShopState.PickCrossBreedSourceOne)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
        }
        else if (CurrentShopState == ShopState.PickCrossBreedSourceTwo)
        {
            if (_clickedPositions[0] == new Vector2Int((int)column, (int)row))
            {
                // Can't select the same cactus twice for crossbreeding
                return;
            }

            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
        }

        CurrentShopState = CurrentShopState switch
        {
            ShopState.PickMoveSource => ShopState.PickMoveTarget,
            ShopState.PickMoveTarget => ShopState.Default,
            ShopState.PickMutationTarget => ShopState.Default,
            ShopState.PickRepotTarget => ShopState.Default,
            ShopState.PickCrossBreedSourceOne => ShopState.PickCrossBreedSourceTwo,
            ShopState.PickCrossBreedSourceTwo => ShopState.PickCrossBreedTarget,
            _ => CurrentShopState
        };
        
        UpdateStateText();
    }

    public void OnClickEmptyCactusSpot(uint column, uint row)
    {
        if (CurrentShopState == ShopState.PickMoveTarget)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
            PerformMove();
        }
        else if (CurrentShopState == ShopState.PickCrossBreedTarget)
        {
            _clickedPositions.Add(new Vector2Int((int)column, (int)row));
            PerformCrossBreed();
        }

        CurrentShopState = CurrentShopState switch
        {
            ShopState.PickMoveTarget => ShopState.Default,
            ShopState.PickCrossBreedTarget => ShopState.Default,
            _ => CurrentShopState
        };

        UpdateStateText();
    }

    public void OnClickMutationButton()
    {
        CancelShopInteraction();
        CurrentShopState = ShopState.PickMutationTarget;
        UpdateStateText();
    }

    public void OnClickCrossBreedButton()
    {
        CancelShopInteraction();
        CurrentShopState = ShopState.PickCrossBreedSourceOne;
        UpdateStateText();
    }

    public void OnClickRepotButton()
    {
        CancelShopInteraction();
        CurrentShopState = ShopState.PickRepotTarget;
        UpdateStateText();
    }

    public void OnClickMoveButton()
    {
        CancelShopInteraction();
        CurrentShopState = ShopState.PickMoveSource;
        UpdateStateText();
    }

    public void PerformMutation()
    {
        Vector2Int sourcePos = _clickedPositions[0];

        CactusData sourceCactus = PlayerInventory.Instance.placedCacti[sourcePos.x, sourcePos.y];

        // Create mutated data
        CactusData mutated = CactusFactory.CreateMutatedCactus(sourceCactus);
        PlayerInventory.Instance.placedCacti[sourcePos.x, sourcePos.y] = mutated;

        // Actually perform the mutation in the world, not just the data
        PlayerInventory.Instance.placedCactusEntities[sourcePos.x, sourcePos.y].UpdateEntityData();

        _clickedPositions.Clear();
        UpdateStateText();
    }

    public void PerformCrossBreed()
    {
        Vector2Int sourceOnePos = _clickedPositions[0];
        Vector2Int sourceTwoPos = _clickedPositions[1];
        Vector2Int targetPos = _clickedPositions[2];

        CactusData sourceOneCactus = PlayerInventory.Instance.placedCacti[sourceOnePos.x, sourceOnePos.y];
        CactusData sourceTwoCactus = PlayerInventory.Instance.placedCacti[sourceTwoPos.x, sourceTwoPos.y];

        // Create child data
        CactusData child = CactusFactory.CreateCrossBredCactus(sourceOneCactus, sourceTwoCactus);
        PlayerInventory.Instance.placedCacti[targetPos.x, targetPos.y] = child;

        // Actually place child in the world, not just the data
        PlayerInventory.Instance.placedCactusEntities[targetPos.x, targetPos.y].UpdateEntityData();

        _clickedPositions.Clear();
        UpdateStateText();
    }

    public void PerformMove()
    {
        Vector2Int sourcePos = _clickedPositions[0];
        Vector2Int targetPos = _clickedPositions[1];

        CactusData sourceCactus = PlayerInventory.Instance.placedCacti[sourcePos.x, sourcePos.y];
        CactusData targetCactus = PlayerInventory.Instance.placedCacti[targetPos.x, targetPos.y];

        // Swap data in inventory
        PlayerInventory.Instance.placedCacti[targetPos.x, targetPos.y] = sourceCactus;
        PlayerInventory.Instance.placedCacti[sourcePos.x, sourcePos.y] = targetCactus;

        // Actually perform the move in the world, not just the data
        CactusEntity sourceEntity = PlayerInventory.Instance.placedCactusEntities[sourcePos.x, sourcePos.y];
        CactusEntity targetEntity = PlayerInventory.Instance.placedCactusEntities[targetPos.x, targetPos.y];

        sourceEntity.UpdateEntityData();
        targetEntity.UpdateEntityData();

        _clickedPositions.Clear();
        UpdateStateText();
    }

    public void PerformRepot()
    {
        Vector2Int sourcePos = _clickedPositions[0];

        // TODO

        _clickedPositions.Clear();
        UpdateStateText();
    }
}
