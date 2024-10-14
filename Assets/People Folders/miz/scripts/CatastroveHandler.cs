using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatastroveHandler : MonoBehaviour
{

    public Button button;
    public bool Catastroveenabled;
    public GameObject wpholder;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => OnButtonClick(button));
        Debug.Log("Added listener to button: " + button.name);
    }



    void OnButtonClick(Button clickedButton)
    {
        Catastroveenabled = true;
    }

    // Update is called once per frame
    void Update()
    {
     
            if (Catastroveenabled && Input.GetMouseButtonDown(0)) // Detect left-click (mouse button 0)
            {
                DetectTowerClick();
            }

    }

    void DetectTowerClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera through the mouse position
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tower")) // Check if the object hit has the "Tower" tag
            {
                Debug.Log("Clicked on a Tower!");
                // Perform whatever actions you need for clicking on a Tower object
                wpholder.GetComponent<WaveHandler>().catastroveamt = 0;
                button.gameObject.SetActive(false);
                print("Catastrove used");
                hit.collider.gameObject.transform.localScale = new Vector3(3, 3, 3); // Scale to 3x its size
      

                Catastroveenabled = false;
                if (hit.collider.GetComponent<TowerAttacking>() != null)
                {
                    hit.collider.GetComponent<TowerAttacking>().towerRadius *= 5;
                    hit.collider.GetComponent<TowerAttacking>().towerdamage *= 5;
                    hit.collider.GetComponent<TowerAttacking>().throwspeed /= 3;
                    hit.collider.GetComponent<TowerAttacking>().towerspeed /= 3;
                    hit.collider.GetComponent<TowerAttacking>().cataEffect.Play();




                 
                }
              TowerAttacking tower = hit.collider.GetComponent<TowerAttacking>();
                StartCoroutine(RevertTowerAfterDelay(hit.transform.gameObject));



            }
        }
    }

    IEnumerator RevertTowerAfterDelay(GameObject tower)
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(30);
        tower.transform.localScale = new Vector3(1, 1, 1); // Reset to original size
        tower.GetComponent<TowerAttacking>().towerRadius /= 5;
        tower.GetComponent<TowerAttacking>().towerdamage /= 5;
        tower.GetComponent<TowerAttacking>().throwspeed *= 3;
        tower.GetComponent<TowerAttacking>().towerspeed *= 3;

       
    }
}

