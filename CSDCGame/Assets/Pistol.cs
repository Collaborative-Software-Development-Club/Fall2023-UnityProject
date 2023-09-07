using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets

public class Pistol : Firearm, MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        capacity = 16; 
        fireCooldown = 180 / 60 / 60; 
    }

    // Projectile/magazine properties

    private int projectilesRemaining;

    // Firing properties
    public double fireCooldown { get; set; }
    
    public FireType fireType { get; set; }


    // Update is called once per frame
    void Update()
    {
        
    }
}
