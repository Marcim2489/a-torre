using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField]int maxHealth = 50;
    [SerializeField]float immunityTime = 0f;
    int currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public event UnityAction tookDamage = delegate {};
    public event UnityAction<int> currentHealthChanged = delegate {};
    public event UnityAction<int> maxHealthChanged = delegate {};
    public event UnityAction died = delegate {};
    public event UnityAction immunityStarted = delegate {};
    public event UnityAction immunityEnded = delegate {};
    bool immune = false;

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
        if (immune)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            // Debug.Log("a");
            // if (projectile.HittedObjects.Contains(gameObject))
            // {
            //     return;
            // }
            projectile.OnContact();
            TakeDamage(projectile.Damage);
        }
        else if (collision.gameObject.TryGetComponent<Hitbox>(out Hitbox hitbox))
        {
            TakeDamage(hitbox.damage);
        }
    }

    void TakeDamage(int damage)
    {
        if (damage <= 0 || immune)
        {
            return;
        }
        if (immunityTime > 0f)
        {
            StartCoroutine(ImmunityCoroutine());
        }
        tookDamage.Invoke();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            died.Invoke();
            currentHealthChanged.Invoke(CurrentHealth);
            // Destroy(gameObject);
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

    IEnumerator ImmunityCoroutine()
    {
        immune = true;
        immunityStarted.Invoke();
        yield return new WaitForSeconds(immunityTime);
        immune = false;
        immunityEnded.Invoke();
    }
}
