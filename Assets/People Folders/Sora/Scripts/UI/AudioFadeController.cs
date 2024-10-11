using UnityEngine;

public class AudioFadeController : MonoBehaviour
{
    public AudioSource audioSource1;  // The audio source that will fade in
    public AudioSource audioSource2;  // The audio source that will fade out
    public float transitionDuration = 2f;  // Duration of the fade effect
    public float returnDelay = 30f;  // Time to wait before reversing the fade
    private bool isTransitioning = false;

    // Call this method when the UI button is clicked
    public void TriggerFade()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeAudio(true));  // Start by fading in audioSource1 and fading out audioSource2
        }
    }

    private System.Collections.IEnumerator FadeAudio(bool forward)
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        // Store the initial volumes
        float startVolume1 = audioSource1.volume;
        float startVolume2 = audioSource2.volume;

        // Set target volumes
        float targetVolume1 = forward ? 1f : 0f;  // If forward is true, fade in audioSource1, otherwise fade it out
        float targetVolume2 = forward ? 0f : 1f;  // If forward is true, fade out audioSource2, otherwise fade it in

        // Perform the fade over transitionDuration
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Lerp the volume of the audio sources
            audioSource1.volume = Mathf.Lerp(startVolume1, targetVolume1, t);  // Fade audioSource1
            audioSource2.volume = Mathf.Lerp(startVolume2, targetVolume2, t);  // Fade audioSource2

            yield return null;  // Wait for the next frame
        }

        // Ensure the final volumes are set exactly
        audioSource1.volume = targetVolume1;
        audioSource2.volume = targetVolume2;

        // If we just faded in, wait for 30 seconds, then reverse the fade
        if (forward)
        {
            yield return new WaitForSeconds(returnDelay);
            StartCoroutine(FadeAudio(false));  // Reverse the fade after the delay
        }

        isTransitioning = false;
    }
}
