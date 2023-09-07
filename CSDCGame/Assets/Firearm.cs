using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Firearm : MonoBehaviour
{
    // Projectile/magazine properties
    public GameObject projectile { get; set; }
    public float speed { get; set; }
    public int capacity { get; set; }
    private int projectilesRemaining;

    // Firing properties
    public double fireCooldown { get; set; }
    public FireType fireType { get; set; }

// helper method 
public double RPMtoCooldown(double rpm) {
    return rpm / 60 / 60; 
}

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            ShootForward();
        }
    }

    /// <summary>
    /// Create a new projectile and fire it along the player's forward vector. Start at the player's origin.
    /// </summary>
    /// <param name="projectile">The projectile to instantiate.</param>
    /// <returns>The projectile fired.</returns>
    public GameObject ShootForward()
    {
        GameObject p = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        p.tag = "Projectile";
        Vector3 force = p.transform.forward;
        force.Scale(new Vector3(speed, speed, speed));
        p.GetComponent<Rigidbody>().AddForce(force);
        return p;
    }

    public enum FireType
    {
        Single,
        Burst3,
        Auto
    }
}
