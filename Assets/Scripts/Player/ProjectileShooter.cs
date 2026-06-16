using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ProjectileData projectileData;

    [Header("Gun")]
    [SerializeField] private Transform firePoint;

    private Animator animator;
    private float lastFireTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Debug.Log("Animator: " + animator);
        Debug.Log("Projectile Data: " + projectileData);
        Debug.Log("Pool Instance: " + ProjectilePool.Instance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (projectileData == null)
        {
            Debug.LogError("Projectile Data is NULL!");
            return;
        }

        if (ProjectilePool.Instance == null)
        {
            Debug.LogError("Projectile Pool Instance is NULL!");
            return;
        }

        if (Time.time < lastFireTime + projectileData.cooldown)
        {
            return;
        }

        lastFireTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        Projectile projectile =
            ProjectilePool.Instance.GetProjectile();

        if (projectile == null)
        {
            return;
        }

        // Spawn position
        if (firePoint != null)
        {
            projectile.transform.position =
                firePoint.position;
        }
        else
        {
            projectile.transform.position =
                transform.position;
        }

        // Shoot where player faces
        Vector2 shootDirection =
            transform.right;

        projectile.Initialize(
            projectileData,
            shootDirection);
    }
}