using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]Vector2 range;
    public Vector2 Range => range;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, range*2);
    }
}