using UnityEngine;

public class ClassUpgradePanel : MonoBehaviour
{
    public static ClassUpgradePanel Instance {get; private set;}

    [SerializeField]ClassSelectorButton buttonOne;
    [SerializeField]ClassSelectorButton buttonTwo;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Appear(UpgradeManager.UpgradeType type, int tier)
    {
        PauseManager.Instance.DisablePauseInput();
        (StaffData, StaffData) data = UpgradeManager.Instance.ExistingClasses[(type, tier)];
        if (data == (null, null))
        {
            Debug.Log("Oh ma gaa");
        }
        buttonOne.SetupButton(data.Item1);
        buttonTwo.SetupButton(data.Item2);
        this.gameObject.SetActive(true);
    }

    public void SelectClass(StaffData data)
    {
        PlayerController.Instance.ChangeStaff(data);
        this.gameObject.SetActive(false);
        PauseManager.Instance.ForceTogglePause();
        PauseManager.Instance.EnablePauseInput();
    }
}
