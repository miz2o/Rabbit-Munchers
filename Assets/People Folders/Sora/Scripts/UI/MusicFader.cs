using UnityEngine;
using System.Collections;

public class MusicFader : MonoBehaviour
{
    public AudioSource audioSource;  // The AudioSource component to fade
    public float fadeDuration = 1.0f; // Duration of the fade-out effect in seconds

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }
}
