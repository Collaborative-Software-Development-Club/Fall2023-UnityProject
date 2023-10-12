using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [Header("Agent")]
    public NavMeshAgent agent;
    [Header("Agent Stats")]
    public float pathfindingDelay = 0.2f;
    public float targetShootingBound =  3f; // when target is within this bound, shoot
    public float targetRetreatBound = 3f; // when target gets to this value, retreat
    public float distanceBuffer = 0.2f;
    public float maxDistance = 15f;
    private float _updateDeadline = 0f;
    //[Header("Shooting")]
    //public Transform emissionPoint;
    //public GameObject bulletPrefab;
    //public float fireRateInSeconds = 0.5f;
    //public float bulletSpeed = 10f;
    //private bool _canShoot = true;
    public Firearm firearm;

    void Update() {
        float distanceToPlayer = Vector3.Distance(GetPlayerTransform().position, transform.position);
        Debug.DrawRay(transform.position, transform.forward, Color.white);
        if (distanceToPlayer <= targetShootingBound && CanSeePlayer()) {
            agent.isStopped = true;
            RotateToPlayer();
            // are we too close?
            if (distanceToPlayer <= targetRetreatBound) { // if so... retreat while shooting!
                agent.isStopped = false;
                Retreat();
            }
            firearm.ShootForward();
        }
        else { // if not, keep moving towards player
            agent.isStopped = false;
            UpdatePath(GetPlayerTransform().position);
        }
    }

    private Transform GetPlayerTransform()
    {
        return transform.parent.GetComponent<EnemyController>().playerTransform;
    }

    /// <summary>
    /// Updates the agent's intended destination
    /// </summary>
    /// <param name="position"> The position to move towards </param>
    private void UpdatePath(Vector3 position) {
        if (Time.time >= _updateDeadline) {
            //Debug.Log("Updating path...");
            _updateDeadline = Time.time + pathfindingDelay;
            agent.SetDestination(position);
        }
    }

    /// <summary>
    /// Rotates agent towards the player
    /// </summary>
    private void RotateToPlayer() {
        Vector3 vectorToPlayer = transform.position - GetPlayerTransform().position;
        float angle = XZRotationConversion.Vector3ToAngle(vectorToPlayer);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -angle + 180f, transform.rotation.eulerAngles.z);
    }

    /// <summary>
    /// Checks if the agent can "see" the player through raycasts
    /// </summary>
    /// <returns> true if agent has line of sight, false otherwise </returns>
    private bool CanSeePlayer() {
        bool canSee = false;
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = GetPlayerTransform().position - transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag.Equals("Player")) canSee = true;
        }
        return canSee;
    }

    /// <summary>
    /// Allows the agent to retreat away from the player's forward vector
    /// </summary>
    private void Retreat() {
        Vector3 pointToCheck; // The point you want to check
        Vector3 forwardUnit = GetPlayerTransform().forward * distanceBuffer;
        Vector3 retreatPosition = transform.position;
        pointToCheck.y = 0.12f;
        NavMeshHit hit;
        for (int f = 0; f < maxDistance; f++) {
            pointToCheck.x = transform.position.x + (forwardUnit.x + (forwardUnit.x * f));
            pointToCheck.z = transform.position.z + (forwardUnit.z + (forwardUnit.z * f));
            if (NavMesh.SamplePosition(pointToCheck, out hit, 0.05f, NavMesh.AllAreas)) {
            retreatPosition = hit.position;
            }
        }
        Debug.DrawRay(transform.position, retreatPosition, Color.magenta);
        UpdatePath(retreatPosition);
    }
}
public static class XZRotationConversion { // angle-vector conversion nonsense because im tired of doing it 20,000 times
    public static Vector3 AngleToVector3(float angleInDegrees, float offsetInDegrees=0) {
        return new Vector3(Mathf.Cos((angleInDegrees + offsetInDegrees) * Mathf.Deg2Rad), 0, Mathf.Sin((angleInDegrees + offsetInDegrees) * Mathf.Deg2Rad));
    }
    public static float Vector3ToAngle(Vector3 vector) {
        return Mathf.Atan2(vector.z, vector.x) * Mathf.Rad2Deg - 90f;
    }
}
