using UnityEngine;

public class PricklyPearProjectile : MonoBehaviour
{
    public uint damageOnHit = 40;

    private static readonly float MAX_LIFETIME_SECONDS = 8f;
    private float _totalRotation;
    private float _lifetime = 0f;

    void Awake()
    {
        _totalRotation = Random.Range(0, 360);
    }

    void Update()
    {
        _lifetime += Time.deltaTime;
        if (_lifetime >= MAX_LIFETIME_SECONDS)
        {
            Destroy(gameObject);
        }

        _totalRotation += 360 * Time.deltaTime;
        transform.position += Vector3.right * Time.deltaTime * 5f;
        transform.rotation = Quaternion.Euler(0, 0, -_totalRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");

            MeleeEnemy enemy = collision.GetComponentInParent<MeleeEnemy>();
            if (enemy != null)
            {
                enemy.Damage(damageOnHit);
            }
            Destroy(gameObject);
        }
    }
}
