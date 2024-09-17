using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject placePart;
    public bool selpartPlaced = false;
    public GameObject selectionpart = null;
    public Vector3 worldPosition;
    public Button[] towerButtons;
    public GameObject[] towers;
    public Button currentButton;
    public GameObject currentselection;
    public GameObject placingtower;
    public bool isCurrentPlaced;
    public int currency;
    public GameObject wpholder;

    public bool isPlaceModeOn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method called");
        if (towerButtons == null || towerButtons.Length == 0)
        {
            Debug.LogError("Tower buttons array is not set or empty.");
            return;
        }

        foreach (Button button in towerButtons)
        {
            if (button == null)
            {
                Debug.LogError("Button is null.");
                continue;
            }
            button.onClick.AddListener(() => OnButtonClick(button));
            Debug.Log("Added listener to button: " + button.name);
        }
        isPlaceModeOn = false;
    }


    // Update is called once per frame
    void Update()
    {
        currency = wpholder.GetComponent<WaveHandler>().currency;
        if (isPlaceModeOn)
        {
            isCurrentPlaced = false;
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Get the world position where the ray hit
                Vector3 worldPosition = hit.point;
                GameObject objectHit = hit.transform.gameObject;

                // Do something with the world position


                if (objectHit != null && objectHit.tag == "Floor")
                {

                    if (selpartPlaced == false)
                    {
                        placingtower = Instantiate(currentselection, new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z)), Quaternion.identity);
                        selectionpart = Instantiate(placePart, new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z)), Quaternion.identity);
                        selpartPlaced = true;

                    }
                    if (selectionpart != null && placingtower != null)
                    {
                        placingtower.transform.position = new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z));
                        selectionpart.transform.position = new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z));
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isCurrentPlaced = true;
                    selpartPlaced = false;
                    isPlaceModeOn = false;
                    int towerprice = placingtower.GetComponent<TowerAttacking>().towercost;
                    int afteramt = currency -= towerprice;

                  
                
      
                    if (afteramt >= 0)
                    {
                        placingtower.GetComponent<TowerAttacking>().Place();
                        wpholder.GetComponent<WaveHandler>().RemoveCurrency(towerprice);
                    }
                    else
                    {
                        Destroy(placingtower);
                        print("not enough currency");
                    }

                    // Clean up the selection part once placed
                    if (selectionpart != null)
                    {
                        Destroy(selectionpart);
                    }
                }
            }
        }





    }

    void OnButtonClick(Button clickedButton)
    {
        Debug.Log("Clicked: " + clickedButton.name);

        // Get the index of the clicked button
        int clickedButtonIndex = System.Array.IndexOf(towerButtons, clickedButton);
        if (clickedButtonIndex >= 0 && clickedButtonIndex < towers.Length)
        {
            currentselection = towers[clickedButtonIndex];
            Debug.Log("Selected tower: " + currentselection.name);
        }

        // Set up the current button and enable place mode
        int currentprice = currentselection.GetComponent<TowerAttacking>().towercost;
        int afterprice = currency -= currentprice;

        if (afterprice >= 0)
        {

            currentButton = clickedButton;
            isPlaceModeOn = true;
        }




        // Destroy the preview objects if any
        if (selectionpart != null)
        {
            Destroy(selectionpart);


            if (placingtower != null && isCurrentPlaced == false)
            {
                Destroy(placingtower);
            }

            selpartPlaced = false;
        }


    }


}