using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField]Text display;
    [SerializeField]UpgradeManager.UpgradeType upgradeType;

    void Start()
    {
        UpgradeManager.Instance.upgradeObtained += UpdateDisplay;
        UpdateDisplay(upgradeType);
    }

    void OnDestroy()
    {
        UpgradeManager.Instance.upgradeObtained -= UpdateDisplay;
    }

    void UpdateDisplay(UpgradeManager.UpgradeType type)
    {
        int amount = 0;
        string t = "";
        if (upgradeType == UpgradeManager.UpgradeType.LIFE)
        {
            amount = UpgradeManager.Instance.LifeUpgrades;
            t = "Life - ";
        }
        else if (upgradeType == UpgradeManager.UpgradeType.ATTACK)
        {
            amount = UpgradeManager.Instance.AttackUpgrades;
            t = "Attack - ";
        }
        else if (upgradeType == UpgradeManager.UpgradeType.DEXTERITY)
        {
            amount = UpgradeManager.Instance.DexterityUpgrades;
            t = "Dexterity - ";
        }
        else if (upgradeType == UpgradeManager.UpgradeType.MANA)
        {
            amount = UpgradeManager.Instance.ManaUpgrades;
            t = "Mana - ";
        }
        display.text = t + amount.ToString();
    }
}