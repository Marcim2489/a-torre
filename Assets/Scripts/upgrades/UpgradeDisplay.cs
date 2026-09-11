using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField]Text display;
    [SerializeField]UpgradeManager.UpgradeType upgradeType;

    void Start()
    {
        UpgradeManager.Instance.upgradeObtained += UpdateDisplay;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        UpgradeManager.Instance.upgradeObtained -= UpdateDisplay;
    }

    void UpdateDisplay()
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
        display.text = t + amount.ToString();
    }
}