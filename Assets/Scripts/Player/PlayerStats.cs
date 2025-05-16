using System;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public HealthBar healthbar;
    public static event Action WhenPlayerDie;
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
            currentHealth = 0;
            Debug.Log("You Died");
            // WhenPlayerDie = event
            //? zaručuje, že se akce zavolá jenom když WhenPlayerDie není null
            // invoke = spustí všechny funkce, které jsou přiřazené tomuto eventu
            WhenPlayerDie?.Invoke();
        }
    
}
