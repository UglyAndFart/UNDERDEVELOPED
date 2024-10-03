using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; // Reference to the NewAudioMixer
    [SerializeField] private Slider masterSlider; // Slider for Master volume
    [SerializeField] private Slider musicSlider;  // Slider for Music volume
    [SerializeField] private Slider sfxSlider;    // Slider for SFX volume

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            masterSlider.value = 1f; // Default to full volume
            musicSlider.value = 1f;  // Default to full music volume
            sfxSlider.value = 1f;    // Default to full SFX volume
            SetAllVolumes();         // Initialize all volumes
        }

        SetAllVolumes();
    }

    // Set all volume levels
    public void SetAllVolumes()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        myMixer.SetFloat("master", volume <= 0 ? -80f : Mathf.Log10(volume) * 20); // Adjust master volume
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", volume <= 0 ? -80f : Mathf.Log10(volume) * 20); // Adjust music volume
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", volume <= 0 ? -80f : Mathf.Log10(volume) * 20); // Adjust SFX volume
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        SetAllVolumes();
    }
}
