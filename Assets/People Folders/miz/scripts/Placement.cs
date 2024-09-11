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
        if (isPlaceModeOn)
        {
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
                        selectionpart = Instantiate(placePart, new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z)), Quaternion.identity);
                        selpartPlaced = true;

                    }
                    if (selectionpart != null)
                    {
                        selectionpart.transform.position = new Vector3(Mathf.RoundToInt(worldPosition.x), 1, Mathf.RoundToInt(worldPosition.z));
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    selpartPlaced = false;
                    isPlaceModeOn = false;
                }
            }
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        Debug.Log("Clicked: " + clickedButton.name);
        if (currentButton != clickedButton)
        {
            currentButton = clickedButton;
            isPlaceModeOn = true;
        }
        else
        {
            isPlaceModeOn = false;
            Destroy(selectionpart);
        }
  

    }

}