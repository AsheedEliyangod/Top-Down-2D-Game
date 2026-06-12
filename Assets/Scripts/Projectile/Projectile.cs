using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileData projectileData;
    private Vector2 direction;

    private float distanceTravelled;
    private int enemiesHit;

    public void Initialize(ProjectileData data, Vector2 shootDirection)
    {
        projectileData = data;
        direction = shootDirection.normalized;

        // Reset pooled values
        distanceTravelled = 0f;
        enemiesHit = 0;

        transform.localScale = Vector3.one * projectileData.scale;

        SpriteRenderer spriteRenderer =
            GetComponent<SpriteRenderer>();

        spriteRenderer.color =
            projectileData.projectileColor;

        // Prevent old invokes from pooled objects
        CancelInvoke();

        // Return to pool after lifetime
        Invoke(nameof(ReturnToPool),
            projectileData.lifetime);
    }

    private void Update()
    {
        float moveDistance =
            projectileData.speed * Time.deltaTime;

        transform.Translate(
            direction * moveDistance,
            Space.World);

        distanceTravelled += moveDistance;

        if (distanceTravelled >= projectileData.range)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy =
            other.GetComponent<EnemyHealth>();

        if (enemy == null)
            return;

        enemy.TakeDamage(projectileData.damage);

        Vector2 knockbackDirection =
            (enemy.transform.position - transform.position)
            .normalized;

        enemy.ApplyKnockback(
            knockbackDirection *
            projectileData.knockback);

        enemiesHit++;

        // Pierce logic
        if (enemiesHit > projectileData.pierceCount)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ProjectilePool.Instance.ReturnProjectile(this);
    }
}