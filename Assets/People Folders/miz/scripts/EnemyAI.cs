using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentpoint;
    public float speed;
    public int damage;

    private WaveHandler waveHandler; // Reference to WaveHandler

    // Start is called before the first frame update
    void Start()
    {
        waveHandler = FindObjectOfType<WaveHandler>(); // Find the WaveHandler instance

        if (waveHandler != null)
        {
            waypoints = waveHandler.waypoints; // Set waypoints from WaveHandler
        }
        else
        {
            Debug.LogError("WaveHandler not found in the scene.");
        }

        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position; // Set starting position
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentpoint < waypoints.Length)
        {
            Transform pointToGo = waypoints[currentpoint];
            transform.position = Vector3.MoveTowards(transform.position, pointToGo.position, speed * Time.deltaTime);

            // Use a small threshold for position comparison
            if (Vector3.Distance(transform.position, pointToGo.position) < 0.1f)
            {
                currentpoint += 1;
                if (currentpoint >= waypoints.Length)
                {
                    if (waveHandler != null)
                    {
                        waveHandler.EnemyDestroyed(damage); // Call EnemyDestroyed on the WaveHandler instance
                    }
                    Destroy(gameObject); // Destroy the enemy after calling EnemyDestroyed
                }
            }
        }
    }
}
