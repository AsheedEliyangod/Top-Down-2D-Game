using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private ProjectileData projectileData;

    private float lastFireTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time < lastFireTime + projectileData.cooldown)
            return;

        lastFireTime = Time.time;

        Projectile projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity);

        projectile.Initialize(
            projectileData,
            Vector2.right);
    }
}