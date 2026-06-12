using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Detection")]
    [SerializeField] private LayerMask detectionMask;

    private Transform player;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector2 direction =
            (player.position - transform.position).normalized;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position);

        RaycastHit2D hit =
            Physics2D.Raycast(
                transform.position,
                direction,
                distance,
                detectionMask);

        Debug.DrawRay(
            transform.position,
            direction * distance,
            Color.red);

        if (hit.collider == null)
            return;

        if (hit.collider.CompareTag("Player"))
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime);
        }
    }
}