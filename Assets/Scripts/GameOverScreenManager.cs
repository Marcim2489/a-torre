using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField]Text roomsClearedText;

    void Start()
    {
        roomsClearedText.text = $"Rooms cleared: {GameManager.Instance.RoomsClearedThisRun}";
    }

    public void Retry()
    {
        GameManager.Instance.StartNewRun();
    }
}