using UnityEngine;
using UnityEngine.UI;

public class LevelPointsDisplay : MonoBehaviour
{
    [SerializeField]Text display;

    void Start()
    {
        UpgradeManager.Instance.pointSpended += UpdateDisplay;
        UpdateDisplay(UpgradeManager.Instance.LevelPoints);
    }

    void OnDestroy()
    {
        UpgradeManager.Instance.pointSpended -= UpdateDisplay;
    }

    void UpdateDisplay(int points)
    {
        display.text = points.ToString();
    }
}