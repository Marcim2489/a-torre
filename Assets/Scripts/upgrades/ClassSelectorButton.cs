using UnityEngine;
using UnityEngine.UI;

public class ClassSelectorButton : MonoBehaviour
{
    [SerializeField]Image buttonImage;
    [SerializeField]Text nameText;
    [SerializeField]Text descriptionText;

    StaffData currentStaffData;

    public void SetupButton(StaffData data)
    {
        currentStaffData = data;
        buttonImage.sprite = currentStaffData.StaffSprite;
        nameText.text = currentStaffData.Name;
        descriptionText.text = currentStaffData.Description;
    }

    public void SelectClass()
    {
        ClassUpgradePanel.Instance.SelectClass(currentStaffData);
    }
}