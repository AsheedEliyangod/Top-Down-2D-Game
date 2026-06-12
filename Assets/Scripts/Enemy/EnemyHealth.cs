using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float currentHealth;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
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

        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyKnockback(Vector2 force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}