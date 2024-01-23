using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float currentHealth;
    public float maxHealth;
    public Slider healthSlider;
    public GameObject deathEffect;
    
    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            // Game Over
            gameObject.SetActive(false);
            LevelManager.instance.EndLevel();
            Instantiate(deathEffect, transform.position, transform.rotation);
            SFXManager.instance.PlaySFX(3);
        }

        healthSlider.value = currentHealth;
    }
}
