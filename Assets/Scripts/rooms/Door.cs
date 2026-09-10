using UnityEngine;

public class Door : MonoBehaviour
{
    enum Direction {RIGHT, LEFT, UP, DOWN}

    public Vector2 RoomCoord {get; set;}
    [SerializeField]Direction direction;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (RoomsManager.Instance.InTransition)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 nextRoomCoord = RoomCoord;
            if(direction == Direction.RIGHT)
            {
                if (collision.transform.position.x > transform.position.x)
                {
                    return;
                }
                nextRoomCoord += Vector2.right;
            }
            else if (direction == Direction.LEFT)
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    return;
                }
                nextRoomCoord += Vector2.left;
            }
            else if (direction == Direction.UP)
            {
                if (collision.transform.position.y > transform.position.y)
                {
                    return;
                }
                nextRoomCoord += Vector2.up;
            }
            else
            {
                if (collision.transform.position.y < transform.position.y)
                {
                    return;
                }
                nextRoomCoord += Vector2.down;
            }
            RoomsManager.Instance.ChangeRoom(nextRoomCoord);
        }
    }
}