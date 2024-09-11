using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentpoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentpoint < waypoints.Length)
        {
            Transform pointToGo = waypoints[currentpoint];
            transform.position = Vector3.MoveTowards(transform.position, pointToGo.position, speed * Time.deltaTime);


            if (transform.position == pointToGo.position)
            {
                currentpoint += 1;


            }
        }
    }
}
