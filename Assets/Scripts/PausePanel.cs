using UnityEngine;

public class PausePanel : MonoBehaviour
{
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