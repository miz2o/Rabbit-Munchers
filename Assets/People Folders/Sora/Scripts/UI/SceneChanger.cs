using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject panel;           // Reference to the panel GameObject
    public string sceneName;           // Name of the primary scene to load
    public string alternateSceneName;  // Name of the alternate scene to load
    [Range(0, 100)]
    public float alternateSceneChance = 50f; // Percentage chance of loading the alternate scene
    public float pauseDuration = 3f;   // Duration to show the panel before changing scenes

    void Start()
    {
        // Ensure the panel is initially inactive
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void ShowPanelAndChangeScene()
    {
        // Activate the panel
        if (panel != null)
        {
            panel.SetActive(true);
        }

        // Start the coroutine to change the scene after a pause
        StartCoroutine(ChangeSceneAfterPause());
    }

    private IEnumerator ChangeSceneAfterPause()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(pauseDuration);

        // Determine which scene to load based on the random chance
        string sceneToLoad = sceneName; // Default to the primary scene
        if (!string.IsNullOrEmpty(alternateSceneName))
        {
            float randomValue = Random.Range(0f, 100f);
            if (randomValue < alternateSceneChance)
            {
                sceneToLoad = alternateSceneName;
            }
        }

        // Load the selected scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
