using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField]Animator animator;
    [SerializeField]SpriteRenderer spriteRenderer;

    PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
    }

    void Update()
    {
        animator.SetBool("walking", player.Walking);
        spriteRenderer.flipX = player.LookingLeft;
    }
}
