using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth = 100;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            LevelManager.instance.LevelLost();
        }
    }
}
