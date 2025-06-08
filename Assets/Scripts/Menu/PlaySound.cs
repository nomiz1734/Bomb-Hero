using UnityEngine;
using UnityEngine.UI;


public class PlaySound : MonoBehaviour
{


    public AudioSource audioSource; 

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
