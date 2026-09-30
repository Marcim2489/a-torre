using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField]float staffPositioningSpeed = 0.2f;
    [SerializeField]float staffOffsetOnAttack = 0.3f;

    Vector2 initialPosition;
    Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if (PlayerController.Instance.AttackPressed)
        {
            Vector2 cursorDirection = PlayerController.Instance.CursorDirectiom;
            // transform.up = cursorDirection;
            transform.up = Vector2.Lerp(transform.up, cursorDirection, staffPositioningSpeed);
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.FromToRotation(transform.up, cursorDirection), staffPositioningSpeed);
            transform.localPosition = Vector2.Lerp(transform.localPosition,
            cursorDirection * staffOffsetOnAttack,
            staffPositioningSpeed);
        }
        else
        {
            transform.localRotation = initialRotation;
            // if ((Vector2)transform.localPosition != initialPosition)
            // {
            transform.localPosition = Vector2.Lerp(transform.localPosition, initialPosition, staffPositioningSpeed);
            // }
        }
    }
}
