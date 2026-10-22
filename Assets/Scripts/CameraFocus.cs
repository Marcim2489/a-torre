using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]float maxDistance = 3f;
    [SerializeField]Vector2 multiplier = new Vector2(1.4f, 1f);
    Vector2 initialLocalPosition;
    Vector2 initialPosition;
    bool active;
    Transform player;

    void Start()
    {
        // player = PlayerController.Instance.transform;
        initialLocalPosition = transform.localPosition;
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        HandlePosition();
    }

    public void Activate()
    {
        active = true;
        HandlePosition();
    }

    public void Deactivate()
    {
        active = false;
        transform.localPosition = initialLocalPosition;
    }

    void HandlePosition()
    {
        if (active == false)
        {
            return;
        }
        float distance = maxDistance;
        float playerDistance = Vector2.Distance(initialPosition, PlayerController.Instance.transform.position);
        if (playerDistance < distance)
        {
            distance = playerDistance;
        }
        transform.localPosition = initialLocalPosition + ((Vector2)PlayerController.Instance.transform.position - initialPosition).normalized * multiplier * distance;
    }
}
