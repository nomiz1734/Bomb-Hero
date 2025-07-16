using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip exp;

    public void PlayShoot()
    {
        effectAudioSource.PlayOneShot(shoot);
    }
    public void PlayExp()
    {
        effectAudioSource.PlayOneShot(exp);
    }

    public void PlayBossMusic()
    {
        defaultAudioSource.Stop();
        bossAudioSource.Play();
    }
    public void PlayDefaultSound() { 
        bossAudioSource.Stop();
        defaultAudioSource.Play();
    }

    public void StopAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Stop();
        effectAudioSource.Stop();
    }
}
