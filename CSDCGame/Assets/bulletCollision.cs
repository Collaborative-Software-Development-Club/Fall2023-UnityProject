using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision col) {
        Destroy(gameObject);
    }
}
