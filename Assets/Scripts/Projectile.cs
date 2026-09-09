using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage {get; private set;}

    [SerializeField]Rigidbody2D rb;
    [SerializeField]float lifeTime = 3f;
    [SerializeField]bool destroyOnContact = true;

    public void Shoot(Vector2 direction, float speed, int damage)
    {
        rb.linearVelocity = direction * speed;
        Damage = damage;
        if (lifeTime > 0f)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    public void OnContact()
    {
        if (destroyOnContact)
        {
            Destroy(gameObject);
        }
    }
}
