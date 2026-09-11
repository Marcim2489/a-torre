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

    (int life, int attack, int dexterity) upgradesObtained;

    // public int LevelPoints => levelPoints;
    public int LifeUpgrades => upgradesObtained.life;
    public int AttackUpgrades => upgradesObtained.attack;
    public int DexterityUpgrades => upgradesObtained.dexterity;

    public enum UpgradeType {LIFE, ATTACK, DEXTERITY}

    public event UnityAction upgradeObtained = delegate {};
    public event UnityAction<int> pointSpended = delegate {};
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        LevelPoints += amount;
        // Debug.Log(LevelPoints);
    }

    public void GetUpgrade(UpgradeType type)
    {
        if (LevelPoints <= 0)
        {
            return;
        }
        LevelPoints--;
        if (type == UpgradeType.LIFE)
        {
            upgradesObtained.life++;
        }
        else if (type == UpgradeType.ATTACK)
        {
            upgradesObtained.attack++;
        }
        else if (type == UpgradeType.DEXTERITY)
        {
            upgradesObtained.dexterity++;
        }
        upgradeObtained.Invoke();
    }
}
