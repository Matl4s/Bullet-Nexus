using System.Linq.Expressions;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public HealthBar healthbar;
    private void Start()
    {
        currentHealth = maxHealth;

        healthbar.SetSliderMax(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthbar.SetSlider(currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        healthbar.SetSlider(currentHealth);
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f);
        }
    }

    private void PlayerDeath()
        {
            Debug.Log("You Died");
        }
    
}
