using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision col) {
        if (col.transform.tag != "Projectile")
        Destroy(gameObject);
    }
}
