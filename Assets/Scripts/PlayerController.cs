using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Projectile projectile;
    [SerializeField]InputAction movementInput;
    [SerializeField]InputAction attackInput;
    [SerializeField]float movementSpeed = 15f;
    [SerializeField]float cooldownTime = 0.3f;
    [SerializeField]float projectileSpeed = 16f;
    [SerializeField]int projectileDamage = 10;
    [SerializeField]float projectileOffset = 2f;

    bool onAttackCooldown = false;

    Vector2 Direction => movementInput.ReadValue<Vector2>().normalized;
    bool AttackPressed => attackInput.IsPressed();

    void Start()
    {
        movementInput.Enable();
        attackInput.Enable();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementSpeed * Direction;
        if(AttackPressed && onAttackCooldown == false)
        {
            ShootProjectile();
            StartCoroutine(CooldownTimer());
        }
    }

    void ShootProjectile()
    {
        Projectile p = Instantiate(projectile);
        Vector2 cursorPositionInScreen = Mouse.current.position.ReadValue();
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(cursorPositionInScreen);
        Vector2 mouseDirection = ((Vector2)(mousePositionInWorld - transform.position)).normalized;
        if (mouseDirection == Vector2.zero)
        {
            mouseDirection = Vector2.up;
        }
        p.transform.position = transform.position + projectileOffset * (Vector3)mouseDirection;
        // Debug.Log($"sem norm {mouseDirection} -- com norm {mouseDirection.normalized}");
        p.Shoot(mouseDirection, projectileSpeed, projectileDamage);
    }


    IEnumerator CooldownTimer()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onAttackCooldown = false;
    }
}
