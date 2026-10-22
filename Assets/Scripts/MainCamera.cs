using Unity.Cinemachine;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance {get; private set;}
    [SerializeField]CinemachineCamera cinemachineCam;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public void ChangeFocus(Transform focus)
    {
        cinemachineCam.Follow = focus;
    }

    // public void SetPositionToFocus()
    // {
        
    // }
}