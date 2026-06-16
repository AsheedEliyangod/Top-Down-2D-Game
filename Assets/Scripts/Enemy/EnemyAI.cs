using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Vision")]
    [SerializeField] private float viewDistance = 6f;
    [SerializeField] private float viewAngle = 90f;

    [Header("Obstacle Detection")]
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolWaitTime = 1f;

    [Header("Search")]
    [SerializeField] private float searchDuration = 3f;

    [Header("Attack")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private Animator animator;

    private int currentPatrolIndex;

    private float waitTimer;
    private float attackTimer;

    private bool searching;

    private Vector2 lastKnownPlayerPosition;
    private Vector2 facingDirection = Vector2.down;

    // Unstuck system
    private float stuckTimer;
    private Vector2 lastPosition;

    private enum EnemyState
    {
        Patrol,
        Chase,
        Search,
        Attack
    }

    private EnemyState currentState =
        EnemyState.Patrol;

    private void Awake()
    {
        animator =
            GetComponent<Animator>();

        enemyHealth =
            GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag(
                "Player");

        if (playerObj != null)
        {
            player =
                playerObj.transform;

            playerHealth =
                playerObj.GetComponent<PlayerHealth>();
        }

        lastPosition =
            transform.position;
    }

    private void Update()
    {
        if (enemyHealth != null &&
            enemyHealth.IsDead)
        {
            return;
        }

        if (player == null)
        {
            return;
        }

        bool canSeePlayer =
            CanSeePlayer();

        float distance =
            Vector2.Distance(
                transform.position,
                player.position);

        if (canSeePlayer)
        {
            lastKnownPlayerPosition =
                player.position;

            searching = false;

            if (distance <= attackRange)
            {
                currentState =
                    EnemyState.Attack;
            }
            else
            {
                currentState =
                    EnemyState.Chase;
            }
        }
        else
        {
            if ((currentState ==
                EnemyState.Chase ||

                currentState ==
                EnemyState.Attack)

                && !searching)
            {
                currentState =
                    EnemyState.Search;

                StartCoroutine(
                    SearchRoutine());
            }
        }

        switch (currentState)
        {
            case EnemyState.Patrol:

                Patrol();

                break;

            case EnemyState.Chase:

                ChasePlayer();

                break;

            case EnemyState.Search:

                SearchPlayer();

                break;

            case EnemyState.Attack:

                AttackPlayer();

                break;
        }

        CheckIfStuck();
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        Transform target =
            patrolPoints[
            currentPatrolIndex];

        Vector2 direction =
            (target.position -
            transform.position)
            .normalized;

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                target.position,
                moveSpeed *
                Time.deltaTime);

        UpdateAnimation(
            direction);

        if (Vector2.Distance(
            transform.position,
            target.position)
            < 0.1f)
        {
            animator.SetBool(
                "IsMoving",
                false);

            waitTimer +=
                Time.deltaTime;

            if (waitTimer >=
                patrolWaitTime)
            {
                waitTimer = 0;

                currentPatrolIndex++;

                if (currentPatrolIndex >=
                    patrolPoints.Length)
                {
                    currentPatrolIndex = 0;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction =
            (player.position -
            transform.position)
            .normalized;

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed *
                Time.deltaTime);

        UpdateAnimation(
            direction);
    }

    private void SearchPlayer()
    {
        Vector2 direction =
            (lastKnownPlayerPosition -
            (Vector2)
            transform.position)
            .normalized;

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                lastKnownPlayerPosition,
                moveSpeed *
                Time.deltaTime);

        UpdateAnimation(
            direction);

        if (Vector2.Distance(
            transform.position,
            lastKnownPlayerPosition)
            < 0.1f)
        {
            animator.SetBool(
                "IsMoving",
                false);
        }
    }

    private void AttackPlayer()
    {
        animator.SetBool(
            "IsMoving",
            false);

        attackTimer +=
            Time.deltaTime;

        if (attackTimer >=
            attackCooldown)
        {
            attackTimer = 0;

            animator.ResetTrigger(
                "Attack");

            animator.SetTrigger(
                "Attack");

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(
                    attackDamage);
            }
        }
    }

    private IEnumerator SearchRoutine()
    {
        searching = true;

        yield return new WaitForSeconds(
            searchDuration);

        searching = false;

        currentState =
            EnemyState.Patrol;
    }

    private bool CanSeePlayer()
    {
        Vector2 direction =
            player.position -
            transform.position;

        float distance =
            direction.magnitude;

        if (distance >
            viewDistance)
        {
            return false;
        }

        float angle =
            Vector2.Angle(
                facingDirection,
                direction);

        if (angle >
            viewAngle / 2)
        {
            return false;
        }

        RaycastHit2D hit =
            Physics2D.Raycast(
                transform.position,
                direction.normalized,
                distance,
                obstacleLayer);

        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    private void CheckIfStuck()
    {
        if (currentState ==
            EnemyState.Chase ||

            currentState ==
            EnemyState.Search)
        {
            float movedDistance =
                Vector2.Distance(
                    transform.position,
                    lastPosition);

            if (movedDistance < 0.02f)
            {
                stuckTimer +=
                    Time.deltaTime;
            }
            else
            {
                stuckTimer = 0f;
            }

            if (stuckTimer >= 1f)
            {
                currentState =
                    EnemyState.Patrol;

                searching = false;

                stuckTimer = 0f;
            }

            lastPosition =
                transform.position;
        }
    }

    private void UpdateAnimation(
        Vector2 direction)
    {
        if (direction.magnitude >
            0.1f)
        {
            facingDirection =
                direction.normalized;
        }

        animator.SetBool(
            "IsMoving",

            direction.magnitude >
            0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color =
            Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            viewDistance);
    }
}