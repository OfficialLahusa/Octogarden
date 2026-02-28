using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    uint maxHP = 100;
    [SerializeField]
    uint currentHP = 100;
    [SerializeField]
    float movementSpeed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
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
