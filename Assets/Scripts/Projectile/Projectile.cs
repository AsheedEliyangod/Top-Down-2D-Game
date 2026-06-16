using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileData projectileData;
    private Vector2 direction;

    private float distanceTravelled;
    private int enemiesHit;

    public void Initialize(
        ProjectileData data,
        Vector2 shootDirection)
    {
        projectileData = data;

        direction =
            shootDirection.normalized;

        // Reset values
        distanceTravelled = 0f;
        enemiesHit = 0;

        transform.localScale =
            Vector3.one *
            projectileData.scale;

        SpriteRenderer spriteRenderer =
            GetComponent<SpriteRenderer>();

        spriteRenderer.color =
            projectileData.projectileColor;

        // Prevent old Invoke calls
        CancelInvoke();

        // Lifetime
        Invoke(
            nameof(ReturnToPool),
            projectileData.lifetime);
    }

    private void Update()
    {
        float moveDistance =
            projectileData.speed *
            Time.deltaTime;

        transform.Translate(
            direction * moveDistance,
            Space.World);

        distanceTravelled +=
            moveDistance;

        // Maximum range
        if (distanceTravelled >=
            projectileData.range)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other)
    {
        // ---------- Enemy ----------
        EnemyHealth enemy =
            other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(
                projectileData.damage);

            Vector2 knockbackDirection =
                (enemy.transform.position
                - transform.position)
                .normalized;

            enemy.ApplyKnockback(
                knockbackDirection *
                projectileData.knockback);

            enemiesHit++;

            // Pierce logic
            if (enemiesHit >
                projectileData.pierceCount)
            {
                ReturnToPool();
            }

            return;
        }

        // ---------- Walls ----------
        if (other.CompareTag("Wall"))
        {
            ReturnToPool();
            return;
        }

        // ---------- Obstacles ----------
        if (other.CompareTag("Obstacle"))
        {
            ReturnToPool();
            return;
        }
    }

    private void ReturnToPool()
    {
        CancelInvoke();

        ProjectilePool.Instance
            .ReturnProjectile(this);
    }
}