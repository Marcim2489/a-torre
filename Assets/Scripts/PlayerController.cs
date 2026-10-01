using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Projectile projectile;
    [SerializeField]InputAction movementInput;
    [SerializeField]InputAction attackInput;
    [SerializeField]Camera cam;
    [SerializeField]float movementSpeed = 15f;
    [SerializeField]float cooldownTime = 0.3f;
    [SerializeField]float projectileSpeed = 16f;
    [SerializeField]int projectileBaseDamage = 10;
    [SerializeField]int damageUpgradeFactor = 3;
    [SerializeField]float projectileOffset = 2f;

    bool onAttackCooldown = false;

    Vector2 Direction => movementInput.ReadValue<Vector2>().normalized;
    public bool AttackPressed => attackInput.IsPressed();

    public Vector2 CursorDirectiom
    {
        get
        {
            Vector2 cursorPositionInScreen = Mouse.current.position.ReadValue();
            Vector3 mousePositionInWorld = cam.ScreenToWorldPoint(cursorPositionInScreen);
            return ((Vector2)(mousePositionInWorld - transform.position)).normalized;
        }
    }

    public bool LookingLeft {get; private set;}

    int ProjectileDamage => projectileBaseDamage + UpgradeManager.Instance.AttackUpgrades * damageUpgradeFactor;

    IEnumerator attackCooldownCoroutine;

    public static PlayerController Instance {get; private set;}

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        movementInput.Enable();
        attackInput.Enable();
        RoomsManager.Instance.roomTransitionStarted += () =>
        {
            if (attackCooldownCoroutine != null)
            {
                StopCoroutine(attackCooldownCoroutine);
                attackCooldownCoroutine = null;
            }
            onAttackCooldown = false;
            rb.linearVelocity = Vector2.zero;
            DisableControllers();
        };
        RoomsManager.Instance.roomTransitionFinished += EnableControllers;
        PauseManager.Instance.pauseToggled += OnPause;
    }

    void OnPause(bool paused)
    {
        if (paused)
        {
            DisableControllers();
        }
        else
        {
            EnableControllers();
        }
    }

    void OnDestroy()
    {
        if (RoomsManager.Instance == null)
        {
            return;
        }
        RoomsManager.Instance.roomTransitionStarted -= DisableControllers;
        RoomsManager.Instance.roomTransitionFinished -= EnableControllers;
        Instance = null;
    }

    void DisableControllers()
    {
        // rb.linearVelocity = Vector2.zero;
        movementInput.Disable();
        attackInput.Disable();
    }

    void EnableControllers()
    {
        movementInput.Enable();
        attackInput.Enable();
    }

    void FixedUpdate()
    {
        if (attackInput.enabled == false)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 dir = Direction;
        rb.linearVelocity = movementSpeed * dir;
        if (dir.x > 0)
        {
            LookingLeft = false;
        }
        else if (dir.x < 0)
        {
            LookingLeft = true;
        }
        if(AttackPressed && onAttackCooldown == false)
        {
            ShootProjectile();
            if (attackCooldownCoroutine != null)
            {
                StopCoroutine(attackCooldownCoroutine);
            }
            attackCooldownCoroutine = CooldownTimer();
            StartCoroutine(attackCooldownCoroutine);
            // StartCoroutine(CooldownTimer());
        }
    }

    void ShootProjectile()
    {
        Projectile p = Instantiate(projectile);
        Vector2 cursorPositionInScreen = Mouse.current.position.ReadValue();
        Vector3 mousePositionInWorld = cam.ScreenToWorldPoint(cursorPositionInScreen);
        Vector2 mouseDirection = ((Vector2)(mousePositionInWorld - transform.position)).normalized;
        if (mouseDirection == Vector2.zero)
        {
            mouseDirection = Vector2.up;
        }
        p.transform.position = transform.position + projectileOffset * (Vector3)mouseDirection;
        // Debug.Log($"sem norm {mouseDirection} -- com norm {mouseDirection.normalized}");
        p.Shoot(mouseDirection, projectileSpeed, ProjectileDamage);
    }

    IEnumerator CooldownTimer()
    {
        onAttackCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onAttackCooldown = false;
    }
}
