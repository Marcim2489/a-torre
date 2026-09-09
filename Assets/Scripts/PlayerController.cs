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
        Vector2 cursorPosition = Mouse.current.position.ReadValue();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, 0f));
        Vector2 mouseDirecion = (mousePosition - transform.position).normalized;
        if (mouseDirecion == Vector2.zero)
        {
            mouseDirecion = Vector2.up;
        }
        p.transform.position = transform.position + projectileOffset * (Vector3)mouseDirecion;
        p.Shoot(mouseDirecion, projectileSpeed, projectileDamage);
    }


    IEnumerator CooldownTimer()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onAttackCooldown = false;
    }
}
