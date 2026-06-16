using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Slider healthSlider;

    [SerializeField] private Image fillImage;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth =
                FindFirstObjectByType<PlayerHealth>();
        }

        healthSlider.maxValue =
            playerHealth.GetMaxHealth();

        healthSlider.value =
            playerHealth.GetCurrentHealth();
    }

    private void Update()
    {
        if (playerHealth == null)
            return;

        float currentHealth =
            playerHealth.GetCurrentHealth();

        float maxHealth =
            playerHealth.GetMaxHealth();

        healthSlider.value =
            currentHealth;

        float healthPercent =
            currentHealth / maxHealth;

        if (healthPercent > 0.6f)
        {
            fillImage.color =
                Color.green;
        }
        else if (healthPercent > 0.3f)
        {
            fillImage.color =
                Color.yellow;
        }
        else
        {
            fillImage.color =
                Color.red;
        }
    }
}