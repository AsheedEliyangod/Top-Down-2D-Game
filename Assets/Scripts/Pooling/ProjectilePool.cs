using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<Projectile> pool =
        new Queue<Projectile>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            Projectile projectile =
                Instantiate(projectilePrefab);

            projectile.gameObject.SetActive(false);

            pool.Enqueue(projectile);
        }
    }

    public Projectile GetProjectile()
    {
        if (pool.Count == 0)
        {
            return null;
        }

        Projectile projectile =
            pool.Dequeue();

        projectile.gameObject.SetActive(true);

        return projectile;
    }

    public void ReturnProjectile(
        Projectile projectile)
    {
        projectile.CancelInvoke();

        projectile.gameObject.SetActive(false);

        pool.Enqueue(projectile);
    }
}