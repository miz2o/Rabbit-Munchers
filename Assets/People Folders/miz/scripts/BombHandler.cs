using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.VFX;

public class BombHandler : MonoBehaviour
{
    public Vector3 targetpoint;  // Target where the bomb should move
    public float speed;   // Speed of the bomb's movement
    public float arcHeight;// Maximum height of the arc
    public GameObject bombHitBox;
    private GameObject hitbox = null;
    public bool devil;

    private Vector3 startpoint;  // Starting position of the bomb
    public float duration;
    private float startTime;     // Time when the bomb started moving
    private bool destination;
    public float explosionwait;
    private Material hitboxMaterial;
    private float transparency = 0f;
    public VisualEffect explosion;
    public int bombdamage;
    public AudioSource explosionSound;

    private string enemytag = "Enemy";
    private Collider currentTarget;
    public float bombradius;
    public bool exploded = false;

    void Start()
    {
        startpoint = transform.position;
        destination = false;
        startTime = Time.time;
      hitbox =  Instantiate(bombHitBox);
        if (hitbox != null)
        {
            hitbox.transform.position = new Vector3(targetpoint.x, 0.5f, targetpoint.z);

            // Ensure the material supports transparency and get the material reference
            Renderer renderer = hitbox.GetComponent<Renderer>();
            if (renderer != null)
            {
                hitboxMaterial = renderer.material;
                // Make sure the shader supports transparency (e.g., Standard Shader in Transparent mode)
                hitboxMaterial.SetFloat("_Mode", 3); // Make the material transparent
                hitboxMaterial.color = new Color(hitboxMaterial.color.r, hitboxMaterial.color.g, hitboxMaterial.color.b, transparency);
            }
        }
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
            if (devil == true)
            {
                print("rotatin");
                // Calculate the direction to the target
                Vector3 targetDirection = targetpoint - transform.position;

                // Calculate the right vector, ensuring we maintain the Y-axis as up
                Vector3 right = Vector3.Cross(targetDirection.normalized, Vector3.up);

                // Recalculate the up direction so that the Y-axis faces the target direction
                Vector3 up = Vector3.Cross(right, targetDirection.normalized);

                // Create a rotation based on the direction the Y-axis should face (up) and the forward direction (targetDirection)
                Quaternion targetRotation = Quaternion.LookRotation(up, targetDirection.normalized);

                // Smoothly rotate the bomb to face the target
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
            }

            if (hitboxMaterial != null)
            {
                if (transparency <= 0.6)
                {
                    transparency = Mathf.Max(0, transparency + 0.01f);
                }
                    Color currentColor = hitboxMaterial.color;
                
                hitboxMaterial.color = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);
            }

            // Destroy the bomb once it reaches the target
            if (fractionOfJourney >= 1.05f)
            {
                destination = true;
            }
        }
        else if (exploded == false)
        {
            exploded = true;
            StartCoroutine(Waittillexplode());
            Destroy(hitbox);
        }
    }

    IEnumerator Waittillexplode()
    {
        explosion.Play();


        explosionSound.PlayOneShot(explosionSound.clip);
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, bombradius);
  
        foreach (Collider collider in collidersInRange)
        {
            if (collider.CompareTag(enemytag))
            {
                if (collider.GetComponent<EnemyAI>() != null)
                {
                    collider.GetComponent<EnemyAI>().getDamage(bombdamage);


                }
            }
        }


        yield return new WaitForSeconds(explosionwait);




   










        Debug.Log("Bomb exploded!");
       


       

        Destroy(gameObject); // Destroy the bomb

  

       




    }
}