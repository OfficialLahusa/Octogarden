using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CactusEntity : MonoBehaviour
{

    [SerializeField]
    TMP_Text hpText;

    [SerializeField]
    SpriteRenderer flowerRenderer;

    [SerializeField]
    GameObject pricklyPearPrefab;

    CactusData entityData;
    
    public uint columnIndex = 0;
    public uint rowIndex = 0;
    public float attackIntervalSeconds = 1f;

    private float _hueOffset;
    private float _attackCooldownTimer = 0f;

    void Awake()
    {
        if(!PlayerInventory.Instance.IsInitialized)
        {
            PlayerInventory.CreateInitialPlacement();
        }

        if(PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] != null)
        {
            entityData = PlayerInventory.Instance.placedCacti[columnIndex, rowIndex];
        }   
        else
        {
            gameObject.SetActive(false);
            //entityData = CactusFactory.CreateCactus();
            //PlayerInventory.Instance.placedCacti[columnIndex, rowIndex] = entityData;
        }

        // Set flower sprite color based on cactus class
        if (flowerRenderer != null)
        {
            flowerRenderer.color = entityData.FlowerColor.ToColor();
        }

        _hueOffset = Random.Range(0f, 1f);
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
        }

        if (entityData.FlowerColor.Equals(CactusFlowerColor.Rainbow) && flowerRenderer != null)
        {
            flowerRenderer.color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.5f + _hueOffset, 1f), 1f, 1f);
        }

        if (CanPerformAttack())
        {
            PerformAttack();
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

    private bool CanPerformAttack()
    {
        bool cooldownReady = _attackCooldownTimer <= 0.05f;
        bool isRanged = entityData.Class == CactusClass.Ranged;

        bool enemyInMeleeRange = false;

        if (!isRanged)
        {
            float rayDist = 5f;
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
            projectile.damageOnHit = 20; // TODO: Refine this damage value based on cactus stats
            Debug.Log("Spawned projectile from Cactus Entity " + entityData.Name);
        }
        else
        {
            // TODO: Implement melee attack logic (e.g. damage enemies in front of the cactus)
        }

        _attackCooldownTimer = Random.Range(0.9f, 1.1f) * attackIntervalSeconds;
    }
}
