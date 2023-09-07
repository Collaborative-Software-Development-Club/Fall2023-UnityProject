using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementType {
        IsoMovement, NormalMovement
    }
    public MovementType moveType;
    public Rigidbody playerRB;
    public float speed;
    private Vector3 _playerMovement;
    private Quaternion rotationQuaternion;

    void Start() {
        if (moveType == MovementType.IsoMovement) rotationQuaternion = Quaternion.Euler(0,45f, 0);
        else rotationQuaternion = Quaternion.Euler(0,0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.white);
        playerRB.velocity = IsoMatrixHelper.QuaternionToVector(rotationQuaternion, _playerMovement) * speed;
    }
    void FixedUpdate() {
        // "Vertical" and "Horizontal"
        _playerMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
public static class IsoMatrixHelper {
    public static Vector3 QuaternionToVector(Quaternion quaternion, Vector3 input) {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(quaternion);
        return rotationMatrix.MultiplyPoint3x4(input);
    }
}
    