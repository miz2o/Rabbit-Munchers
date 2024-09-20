using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public Vector3 targetpoint;  // Target where the bomb should move
    public float speed;   // Speed of the bomb's movement
    public float arcHeight;// Maximum height of the arc
    public GameObject bombHitBox;

    private Vector3 startpoint;  // Starting position of the bomb
    public float duration;
    private float startTime;     // Time when the bomb started moving
    private bool destination;

    void Start()
    {
        startpoint = transform.position;
        destination = false;
        startTime = Time.time;
      GameObject hitbox =  Instantiate(bombHitBox);
        hitbox.transform.position = targetpoint;
    }

    void Update()
    {
        if (destination == false)
        {

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / duration;


            Vector3 currentPos = Vector3.Lerp(startpoint, targetpoint, fractionOfJourney);


            currentPos.y += Mathf.Sin(fractionOfJourney * Mathf.PI) * arcHeight;


            transform.position = currentPos;

            // Destroy the bomb once it reaches the target
            if (fractionOfJourney >= 1f)
            {
                destination = true;
            }
        }
        else
        {
            StartCoroutine(Waittillexplode());
        }
    }

    IEnumerator Waittillexplode()
    {
        

        yield return new WaitForSeconds(0.7f);

        Debug.Log("Bomb exploded!");
        Destroy(gameObject); // Destroy the bomb
    }
}