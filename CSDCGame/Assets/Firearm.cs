using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public class Firearm : MonoBehaviour
{
    // Projectile/magazine properties
    public GameObject projectile { get; set; }
    public float speed { get; set; }
    public int capacity { get; set; }
    public int projectilesRemaining { get; set; }

    // Firing speed properties
    private double _fireCooldown = 0;
    // fireRate and fireCooldown are both based on the _fireCooldown field.
    /// <summary>
    /// Fire rate of the weapon in times fired per second.
    /// </summary>
    public double fireRate
    {
        get { return 1 / _fireCooldown; }
        set { _fireCooldown = 1 / value; }
    }
    /// <summary>
    /// Cooldown between firing the weapon in seconds.
    /// </summary>
    public double fireCooldown
    {
        get { return _fireCooldown; }
        set { _fireCooldown = value; }
    }
    // Other firing properties
    public FireType fireType { get; set; }


    public virtual void FixedUpdate()
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
    public virtual GameObject ShootForward()
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
