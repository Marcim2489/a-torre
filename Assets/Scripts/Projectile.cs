using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage => hitbox.damage;

    [SerializeField]Rigidbody2D rb;
    [SerializeField]float lifeTime = 3f;
    [SerializeField]bool destroyOnContact = true;
    [SerializeField]Hitbox hitbox;
    // [SerializeField]LayerMask wallLayer;

    // List<GameObject> hittedObjects = new List<GameObject>();

    // public List<GameObject> HittedObjects => hittedObjects;

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

    // public void OnContact(GameObject obj)
    // {
    //     if (destroyOnContact)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     hittedObjects.Add(obj);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"{LayerMask.LayerToName(other.gameObject.layer)}");
        if (LayerMask.LayerToName(other.gameObject.layer) == "Wall")
        {
            // Debug.Log("b");
            Destroy(gameObject, 0.05f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Wall")
        {
            // Debug.Log("b");
            Destroy(gameObject, 0.05f);
        }
    }
}
