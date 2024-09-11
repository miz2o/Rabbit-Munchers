using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject placePart;
    public bool selpartPlaced = false;
    public GameObject selectionpart = null;
    public Vector3 worldPosition;



    public bool isPlaceModeOn;
    // Start is called before the first frame update
    void Start()
    {

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
                Debug.Log("World Position: " + worldPosition);

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

                }
            }
        }
    }
}
