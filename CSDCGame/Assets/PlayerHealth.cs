using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // initial health
    [HideInInspector] public int currentHealth; // health changes if taken damage

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            //Debug.Log("Player took damage. Current health: " + currentHealth);
        }

    }

    private void GameOver()
    {
        Debug.Log("Game Over - Player's health reached zero!");
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Projectile")
        {
            TakeDamage(1);
        }
    }
    
}
