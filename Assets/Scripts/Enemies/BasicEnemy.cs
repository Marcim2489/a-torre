using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]Projectile projectilePrefab;
    [SerializeField]Rigidbody2D rigidBody;
    [SerializeField]float detectionRange = 4f;
    [SerializeField]float proximityRange = 2f;
    [SerializeField]float moveSpeed = 8f;
    [SerializeField]float attackCooldown = 1.1f;
    [SerializeField]float projectileSpeed = 5f;
    [SerializeField]float projectileReleaseOffset = 1.3f;
    [SerializeField]int projectileDamage = 10;

    Transform player;
    bool playerDetected = false;

    float playerDistance;

    IEnumerator attackCoroutine;

    void Start()
    {
        player = PlayerController.Instance.transform;
    }

    void Update()
    {
        playerDistance = Vector2.Distance(player.position, transform.position);
        if (playerDetected == false)
        {
            if(playerDistance > detectionRange)
            {
                return;
            }
            else
            {
                playerDetected = true;
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    attackCoroutine = null;
                }
                attackCoroutine = AttackRoutine();
                StartCoroutine(attackCoroutine);
            }
        }

        if(playerDistance > proximityRange)
        {
            rigidBody.linearVelocity = moveSpeed * (player.position - transform.position).normalized;
        }
        else
        {
            rigidBody.linearVelocity = Vector2.zero;
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        ThrowProjectile();
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        attackCoroutine = AttackRoutine();
        StartCoroutine(attackCoroutine);
    }

    void ThrowProjectile()
    {
        Projectile p = Instantiate(projectilePrefab);
        Vector2 playerDirection = (player.position - transform.position).normalized;
        p.transform.position = transform.position + (Vector3)(projectileReleaseOffset*playerDirection);
        p.Shoot(playerDirection, projectileSpeed, projectileDamage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}
