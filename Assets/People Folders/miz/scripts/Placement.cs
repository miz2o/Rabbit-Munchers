using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject placePart;
    public bool partPlaced = false;
    public GameObject selectionpart = null;
    public Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Get the world position where the ray hit
            Vector3 worldPosition = hit.point;

            // Do something with the world position
            Debug.Log("World Position: " + worldPosition);

            if (partPlaced == false)
            {
                selectionpart = Instantiate(placePart, new Vector3(worldPosition.x, 1, worldPosition.z), Quaternion.identity);
                partPlaced = true;

            }
            if (selectionpart != null)
            {
                selectionpart.transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {


        }
    }
}
