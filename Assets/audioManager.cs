using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("------------- Audio Source ------------")]
    [SerializeField] AudioSource musicSource;

    [Header("------------- Theme Songs ------------")]
    public AudioClip mercuryTheme;
    public AudioClip venusTheme;
    public AudioClip earthTheme;
    public AudioClip marsTheme;
    public AudioClip jupiterTheme;
    public AudioClip saturnTheme;
    public AudioClip uranusTheme;
    public AudioClip neptuneTheme;

    private void Start()
    {
        musicSource.clip = earthTheme;
        musicSource.Play();
    }

    public void playTheme(AudioClip theme)
    {

        musicSource.PlayOneShot(theme);
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

}


