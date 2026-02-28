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

    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        lifetime += Time.deltaTime;

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
