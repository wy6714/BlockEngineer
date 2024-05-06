using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTemp : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float healthDecreaseRate = 2f;
    public float healthIncreaseRate = 20f;

    private float currentHealth;
    private bool insideCandleLight = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (insideCandleLight)
        {
            IncreaseHealth(Time.deltaTime * healthIncreaseRate);
        }
        else
        {
            DecreaseHealth(Time.deltaTime * healthDecreaseRate);
        }

        if (currentHealth <= 0)
        {
            PlayerController scirpt = gameObject.GetComponent<PlayerController>();
            scirpt.callPlayerDie(gameObject);
        }
        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("candleLight"))
        {
            insideCandleLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("candleLight"))
        {
            insideCandleLight = false;
        }
    }

    private void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}
