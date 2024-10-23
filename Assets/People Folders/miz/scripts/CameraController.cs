using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float mousespeed;
    public float scrollspeed;
    public float vert;
    public float hor;
    public float vertmouse;
    public float hormouse;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for movement
        vert = Input.GetAxis("Vertical") * speed;
        hor = Input.GetAxis("Horizontal") * speed;

        // Get input for mouse movement
        vertmouse = Input.GetAxis("Mouse X") * mousespeed;
        hormouse = Input.GetAxis("Mouse Y") * mousespeed;

        // Move the parent object (if there's a parent)
        Vector3 movement = new Vector3(hor, 0, vert); // Adjusted to avoid affecting y in this step
        transform.parent.position += movement * Time.deltaTime;

        Vector3 parentPosition = transform.parent.position;
        parentPosition.x = Mathf.Clamp(parentPosition.x, -25, -8);
        parentPosition.z = Mathf.Clamp(parentPosition.z, 2, 22);
        transform.parent.position = parentPosition;

        // Get input for scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Apply scroll input to the Y position
        if (scrollInput != 0)
        {
            Vector3 newPosition = transform.parent.position;
            newPosition.y -= scrollInput * scrollspeed;
         

            if(newPosition.y > 31)
            {
                newPosition.y = 31;
            }
            if (newPosition.y < 17)
            {
                newPosition.y = 17;
            }
            transform.parent.position = newPosition;

        }

        // Rotate the camera based on mouse movement
        transform.Rotate(-hormouse, vertmouse, 0); // Applied only rotation here

        // Clamp the rotation to desired limits
        Vector3 rotationEuler = transform.rotation.eulerAngles;

        if (rotationEuler.x < 57)
        {
            rotationEuler.x = 57;
        }
        else if (rotationEuler.x > 60)
        {
            rotationEuler.x = 60;
        }

        // Lock the Z-axis and Y-axis rotation
        rotationEuler.z = 0;
        rotationEuler.y = 0;

        // Apply the modified rotation back to the transform
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
