using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance {get; private set;}
    public event UnityAction<bool> pauseToggled = delegate {};

    [SerializeField]InputAction pauseInput;

    bool paused = false;

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
        pauseInput.Enable();
        pauseInput.started += Pause;
    }

    void Pause(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
        {
            return;
        }
        if (paused == false)
        {
            Time.timeScale = 0f;
            paused = true;
        }
        else
        {
            Time.timeScale = 1f;
            paused = false;
        }
        pauseToggled.Invoke(paused);
    }
}
