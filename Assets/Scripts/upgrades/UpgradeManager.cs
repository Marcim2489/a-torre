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
    int manaUpgrades;

    public int LifeUpgrades => lifeUpgrades;
    public int AttackUpgrades => attackUpgrades;
    public int DexterityUpgrades => dexterityUpgrades;
    public int ManaUpgrades => manaUpgrades;

    [SerializeField]StaffData defaultClass;
    [SerializeField]StaffData lifeClassA;
    [SerializeField]StaffData lifeClassB;
    [SerializeField]StaffData attackClassA;
    [SerializeField]StaffData attackClassB;
    [SerializeField]StaffData dexterityClassA;
    [SerializeField]StaffData dexterityClassB;
    [SerializeField]StaffData manaClassA;
    [SerializeField]StaffData manaClassB;

    Dictionary<(UpgradeType upgradeType, int tier), (StaffData, StaffData)> existingClasses;

    public Dictionary<(UpgradeType upgradeType, int tier), (StaffData, StaffData)> ExistingClasses => existingClasses;

    public enum UpgradeType {LIFE, ATTACK, DEXTERITY, MANA}

    bool changedClass = false;

    UpgradeType chosenClass;

    public event UnityAction<UpgradeType> upgradeObtained = delegate {};
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
                {(UpgradeType.MANA, 1), (manaClassA, manaClassB)}
            };
            return;
        }
        Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        LevelPoints += amount * 15;
        // Debug.Log(LevelPoints);
    }

    public void GetUpgrade(UpgradeType type)
    {
        if (LevelPoints <= 0)
        {
            return;
        }
        // if (changedClass && type)
        
        int pointsOfClass = 0;
        if (type == UpgradeType.LIFE)
        {
            if (changedClass && chosenClass != type && lifeUpgrades >= 5)
            {
                return;
            }
            lifeUpgrades++;
            pointsOfClass = lifeUpgrades;
        }
        else if (type == UpgradeType.ATTACK)
        {
            if (changedClass && chosenClass != type && attackUpgrades >= 5)
            {
                return;
            }
            attackUpgrades++;
            pointsOfClass = attackUpgrades;
        }
        else if (type == UpgradeType.DEXTERITY)
        {
            if (changedClass && chosenClass != type && dexterityUpgrades >= 5)
            {
                return;
            }
            dexterityUpgrades++;
            pointsOfClass = dexterityUpgrades;
        }
        else if (type == UpgradeType.MANA)
        {
            if (changedClass && chosenClass != type && manaUpgrades >= 5)
            {
                return;
            }
            manaUpgrades++;
            pointsOfClass = manaUpgrades;
        }
        if (changedClass == false && pointsOfClass == 5)
        {
            ClassUpgradePanel.Instance.Appear(type, 1);
            PausePanel.Instance.gameObject.SetActive(false);
            changedClass = true;
        }
        LevelPoints--;
        upgradeObtained.Invoke(type);
    }
}
