using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    uint maxHP = 100;
    [SerializeField]
    uint currentHP = 100;
    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float walkWobbleStrength = 7.5f;
    [SerializeField]
    float walkWobbleSpeed = 2.5f;

    float lifetime = 0f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        lifetime += Time.deltaTime;

        float rayDist = 1f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, rayDist);
        bool hasHit = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Cactus"))
            {
                hasHit = true;
                break;
            }
        }
            
        Debug.DrawLine(transform.position, transform.position + Vector3.left * rayDist, hasHit ? Color.green : Color.red, 0f, false);
        if (!hasHit)
            transform.Translate(Vector3.left * movementSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(lifetime * walkWobbleSpeed * movementSpeed) * walkWobbleStrength);
    }

    public void Damage(uint damageAmount)
    {
        if (damageAmount >= currentHP)
        {
            currentHP = 0;
            Destroy(gameObject);
        }
        else
        {
            currentHP -= damageAmount;
        }
    }
}
