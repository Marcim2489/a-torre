using UnityEngine;

public class ManaBar : MonoBehaviour
{
    // [SerializeField]CharacterHealthManager healthManager;
    float initialManaBarSize;
    float initialBackgroundBarSize;
    float initialMana => player.MaxMana;
    RectTransform manaBarDisplay;
    [SerializeField]RectTransform barBackground;
    PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
        manaBarDisplay = this.GetComponent<RectTransform>();
        initialManaBarSize = manaBarDisplay.rect.width;
        // initialMana = player.MaxMana;
        player.currentManaChanged += UpdateManaBar;
        UpdateManaBar(player.CurrentMana);

        if(barBackground == null)
        {
            return;
        }
        initialBackgroundBarSize = barBackground.rect.width;
        player.maxManaChanged += UpdateBackground;
    }

    void UpdateManaBar(float currentMana)
    {
        Debug.Log(currentMana);
        manaBarDisplay.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialManaBarSize * (currentMana/initialMana));
    }

    void UpdateBackground(float newMaxMana)
    {
        barBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialBackgroundBarSize * ((float)newMaxMana/(float)initialMana));
    }
}