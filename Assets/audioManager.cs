using UnityEngine;
using System.Collections;

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

    public void StopMusic(float fadeDuration = 1f)
    {
        StartCoroutine(FadeOutAndStop(fadeDuration));
    }

    private IEnumerator FadeOutAndStop(float fadeDuration)
    {
        float startVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume for future playback
    }
}
