using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    // Public variables to set resolution from Inspector
    public int width = 1920;
    public int height = 1080;
    public bool fullscreen = true;

    // Method to set resolution
    public void SetResolution()
    {
        Screen.SetResolution(width, height, fullscreen);
    }
}

