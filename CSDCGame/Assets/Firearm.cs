using System.Collections;
using TreeEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows;


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
    public GameObject shotgunEmitter1;
    public GameObject shotgunEmitter2;
    [HideInInspector] public int globalCapacity;
    private Weapon w;
    private bool keyDown = false;

    private struct Weapon {
        public float reloadTime;
        public float projectileSpeed;
        public int capacity;
        public float cooldown;

    }

    void Start() {
        w = new Weapon();
        switchGunProperties(fireType);
    }

// helper method 
public double RPMtoCooldown(double rpm) {
    return rpm / 60 / 60; 
}

private void switchGunProperties(FireType type) {
    switch (type) {
            case FireType.Single:
            fireType = FireType.Single;
            w.reloadTime = 1.25f;
            w.capacity = 7;
            w.projectileSpeed = 30;
            w.cooldown = 0.5f;
            globalCapacity = w.capacity;
            projectilesRemaining = w.capacity;
            break;
            case FireType.Burst3:
            fireType = FireType.Burst3;
            w.reloadTime = 2;
            w.capacity = 5;
            w.projectileSpeed = 10;
            w.cooldown = 1;
            globalCapacity = w.capacity;
            projectilesRemaining = w.capacity;
            break;
            case FireType.Auto:
            fireType = FireType.Auto;
            w.reloadTime = 2f;
            w.capacity = 20;
            w.projectileSpeed = 20;
            w.cooldown = 0.2f;
            globalCapacity = w.capacity;
            projectilesRemaining = w.capacity;
            break;
            default:
            break;
        }
}

void Update() {
    keyDown = UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
    if (fireType == FireType.Auto && UnityEngine.Input.GetKey(KeyCode.Mouse0)) keyDown = true;
    if(keyDown)
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
        if (UnityEngine.Input.GetKey(KeyCode.R) || UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            Reload();
            reload.Play();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.E)) {
            reload.Play();
            if (fireType == FireType.Single) {
                switchGunProperties(FireType.Burst3);
            }
            else if (fireType == FireType.Burst3) {
                switchGunProperties(FireType.Auto);
            }
            else {
                switchGunProperties(FireType.Single);
            }
        }
}

    /// <summary>
    /// Coroutine for adding delay between shots.
    /// </summary>
    private IEnumerator FireCooldown()
    {
        inFireCooldown = true;
        yield return new WaitForSeconds(w.cooldown);
        inFireCooldown = false;
    }

    /// <summary>
    /// Create a new projectile and fire it along this object's parent's (typically the player) forward vector. Start at the this object's origin.
    /// </summary>
    /// <param name="projectile">The projectile to instantiate.</param>
    /// <returns>The projectile fired.</returns>
    public void ShootForward()
    {
        Shoot(transform.parent.forward, transform.position);
        if (fireType == FireType.Burst3) {
            Vector2 parentV2 = new Vector2(transform.parent.forward.x, transform.parent.forward.z);
            Shoot(RotateVector(parentV2, -10), shotgunEmitter2.transform.position);
            Shoot(RotateVector(parentV2, 10), shotgunEmitter1.transform.position);
        }
    }

    public virtual GameObject Shoot(Vector3 direction, Vector3 position)
    {
        // Create new projectile with this object's position and rotation.
        GameObject p = Instantiate(projectile, position, Quaternion.identity);
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

    Vector3 RotateVector(Vector2 vector, float degrees)
    {
        // Convert degrees to radians
        float radians = degrees * Mathf.Deg2Rad;

        // Perform the rotation using trigonometric functions
        float newX = vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians);
        float newY = vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians);

        return new Vector3(newX, 0, newY);
    }
}