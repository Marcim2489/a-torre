using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance {get; private set;}

    int levelPoints;

    public int LevelPoints
    {
        get
        {
            return levelPoints;
        }
        private set
        {
            levelPoints = value;
            pointSpended.Invoke(levelPoints);
        }
    }

    int lifeUpgrades;
    int attackUpgrades;
    int dexterityUpgrades;

    public int LifeUpgrades => lifeUpgrades;
    public int AttackUpgrades => attackUpgrades;
    public int DexterityUpgrades => dexterityUpgrades;

    [SerializeField]StaffData defaultClass;
    [SerializeField]StaffData lifeClassA;
    [SerializeField]StaffData lifeClassB;
    [SerializeField]StaffData attackClassA;
    [SerializeField]StaffData attackClassB;
    [SerializeField]StaffData dexterityClassA;
    [SerializeField]StaffData dexterityClassB;

    Dictionary<(UpgradeType upgradeType, int tier), (StaffData, StaffData)> existingClasses;

    public Dictionary<(UpgradeType upgradeType, int tier), (StaffData, StaffData)> ExistingClasses => existingClasses;

    public enum UpgradeType {LIFE, ATTACK, DEXTERITY, MANA}

    bool changedClass = false;

    public event UnityAction upgradeObtained = delegate {};
    public event UnityAction<int> pointSpended = delegate {};

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            existingClasses = new Dictionary<(UpgradeType upgradeType, int tier), (StaffData, StaffData)>()
            {
                {(UpgradeType.LIFE, 1), (lifeClassA, lifeClassB)},
                {(UpgradeType.ATTACK, 1), (attackClassA, attackClassB)},
                {(UpgradeType.DEXTERITY, 1), (dexterityClassA, dexterityClassB)},
            };
            return;
        }
        Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        LevelPoints += amount * 5;
        // Debug.Log(LevelPoints);
    }

    public void GetUpgrade(UpgradeType type)
    {
        if (LevelPoints <= 0)
        {
            return;
        }
        LevelPoints--;
        int pointsOfClass = 0;
        if (type == UpgradeType.LIFE)
        {
            lifeUpgrades++;
            pointsOfClass = lifeUpgrades;
        }
        else if (type == UpgradeType.ATTACK)
        {
            attackUpgrades++;
            pointsOfClass = attackUpgrades;
        }
        else if (type == UpgradeType.DEXTERITY)
        {
            dexterityUpgrades++;
            pointsOfClass = dexterityUpgrades;
        }
        if (changedClass == false && pointsOfClass == 5)
        {
            ClassUpgradePanel.Instance.Appear(type, 1);
            PausePanel.Instance.gameObject.SetActive(false);
        }
        
        upgradeObtained.Invoke();
    }
}
