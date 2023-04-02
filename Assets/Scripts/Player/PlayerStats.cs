using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance { get; set; }

    public int startingHealth = 100;
    private int currentHealth = 100;
    public AudioClip playerHurtSFX;
    public AudioClip playerHealSFX;
    public AudioClip playerDeadSFX;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(playerHurtSFX, transform.position);
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeadSFX, transform.position);
            LevelManager.instance.LevelLost();
        }
    }

    public void Heal(int health)
    {
        AudioSource.PlayClipAtPoint(playerHealSFX, transform.position);
        currentHealth += health;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        Debug.Log("Health: " + currentHealth);
    }
    
    public int CurrentHealth { 
        get {
            return this.currentHealth;
        } 
    }
}
