using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
    // List of scene names to preload
    public List<string> scenesToPreload;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PreloadScenes());
    }

    IEnumerator PreloadScenes()
    {
        foreach (string sceneName in scenesToPreload)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                asyncLoad.allowSceneActivation = false;

                // Wait until the scene is loaded
                while (!asyncLoad.isDone)
                {
                    // When the loading is 90% done, it is considered as almost done
                    // allowSceneActivation = true can activate it, but we don't want to
                    if (asyncLoad.progress >= 0.9f)
                    {
                        break;
                    }

                    yield return null;
                }

                Debug.Log("Preloaded Scene: " + sceneName);
            }
        }

        // After preloading all scenes, you can activate the first scene or show your main menu
        // Example:
        SceneManager.LoadScene("Main_Menu");
    }
}
