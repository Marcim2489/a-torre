using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage => hitbox.damage;

    [SerializeField]Rigidbody2D rb;
    [SerializeField]float lifeTime = 3f;
    [SerializeField]bool destroyOnContact = true;
    [SerializeField]Hitbox hitbox;

    public void Shoot(Vector2 direction, float speed, int damage)
    {
        rb.linearVelocity = direction * speed;
        hitbox.damage = damage;
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
