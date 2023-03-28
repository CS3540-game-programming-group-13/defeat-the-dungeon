using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth = 100;
    public AudioClip playerHurtSFX;
    public AudioClip playerDeadSFX;

    void Start()
    {
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
}
