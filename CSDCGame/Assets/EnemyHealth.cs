using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float damageCooldown = 1f;
    private float lastDamageTime;
    void Start()
    {
        currentHealth = maxHealth;
        lastDamageTime = -damageCooldown;
    }
    public void TakeDamage(int damageAmount)
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth -= damageAmount;
            lastDamageTime = Time.time;
            Debug.Log("Damaged enemy, " + currentHealth + " health left");
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Projectile")
        {
            TakeDamage(1);
        }
    }
}
