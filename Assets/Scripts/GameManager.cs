using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    int roomsClearedThisRun = 0;

    public int RoomsClearedThisRun => roomsClearedThisRun;

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

    public void Reset()
    {
        roomsClearedThisRun = 0;
    }

    public void ClearedRoom()
    {
        roomsClearedThisRun++;
        UpgradeManager.Instance.AddPoints(1);
    }

    public void EndRun()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void StartNewRun()
    {
        Reset();
        SceneManager.LoadScene("SampleScene");
    }
}
