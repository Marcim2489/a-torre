using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField]int maxHealth = 50;
    int currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public event UnityAction tookDamage = delegate {};
    public event UnityAction<int> currentHealthChanged = delegate {};
    public event UnityAction<int> maxHealthChanged = delegate {};
    public event UnityAction died = delegate {};

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // void Start()
    // {
    //     currentHealth = maxHealth;
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.OnContact();
            TakeDamage(projectile.Damage);
        }else if (collision.gameObject.TryGetComponent<Hitbox>(out Hitbox hitbox))
        {
            TakeDamage(hitbox.damage);
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
            currentHealthChanged.Invoke(CurrentHealth);
            Destroy(gameObject);
            return;
        }
        currentHealthChanged.Invoke(CurrentHealth);
    }

    public void ChangeMaxHealth(int amount)
    {
        maxHealth += amount;
        if (maxHealth <= 0)
        {
            maxHealth = 1;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            currentHealthChanged.Invoke(CurrentHealth);
        }
        maxHealthChanged.Invoke(MaxHealth);
    }
}
