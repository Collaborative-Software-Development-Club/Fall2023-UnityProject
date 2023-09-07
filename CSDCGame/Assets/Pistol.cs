using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : Firearm
{

    // Projectile/magazine properties

    private int projectilesRemaining;

    protected override void WeaponStart() {
        Debug.Log("starting");
    }
    protected override void WeaponUpdate() {

    }
}
