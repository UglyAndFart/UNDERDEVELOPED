using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source-------------")]
    [SerializeField] private AudioSource musicSource; // Reference to the music audio source
    [SerializeField] private AudioSource SFXSource;   // Reference to the SFX audio source
    [SerializeField] private AudioMixer audioMixer;   // Reference to the AudioMixer

    [Header("--------------Audio Clip--------------")]
    public AudioClip background; // Background music clip
    public AudioClip run;        // Sound effect for running
    public AudioClip button;     // Sound effect for button press
    public AudioClip hover;  // Hover SFX
    public AudioClip click;  // Click SFX

    private void Start()
    {
        PlayBackgroundMusic();
    }

    // Method to play background music
    public void PlayBackgroundMusic()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // Method to play SFX
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Method to set music volume
    public void SetMusicVolume(float volume)
    {
        if (volume <= 0)
        {
            audioMixer.SetFloat("music", -80f); // Mute the music
        }
        else
        {
            audioMixer.SetFloat("music", Mathf.Log10(volume) * 20); // Set the music volume
        }
    }

    // Method to set SFX volume
    public void SetSFXVolume(float volume)
    {
        if (volume <= 0)
        {
            audioMixer.SetFloat("sfx", -80f); // Mute the SFX
        }
        else
        {
            audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20); // Set the SFX volume
        }
    }
}
