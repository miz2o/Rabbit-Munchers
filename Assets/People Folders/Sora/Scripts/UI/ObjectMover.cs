using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    // Array of waypoints (positions in world space)
    public Transform[] waypoints;
    public float speed = 5f;        // Speed at which the object moves
    public bool loop = true;        // If true, the object loops through the waypoints indefinitely

    private int currentWaypointIndex = 0;  // The index of the current waypoint the object is moving toward

    void Update()
    {
        // Ensure there are waypoints set
        if (waypoints.Length == 0) return;

        // Move the object towards the current waypoint
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Get the current waypoint position
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move the object towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the object has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // If the object has reached the final waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loop)
                {
                    // Start from the first waypoint again (loop)
                    currentWaypointIndex = 0;
                }
                else
                {
                    // Stop moving if looping is disabled
                    enabled = false;
                }
            }
        }
    }

    // Optional: Visualize the route in the editor using Gizmos
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Draw a line back to the first waypoint if looping
        if (loop)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }
    }
}
