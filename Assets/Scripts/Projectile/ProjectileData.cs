using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData",
menuName = "Game/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Header("Behaviour")]
    public float damage = 10f;
    public float speed = 10f;
    public float range = 10f;
    public float cooldown = 1f;
    public int pierceCount = 0;
    public float knockback = 0f;
    public float lifetime = 5f;

    [Header("Visual")]
    public Color projectileColor = Color.red;
    public float scale = 1f;
}