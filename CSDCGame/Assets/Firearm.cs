using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Firearm : MonoBehaviour
{
    // Projectile/magazine properties
    public GameObject projectile { get; set; }
    public double speed { get; set; }
    public int capacity { get; set; }
    private int projectilesRemaining;

    // Firing properties
    public double fireCooldown { get; set; }
    public FireType fireType { get; set; }


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
        p.GetComponent<Rigidbody>().;
        return p;
    }

    public enum FireType
    {
        Single,
        Burst3,
        Auto
    }
}
