using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField]
    uint maxHP = 100;
    
    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float meleeRange = 0.4f;
    [SerializeField]
    uint attackDamage = 5;
    [SerializeField]
    float attackIntervalSeconds = 0.4f;

    [SerializeField]
    float walkWobbleStrength = 7.5f;
    [SerializeField]
    float walkWobbleSpeed = 2.5f;

    [SerializeField]
    uint seaweedDroppedOnKill = 10;

    private float _lifetime = 0f;
    private float _attackCooldownTimer = 0f;
    private uint _currentHP;

    void Awake()
    {
        _currentHP = maxHP;
    }

    void Update()
    {
        _lifetime += Time.deltaTime;

        _attackCooldownTimer -= Time.deltaTime;
        if (_attackCooldownTimer <= 0f)
        {
            _attackCooldownTimer = 0f;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, meleeRange);
        bool hasHit = false;
        CactusEntity hitCactus = null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Cactus"))
            {
                hasHit = true;
                hitCactus = hit.collider.GetComponent<CactusEntity>();
                break;
            }
        }

        if (hasHit && hitCactus != null && _attackCooldownTimer <= 0f)
        {
            hitCactus.Damage(attackDamage);
            _attackCooldownTimer = attackIntervalSeconds;
        }

        //Debug.DrawLine(transform.position, transform.position + Vector3.left * rayDist, hasHit ? Color.green : Color.red, 0f, false);
        if (!hasHit)
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(_lifetime * walkWobbleSpeed * movementSpeed) * walkWobbleStrength);
    }

    public void Damage(uint damageAmount)
    {
        if (damageAmount >= _currentHP)
        {
            _currentHP = 0;
            PlayerInventory.Instance.Seaweed += seaweedDroppedOnKill;
            Destroy(gameObject);
        }
        else
        {
            _currentHP -= damageAmount;
        }
    }
}
