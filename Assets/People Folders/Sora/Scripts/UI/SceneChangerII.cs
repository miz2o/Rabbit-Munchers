using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerII : MonoBehaviour
{
    [Header("Scene Settings")]
    // Name of the scene to load
    public string sceneName;

    // Time to wait before showing the panel (in seconds)
    public float timeToWait = 5f;

    // Time to wait after the panel is shown before changing the scene
    public float waitAfterPanel = 2f;

    [Header("Panel Settings")]
    // Reference to the panel to be enabled
    public GameObject panel;

    [Header("Music Settings")]
    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Audio clip to play before the scene changes
    public AudioClip musicClip;

    // Time before the scene changes to play the music (in seconds)
    public float timeBeforeMusic = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Enable the panel 'timeBeforeMusic' seconds before the scene changes
        Invoke("EnablePanel", timeToWait - timeBeforeMusic);

        // Play the music 'timeBeforeMusic' seconds before the scene changes
        Invoke("PlayMusic", timeToWait - timeBeforeMusic);

        // Start the timer to change the scene after the panel is shown for 'waitAfterPanel' seconds
        Invoke("ChangeScene", timeToWait + waitAfterPanel);
    }

    // Method to change the scene
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to enable the panel
    void EnablePanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    // Method to play the music
    void PlayMusic()
    {
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
}
