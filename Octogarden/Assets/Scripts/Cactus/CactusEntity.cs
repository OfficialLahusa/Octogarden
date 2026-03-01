using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CactusEntity : MonoBehaviour
{
    // Variants
    [SerializeField]
    GameObject basicMelee;
    [SerializeField]
    GameObject basicRanged;
    [SerializeField]
    GameObject basicTank;
    [SerializeField]
    GameObject vampireMelee;
    [SerializeField]
    GameObject vampireRanged;
    [SerializeField]
    GameObject vampireTank;
    [SerializeField]
    GameObject popsickleMelee;
    [SerializeField]
    GameObject popsickleRanged;
    [SerializeField]
    GameObject popsickleTank;
    [SerializeField]
    GameObject fisherMelee;
    [SerializeField]
    GameObject fisherRanged;
    [SerializeField]
    GameObject fisherTank;
    [SerializeField]
    GameObject basicPot;
    [SerializeField]
    GameObject steelPot;
    [SerializeField]
    GameObject wineBarrel;
    [SerializeField]
    GameObject plasticPot;
    [SerializeField]
    GameObject goldenPot;
    [SerializeField]
    GameObject falloutPot;
    [SerializeField]
    GameObject crystalPot;

    [SerializeField]
    TMP_Text hpText;

    [SerializeField]
    SpriteRenderer flowerRenderer;

    [SerializeField]
    GameObject pricklyPearPrefab;
    [SerializeField]
    GameObject meleeIndicatorPrefab;

    CactusData entityData;
    
    public uint columnIndex = 0;
    public uint rowIndex = 0;

    private float _hueOffset;
    private float _attackCooldownTimer = 0f;

    private WaveSpawner _waveSpawner;

    void Awake()
    {
        if(!PlayerInventory.Instance.IsInitialized)
        {
            PlayerInventory.CreateInitialPlacement();
        }

        // Register entity in inventory for reference by other systems (e.g. ShopHandler)
        PlayerInventory.Instance.RegisterCactusEntity(this, columnIndex, rowIndex);

        UpdateEntityData();

        _hueOffset = Random.Range(0f, 1f);   
        
        if (entityData != null)
            _attackCooldownTimer = entityData.AttackIntervalSeconds;

        _waveSpawner = FindFirstObjectByType<WaveSpawner>();
    }

    void Update()
    {
        _attackCooldownTimer -= Time.deltaTime;
        if (_attackCooldownTimer < 0f)
            _attackCooldownTimer = 0f;

        if (hpText != null)
            hpText.text = $"{entityData.Name}\n{entityData.CurrentHealth}/{entityData.MaxHealth}";

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


        if (wasHovered)
        {
            TooltipManager.ShowTooltip(entityData);

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // Trigger ShopHandler Event
                ShopHandler shopHandler = FindFirstObjectByType<ShopHandler>();
                shopHandler.OnClickExistingCactus(columnIndex, rowIndex);
            }
        }

        if (entityData.FlowerColor.Equals(CactusFlowerColor.Rainbow) && flowerRenderer != null)
        {
            flowerRenderer.color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.5f + _hueOffset, 1f), 1f, 1f);
        }

        if (_waveSpawner.IsWaveActive && !_waveSpawner.IsCompleted && CanPerformAttack())
        {
            PerformAttack();
        }
    }

    public void UpdateEntityData()
    {
        if (PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] != null)
        {
            gameObject.SetActive(true);
            entityData = PlayerInventory.Instance.placedCacti[columnIndex, rowIndex];
        }
        else
        {
            gameObject.SetActive(false);
            //entityData = CactusFactory.CreateCactus();
            //PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] = entityData;
        }

        // Set flower sprite color based on cactus class
        if (flowerRenderer != null && entityData != null)
        {
            flowerRenderer.color = entityData.FlowerColor.ToColor();
        }

        // Activate the correct variant based on cactus class
        if (entityData != null)
        {
            basicMelee.SetActive(entityData.Class == CactusClass.Melee && entityData.OutfitType == CactusOutfitType.Basic);
            basicRanged.SetActive(entityData.Class == CactusClass.Ranged && entityData.OutfitType == CactusOutfitType.Basic);
            basicTank.SetActive(entityData.Class == CactusClass.Tank && entityData.OutfitType == CactusOutfitType.Basic);

            vampireMelee.SetActive(entityData.Class == CactusClass.Melee && entityData.OutfitType == CactusOutfitType.Vampiric);
            vampireRanged.SetActive(entityData.Class == CactusClass.Ranged && entityData.OutfitType == CactusOutfitType.Vampiric);
            vampireTank.SetActive(entityData.Class == CactusClass.Tank && entityData.OutfitType == CactusOutfitType.Vampiric);

            popsickleMelee.SetActive(entityData.Class == CactusClass.Melee && entityData.OutfitType == CactusOutfitType.Popsickle);
            popsickleRanged.SetActive(entityData.Class == CactusClass.Ranged && entityData.OutfitType == CactusOutfitType.Popsickle);
            popsickleTank.SetActive(entityData.Class == CactusClass.Tank && entityData.OutfitType == CactusOutfitType.Popsickle);

            fisherMelee.SetActive(entityData.Class == CactusClass.Melee && entityData.OutfitType == CactusOutfitType.Barbed);
            fisherRanged.SetActive(entityData.Class == CactusClass.Ranged && entityData.OutfitType == CactusOutfitType.Barbed);
            fisherTank.SetActive(entityData.Class == CactusClass.Tank && entityData.OutfitType == CactusOutfitType.Barbed);

            basicPot.SetActive(entityData.PotType == CactusPotType.Ceramic);
            steelPot.SetActive(entityData.PotType == CactusPotType.Steel);
            wineBarrel.SetActive(entityData.PotType == CactusPotType.WineBarrel);
            plasticPot.SetActive(entityData.PotType == CactusPotType.Plastic);
            goldenPot.SetActive(entityData.PotType == CactusPotType.Golden);
            falloutPot.SetActive(entityData.PotType == CactusPotType.RadioactiveBarrel);
            crystalPot.SetActive(entityData.PotType == CactusPotType.Glass);
        }
    }

    public void Damage(uint damageAmount)
    {
        if (damageAmount >= entityData.CurrentHealth)
        {
            entityData.CurrentHealth = 0;
            PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] = null;
            gameObject.SetActive(false);
            basicMelee.SetActive(false);
            basicRanged.SetActive(false);
            basicTank.SetActive(false);

            vampireMelee.SetActive(false);
            vampireRanged.SetActive(false);
            vampireTank.SetActive(false);

            popsickleMelee.SetActive(false);
            popsickleRanged.SetActive(false);
            popsickleTank.SetActive(false);

            fisherMelee.SetActive(false);
            fisherRanged.SetActive(false);
            fisherTank.SetActive(false);

            basicPot.SetActive(false);
            steelPot.SetActive(false);
            wineBarrel.SetActive(false);
            plasticPot.SetActive(false);
            goldenPot.SetActive(false);
            falloutPot.SetActive(false);
            crystalPot.SetActive(false);
        }
        else
        {
            entityData.CurrentHealth -= damageAmount;
        }
    }

    private bool CanPerformAttack()
    {
        bool cooldownReady = _attackCooldownTimer <= 0.05f;
        bool isRanged = entityData.Class == CactusClass.Ranged;

        bool enemyInMeleeRange = false;

        if (!isRanged)
        {
            float rayDist = 0.85f;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right, rayDist);
            MeleeEnemy hitEnemy = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                {
                    enemyInMeleeRange = true;
                    hitEnemy = hit.collider.GetComponent<MeleeEnemy>();
                    break;
                }
            }
        }

        return cooldownReady && (isRanged || enemyInMeleeRange);
    }

    private void PerformAttack()
    {
        if (entityData.Class == CactusClass.Ranged)
        {
            GameObject projectileObj = Instantiate(pricklyPearPrefab, transform.position, Quaternion.identity);
            PricklyPearProjectile projectile = projectileObj.GetComponent<PricklyPearProjectile>();
            projectile.damageOnHit = entityData.AttackDamage;
        }
        else
        {
            float rayDist = 2.25f;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right, rayDist);
            MeleeEnemy hitEnemy = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                {
                    hitEnemy = hit.collider.GetComponentInParent<MeleeEnemy>();
                    hitEnemy.Damage(entityData.AttackDamage);
                }
            }

            GameObject meleeIndicatorObj = Instantiate(meleeIndicatorPrefab, transform.position, Quaternion.identity);
            meleeIndicatorObj.transform.position += Vector3.right * meleeIndicatorObj.transform.localScale.x / 2; // Position the indicator in front of the cactus

        }

        _attackCooldownTimer = Random.Range(0.9f, 1.1f) * entityData.AttackIntervalSeconds;
    }
}
