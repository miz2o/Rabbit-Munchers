using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerAttacking : MonoBehaviour
{
    public float towerRadius;
    public float towerspeed;
    public int towerdamage;
    public GameObject rangepart;
 
    public bool placed;

    private string enemytag = "Enemy";
    private Collider currentTarget;

    public Camera mainCamera; 
    public string towertag = "Tower";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteEverySecond());
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTarget != null && currentTarget.GetComponent<EnemyAI>() == null)
        {
            currentTarget = null;
        }
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

    }



    public void Place()
    {
        placed = true;
        rangepart.SetActive(false);
    }




    IEnumerator ExecuteEverySecond()
    {
        while (true)
        {
            if (placed == true)
            {
                // If no current target, search for a new target
                if (currentTarget == null)
                {
                    Collider[] collidersInRange = Physics.OverlapSphere(transform.position, towerRadius);

                    foreach (Collider collider in collidersInRange)
                    {
                        if (collider.CompareTag(enemytag))
                        {
                            // Lock onto the first detected enemy
                            currentTarget = collider;
                            break; // Exit loop after finding the first valid enemy
                        }
                    }
                }

                // If we have a target
                if (currentTarget != null)
                {
                    // Check if the target is still within range
                    if (Vector3.Distance(transform.position, currentTarget.transform.position) <= towerRadius)
                    {
                        // Attack the enemy
                        if (currentTarget.GetComponent<EnemyAI>() != null)
                        {
                            currentTarget.GetComponent<EnemyAI>().getDamage(towerdamage);
                          
                        }
                    }
                    else
                    {
                        // Target left range, reset target
                      
                        currentTarget = null;
                    }
                }
            }

            yield return new WaitForSeconds(towerspeed); // Attack interval
        }
    }
}