using UnityEngine;

public class parallaxScrolling : MonoBehaviour
{
    public float followFactor = 0.1f;
    public float depth = 10f;
    public float scrollSpeed = 10f;
    public float minX = -10f; // Minimum X limit
    public float maxX = 10f;  // Maximum X limit
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Handle parallax scrolling based on mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = depth;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 offset = worldMousePosition - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, depth));

        // Dampen the camera movement for smoother interaction
        transform.position = Vector3.Lerp(transform.position, initialPosition + offset * followFactor, Time.deltaTime * 5);

        // Handle scroll wheel input for left and right movement
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        initialPosition += new Vector3(scroll * scrollSpeed, 0, 0);

        // Apply limits to the scrolling on the X axis
        initialPosition.x = Mathf.Clamp(initialPosition.x, minX, maxX);

        // Maintain the original Z axis position
        initialPosition.z = transform.position.z;
    }
}
