using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField]int maxHealth = 50;
    int currentHealth;
    public int CurrentHealth => currentHealth;

    public event UnityAction tookDamage = delegate {};
    public event UnityAction<int> healthChanged = delegate {};
    public event UnityAction died = delegate {};

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.OnContact();
            TakeDamage(projectile.Damage);
        }
    }

    void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }
        tookDamage.Invoke();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            died.Invoke();
        }
        healthChanged.Invoke(CurrentHealth);
    }
}
