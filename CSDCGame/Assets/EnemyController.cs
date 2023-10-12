using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Do enemy spawning from this script. Enemies must be children of a GameObject
    // using this script in order function properly (this script is needed for
    // player tracking).

    public Transform playerTransform;
}
