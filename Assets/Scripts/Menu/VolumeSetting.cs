using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSetting
    : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);  
    }
    public void LoadVolume()
    {
     

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", -1f);

        SetMusicVolume();
    }
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", -1f);

        if (savedVolume == -1f)
        {
            savedVolume = 0.75f; // đặt âm lượng mặc định
            PlayerPrefs.SetFloat("MusicVolume", savedVolume);
        }

        musicSlider.value = savedVolume;
        SetMusicVolume();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
