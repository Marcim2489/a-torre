using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]Rigidbody2D rigidBody;
    [SerializeField]float detectionRange = 4f;
    [SerializeField]float impulseForce = 12f;
    [SerializeField]float deacceleration = 1f;
    [SerializeField]float moveCooldown = 0.8f;
    [SerializeField]Transform eye;
    [SerializeField]float eyeRange = 0.3f;
    [SerializeField]float eyeMoveFactor = 0.2f;
    [SerializeField]CharacterHealthManager healthManager;

    bool playerDetected = false;
    Transform player;
    Vector2 eyeInitialPosition;

    void Start()
    {
        player = PlayerController.Instance.transform;
        eyeInitialPosition = eye.localPosition;
        healthManager.tookDamage += DetectPlayer;
    }

    void Update()
    {
        rigidBody.linearVelocity = Vector2.Lerp(rigidBody.linearVelocity, Vector2.zero, deacceleration);

        float currentSpeed = rigidBody.linearVelocity.magnitude/(impulseForce*0.7f);

        eye.localPosition = Vector2.Lerp(eye.localPosition, eyeInitialPosition + eyeRange * rigidBody.linearVelocity.normalized * currentSpeed, eyeMoveFactor);
        eye.localPosition -= Vector3.forward;
        if (playerDetected == false)
        {
            if (Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                DetectPlayer();
            }
        }
    }

    void DetectPlayer()
    {
        if (playerDetected)
        {
            return;
        }
        playerDetected = true;
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(moveCooldown*0.9f, moveCooldown*1.1f));
        DashTowardsPlayer();
        StartCoroutine(DashCoroutine());
    }

    void DashTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rigidBody.linearVelocity = direction * impulseForce;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}