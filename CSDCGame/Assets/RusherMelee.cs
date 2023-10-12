using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusherMelee : Firearm
{
    public void Start()
    {
        GunSetup(100f, int.MaxValue, int.MaxValue, 0.3f, 1.5f, FireType.Auto, 0f);
    }

    // Intentionally empty. Overriding removes fire-on-click behavior.
    public override void FixedUpdate() { }
}
