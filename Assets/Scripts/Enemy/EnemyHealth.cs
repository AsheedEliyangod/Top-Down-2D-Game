using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float currentHealth;
    private Color originalColor;

    public bool IsDead { get; private set; }

    private Animator animator;
    private EnemyAI enemyAI;

    private void Start()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
            spriteRenderer =
                GetComponent<SpriteRenderer>();

        animator =
            GetComponent<Animator>();

        enemyAI =
            GetComponent<EnemyAI>();

        originalColor =
            spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        if (!IsDead)
        {
            spriteRenderer.color =
                originalColor;
        }
    }

    private void Die()
    {
        IsDead = true;

        // Stop AI completely
        if (enemyAI != null)
            enemyAI.enabled = false;

        // Stop physics
        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity =
                Vector2.zero;

            rb.simulated = false;
        }

        // Play death animation
        if (animator != null)
        {
            animator.SetBool(
                "Dead",
                true);
        }

        // Destroy after animation
        Destroy(gameObject, 1.2f);
    }

    public void ApplyKnockback(
        Vector2 force)
    {
        if (IsDead)
            return;

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(
                force,
                ForceMode2D.Impulse);
        }
    }
}