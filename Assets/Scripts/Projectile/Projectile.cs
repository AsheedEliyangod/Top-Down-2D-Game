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

      transform.localScale = Vector3.one * projectileData.scale;

      SpriteRenderer spriteRenderer =
          GetComponent<SpriteRenderer>();

      spriteRenderer.color =
          projectileData.projectileColor;

      Destroy(gameObject, projectileData.lifetime);
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       EnemyHealth enemy = other.GetComponent<EnemyHealth>();

       if (enemy == null)
          return;

       enemy.TakeDamage(projectileData.damage);

       Vector2 knockbackDirection =
          (enemy.transform.position - transform.position).normalized;

       enemy.ApplyKnockback(
          knockbackDirection * projectileData.knockback);

       enemiesHit++;

       if (enemiesHit > projectileData.pierceCount)
       {
         Destroy(gameObject);
       }
    }
   
}