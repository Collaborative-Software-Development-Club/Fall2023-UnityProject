using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    public float speed;
    private Vector3 _playerMovement;

    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = _playerMovement * speed;
    }
    void FixedUpdate() {
        // "Vertical" and "Horizontal"
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _playerMovement = new Vector3(x, 0, z);
    }
}
