using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField]float positioningSpeed = 0.4f;
    [SerializeField]float offsetOnAttack = 0.7f;

    float startingPosX;
    float startingRotZ;

    Vector2 defaultPosition;
    Quaternion defaultRotation;

    void Start()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation;
        startingPosX = defaultPosition.x;
        startingRotZ = defaultRotation.z;
    }

    void Update()
    {
        if (PlayerController.Instance.AttackPressed)
        {
            Vector2 cursorDirection = PlayerController.Instance.CursorDirectiom;
            // transform.up = cursorDirection;
            transform.up = Vector2.Lerp(transform.up, cursorDirection, positioningSpeed);
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.FromToRotation(transform.up, cursorDirection), staffPositioningSpeed);
            transform.localPosition = Vector2.Lerp(transform.localPosition,
            cursorDirection * offsetOnAttack,
            positioningSpeed);
        }
        else
        {
            if (PlayerController.Instance.LookingLeft)
            {
                defaultPosition.x = -startingPosX;
                defaultRotation.z = -startingRotZ;
            }
            else
            {
                defaultPosition.x = startingPosX;
                defaultRotation.z = startingRotZ;
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, defaultRotation, positioningSpeed);
            // if ((Vector2)transform.localPosition != initialPosition)
            // {
            transform.localPosition = Vector2.Lerp(transform.localPosition, defaultPosition, positioningSpeed);
            // }
        }
    }
}
