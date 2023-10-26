using System.Collections;
using UnityEngine;


public class Firearm : MonoBehaviour
{
    // Projectile/magazine properties
    public GameObject projectile;
    public int projectilesRemaining;
    [InspectorName("Projectile Lifetime (seconds)")]
    public float projectileLifetimeSecs;

    // Firing speed properties
    public float _fireCooldown = 0;
    // fireRate and fireCooldown are both based on the _fireCooldown field. Only _fireCooldown appears in the inspector.
    /// <summary>
    /// Fire rate of the weapon in times fired per second.
    /// </summary>
    public float fireRate
    {
        get { return 1 / _fireCooldown; }
        set { _fireCooldown = 1 / value; }
    }
    /// <summary>
    /// Cooldown between firing the weapon in seconds.
    /// </summary>
    public float fireCooldown
    {
        get { return _fireCooldown; }
        set { _fireCooldown = value; }
    }
    /// <summary>
    /// Track whether the firearm is in-between shots.
    /// </summary>
    private bool inFireCooldown = false;
    // Other firing properties
    public FireType fireType;

    // Reload speed properties
    public float reloadTime;
    /// <summary>
    /// Track whether the firearm is being reloaded.
    /// </summary>
    private bool inReloadCooldown = false;
    public AudioSource bulletSound;
    public AudioSource reload;
    private Weapon w;

    private struct Weapon {
        public float reloadTime;
        public float projectileSpeed;
        public int capacity;

    }

    void Start() {
        w = new Weapon();
        switch (fireType) {
            case FireType.Single:
            w.reloadTime = 1.25f;
            w.capacity = 7;
            w.projectileSpeed = 30;
            break;
            case FireType.Burst3:
            w.reloadTime = 2;
            w.capacity = 5;
            w.projectileSpeed = 10;
            break;
            case FireType.Auto:
            w.reloadTime = 1.5f;
            w.capacity = 20;
            w.projectileSpeed = 20;
            break;
            default:
            break;
        }
        WeaponStart();
    }

    /// <summary>
    /// Helper method to easily set the variables needed for the firearm to function.
    /// The projectile variable is omitted, but still necessary.
    /// </summary>
    // public virtual void GunSetup(float projectileSpeed, int capacity, int projectilesRemaining,
    //     float projectileLifetimeSecs, float fireCooldown, FireType fireType, float reloadTime)
    // {
    //     this.projectileSpeed = projectileSpeed;
    //     this.capacity = capacity;
    //     this.projectilesRemaining = projectilesRemaining;
    //     this.projectileLifetimeSecs = projectileLifetimeSecs;
    //     this.fireCooldown = fireCooldown;
    //     this.fireType = fireType;
    //     this.reloadTime = reloadTime;
    // }

    /// <summary>
    /// Helper method to easily set the variables needed for the firearm to function.
    /// </summary>
    // public virtual void GunSetup(float speed, int capacity, int projectilesRemaining,
    //     float projectileLifetimeSecs, float fireCooldown, FireType fireType, float reloadTime,
    //     GameObject projectile)
    // {
    //     GunSetup(speed, capacity, projectilesRemaining, projectileLifetimeSecs, fireCooldown, fireType, reloadTime);
    //     this.projectile = projectile;
    // }

// helper method 
public double RPMtoCooldown(double rpm) {
    return rpm / 60 / 60; 
}

    public virtual void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (!inFireCooldown && !inReloadCooldown && projectilesRemaining > 0)
            {
                // Results in default behavior of full-auto.
                StartCoroutine(FireCooldown());
                ShootForward();
                projectilesRemaining--;
                bulletSound.Play();
            }
        }
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Mouse1))
        {
            Reload();
            reload.Play();
        }
    }

    /// <summary>
    /// Coroutine for adding delay between shots.
    /// </summary>
    private IEnumerator FireCooldown()
    {
        inFireCooldown = true;
        yield return new WaitForSeconds(fireCooldown);
        inFireCooldown = false;
    }

    /// <summary>
    /// Create a new projectile and fire it along this object's parent's (typically the player) forward vector. Start at the this object's origin.
    /// </summary>
    /// <param name="projectile">The projectile to instantiate.</param>
    /// <returns>The projectile fired.</returns>
    public virtual GameObject ShootForward()
    {
        return Shoot(transform.parent.forward);
    }

    public virtual GameObject Shoot(Vector3 direction)
    {
        // Create new projectile with this object's position and rotation.
        GameObject p = Instantiate(projectile, transform.position, transform.rotation);
        // Because Quinton said so.
        p.tag = "Projectile";
        // Scale up direction vector to the projectile's speed.
        direction.Scale(new Vector3(w.projectileSpeed, w.projectileSpeed, w.projectileSpeed));
        // Set velocity of projectile.
        p.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
        // Set projectile to despawn after its lifetime is over (according to projectileLifetimeSecs).
        SetProjectileDespawn(p);
        // Return projectile created.
        return p;
    }

    public virtual void SetProjectileDespawn(GameObject projectile)
    {
        StartCoroutine(ProjectileDespawn(projectile));
    }

    private IEnumerator ProjectileDespawn(GameObject projectile)
    {
        yield return new WaitForSeconds(projectileLifetimeSecs);
        Destroy(projectile);
    }

    public virtual void Reload()
    {
        StartCoroutine(ReloadCooldown());
        // TODO: Notify subscribers that a reload started.
    }

    private IEnumerator ReloadCooldown()
    {
        inReloadCooldown = true;
        yield return new WaitForSeconds(reloadTime);
        inReloadCooldown = false;
        projectilesRemaining = w.capacity;
    }

    public enum FireType
    {
        Single,
        Burst3,
        Auto
    }
    void Update() {
        WeaponUpdate();
    }
    protected virtual void WeaponUpdate() {

    }
    protected virtual void WeaponStart() {
        
    }
}
