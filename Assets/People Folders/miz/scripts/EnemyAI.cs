using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentpoint;
    public float speed;
    public int damage;
    public int health;
    
    public ParticleSystem damageEffect;
    public int enemworth;
    public GameObject healthCanvas;
    private bool isDead = false;
    public Image enemyhp;

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

    public void getDamage(int loss)
    {
        if (isDead) return;
        damageEffect.Play();
        health -= loss;
        if(health <= 0)
        {
            isDead = true;
            waveHandler.EnemyKilled(enemworth);
            Destroy(gameObject);
           

        }
    }
    // Update is called once per frame
    void Update()
    {
        healthCanvas.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        enemyhp.fillAmount = health / 100f;
      
        if (currentpoint < waypoints.Length)
        {
            Transform pointToGo = waypoints[currentpoint];
            // Move towards the next waypoint
            Vector3 targetPos = new Vector3(pointToGo.position.x, 0.5f , pointToGo.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);





            Vector3 direction = pointToGo.transform.position - transform.position;
            direction.y = 0;
       
            transform.rotation = Quaternion.LookRotation(direction);

            // Use a small threshold for position comparison
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(pointToGo.position.x, pointToGo.position.z)) < 0.1f)
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
