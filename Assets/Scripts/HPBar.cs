using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField]CharacterHealthManager healthManager;
    float initialHPBarSize;
    float initialBackgroundBarSize;
    int initialHP;
    RectTransform hpBarDisplay;
    [SerializeField]RectTransform barBackground;

    void Start()
    {
        hpBarDisplay = this.GetComponent<RectTransform>();
        initialHPBarSize = hpBarDisplay.rect.width;
        initialHP = healthManager.MaxHealth;
        healthManager.currentHealthChanged += UpdateHealthBar;
        UpdateHealthBar(healthManager.CurrentHealth);

        if(barBackground == null)
        {
            return;
        }
        initialBackgroundBarSize = barBackground.rect.width;
        healthManager.maxHealthChanged += UpdateBackground;
    }

    void UpdateHealthBar(int currentHP)
    {
        hpBarDisplay.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialHPBarSize * ((float)currentHP/(float)initialHP));
    }

    void UpdateBackground(int newMaxHP)
    {
        barBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialBackgroundBarSize * ((float)newMaxHP/(float)initialHP));
    }
}
