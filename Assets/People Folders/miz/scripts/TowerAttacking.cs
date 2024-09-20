using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerAttacking : MonoBehaviour
{
    public float towerRadius;
    public float towerspeed;
    public int towerdamage;
    public GameObject rangepart;
    public int towercost;
    public AudioSource attackaudio;
    public AudioSource spawnaudio;
    public Animator animator;
    public float throwspeed;



    public bool placed;

    private string enemytag = "Enemy";
    private Collider currentTarget;

    public Camera mainCamera; 
    public string towertag = "Tower";
    public GameObject item;
    public int towerIndex;


    // Start is called before the first frame update
    void Start()
    {
        spawnaudio.Play();
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
        else
        {
            animator.ResetTrigger("Attacking");
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



                        animator.SetTrigger("Attacking");


                        int randomnumber = UnityEngine.Random.Range(1, 100);
                        if (randomnumber == 1)
                        {
                            spawnaudio.Play();
                        }

                        if (item != null && currentTarget != null)
                        {


                            if (towerIndex == 1)
                            {
                                print("Getting ready to throw");

                                // Instantiate the item and set its position to the tower's current position
                                GameObject toThrow = Instantiate(item, transform.position, Quaternion.identity);

                                // Coroutine to move the instantiated item towards the enemy
                                StartCoroutine(MoveItemTowardsTarget(toThrow, currentTarget));
                                attackaudio.Play();
                            }

                            if (towerIndex == 2)
                            {
                                print("Getting ready to throw");
                                yield return new WaitForSeconds(0.7f);
                                ///chatgpt please ass a .7 second delay here

                                // Instantiate the item and set its position to the tower's current position
                                GameObject toThrow = Instantiate(item, transform.position, Quaternion.identity);
                                if (toThrow.GetComponent<BombHandler>())
                                {
                                    BombHandler bombHandler = toThrow.GetComponent<BombHandler>();

                                    // Set the target point to the enemy's position or some calculated point
                                    bombHandler.targetpoint = currentTarget.transform.position; // Example: target the current enemy's position
                                }
                                attackaudio.Play();
                            }
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
    IEnumerator MoveItemTowardsTarget(GameObject item, Collider target)
    {
        while (item != null && target != null && Vector3.Distance(item.transform.position, target.transform.position) > 0.1f)
        {
           
            var step = throwspeed * Time.deltaTime;
            item.transform.position = Vector3.MoveTowards(item.transform.position, target.transform.position, step);
            yield return null;
        }

        if (item != null)
        {
            Destroy(item); 
        }
        if (currentTarget != null && currentTarget.GetComponent<EnemyAI>() != null)
        {
            currentTarget.GetComponent<EnemyAI>().getDamage(towerdamage);

         
        }
        




    }
}