using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] playlist;
    public int currentClipIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayPlaylist());
    }

    IEnumerator PlayPlaylist()
    {
        while (true)
        {
            // Play the current audio clip
            audioSource.clip = playlist[currentClipIndex];
            audioSource.Play();

            // Wait until the clip has finished playing
            yield return new WaitForSeconds(audioSource.clip.length);

            // Move to the next clip
            currentClipIndex++;
            if (currentClipIndex >= playlist.Length) 
            {
                currentClipIndex = 0; // Loop back to the first clip if at the end
            }
        }
    }
}