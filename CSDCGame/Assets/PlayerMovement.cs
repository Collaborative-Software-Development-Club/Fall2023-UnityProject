using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement / Aiming")]
    public Rigidbody rb;
    public float movementSpeed = 5f;
    public enum MovementType {
        Normal, IsoAdjusted
    }
    public MovementType moveType; // use normal for debug, iso-adjust for default movement
    private Vector3 _inputVector;
    private RaycastHit _aimData;
    void Update()
    {
        // move
            ProcessMovement(moveType);

        // aim
            LookAt();
        

        // debugging
            Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }
    void FixedUpdate() {
        _inputVector = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    /// <summary>
    /// Handle isometric movement via matrix multiplication on a rotational iso matrix.
    /// </summary>
    /// <param name="type"> The enum movement type </param>
    private void ProcessMovement(MovementType type) {
        if (_inputVector.magnitude != 0) {
        switch (moveType) {
            case MovementType.Normal: // normal movement

            rb.velocity = new Vector3(_inputVector.x * movementSpeed, 0f, _inputVector.z * movementSpeed);
            
            break;
            case MovementType.IsoAdjusted: // moves at 45 degree angle
           
            Vector3 skewedMove = IsoMatrixHelper.GetNormalizedIsoInputVector(_inputVector);
            rb.velocity = skewedMove * movementSpeed;

            break;
        }
        }
        else {
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Rotate player if mouse lands on a viable spot. Do nothing otherwise.
    /// </summary>
    private void LookAt() {
        if (getRecentAimData())
        {
            Vector3 rayDirection = _aimData.point - transform.position; // returns the direction fo the ray; essentially the mouse position.
            float angle = Mathf.Atan2(rayDirection.z, rayDirection.x) * Mathf.Rad2Deg - 90f;
            //Debug.Log("The angle is " + angle);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -angle, transform.rotation.eulerAngles.z);
            
            Debug.DrawRay(transform.position, rayDirection, Color.green);
        }
    }

    /// <summary>
    /// Retrieve information on raycasts projected from mouse position.
    /// </summary>
    /// <returns> True if ray hits object. False otherwise. </returns>
    private bool getRecentAimData() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _aimData))
        {
            return true;
        }
        else return false;
    }
}

/// <summary>
/// Helper class for matrix stuff.
/// </summary>
public static class IsoMatrixHelper {
    private static Matrix4x4 _rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0)); // create 45 degree rotation matrix
    public static Vector3 ToIso(Vector3 input) => _rotationMatrix.MultiplyPoint3x4(input); // multiply rotation by current transform

    public static Vector3 GetNormalizedIsoInputVector(Vector3 input) {
        return input == Vector3.zero? Vector3.zero : ToIso(input) / ToIso(input).magnitude;
    }
}

/// <summary>
/// Helper class for debugging player features.
/// </summary>
public static class DebugPlayer { // debugging class
    public static void DrawAtAngle(Vector3 playerPosition, float angle, Vector3 referenceRay) {
        Vector3 angularVector = new Vector3(Mathf.Cos((angle + 90f) * Mathf.Deg2Rad) * referenceRay.magnitude, referenceRay.y, Mathf.Sin((angle + 90f) * Mathf.Deg2Rad) * referenceRay.magnitude);
        Debug.DrawRay(playerPosition, angularVector, Color.cyan);
    }
}
    