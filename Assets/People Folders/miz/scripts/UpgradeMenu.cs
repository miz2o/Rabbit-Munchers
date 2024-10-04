using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject selectedTower;
    public Vector3 stayorientation;
    public GameObject spawnedmenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        upgradeMenu.transform.eulerAngles = stayorientation;

        if (Input.GetMouseButtonDown(0))
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
                if(spawnedmenu != null)
                {
                    Destroy(spawnedmenu);
                }
                selectedTower = hit.collider.gameObject;
                spawnedmenu = Instantiate(upgradeMenu);



                if (selectedTower != null)
                {
                    // Calculate a new position relative to the tower
                    Vector3 gotoo = new Vector3(
                        selectedTower.transform.position.x + 0.624007f,
                        selectedTower.transform.position.y + 1.091f,
                        selectedTower.transform.position.z - 0.389f
                    );
                    spawnedmenu.transform.position = gotoo;
                }
            }
            else
            {
                if (spawnedmenu != null)
                {
                    Destroy(spawnedmenu);
                }
            }
        }
    }
}
