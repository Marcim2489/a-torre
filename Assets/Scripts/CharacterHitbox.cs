using UnityEngine;
using UnityEngine.Events;

public class CharacterHitbox : MonoBehaviour
{
    public event UnityAction<int> tookHit = delegate {};

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.OnContact();
            tookHit.Invoke(projectile.Damage);
        }
    }
}
