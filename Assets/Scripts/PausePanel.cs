using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public static PausePanel Instance {get; private set;}

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
        PauseManager.Instance.pauseToggled += OnPause;
        gameObject.SetActive(false);
    }

    void OnPause(bool paused)
    {
        gameObject.SetActive(paused);
    }
}