using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField]Vector2 maxDistance = new Vector2(4f, 6f);
    // [SerializeField]Vector2 multiplier = new Vector2(1.4f, 1f);
    // Vector2 initialLocalPosition;
    Vector2 initialPosition;
    bool active;
    bool initialPositionSet = false;
    // Transform player;

    void Start()
    {
        // player = PlayerController.Instance.transform;
        // initialLocalPosition = transform.localPosition;
        TrySetInitialPosition();
    }

    void TrySetInitialPosition()
    {
        if (initialPositionSet)
        {
            return;
        }
        initialPosition = transform.position;
        initialPositionSet = true;
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
        TrySetInitialPosition();
        transform.position = initialPosition;
        // transform.localPosition = initialLocalPosition;
    }

    void HandlePosition()
    {
        if (active == false)
        {
            return;
        }
        TrySetInitialPosition();
        Vector2 newPosition = PlayerController.Instance.transform.position;
        if (newPosition.x > initialPosition.x + maxDistance.x)
        {
            newPosition.x = initialPosition.x + maxDistance.x;
        }
        else if (newPosition.x < initialPosition.x - maxDistance.x)
        {
            newPosition.x = initialPosition.x - maxDistance.x;
        }
        if (newPosition.y > initialPosition.y + maxDistance.y)
        {
            newPosition.y = initialPosition.y + maxDistance.y;
        }
        else if (newPosition.y < initialPosition.y - maxDistance.y)
        {
            newPosition.y = initialPosition.y - maxDistance.y;
        }
        transform.position = newPosition;
        // float distance = maxDistance;
        // float playerDistance = Vector2.Distance(initialPosition, PlayerController.Instance.transform.position);
        // if (playerDistance < distance)
        // {
        //     distance = playerDistance;
        // }
        // transform.localPosition = initialLocalPosition + ((Vector2)PlayerController.Instance.transform.position - initialPosition).normalized * multiplier * distance;
    }
}
