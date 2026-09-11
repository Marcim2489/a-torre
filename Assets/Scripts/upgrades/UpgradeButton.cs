using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]UpgradeManager.UpgradeType upgradeType;

    public void GetUpgrade()
    {
        UpgradeManager.Instance.GetUpgrade(upgradeType);
    }
}
