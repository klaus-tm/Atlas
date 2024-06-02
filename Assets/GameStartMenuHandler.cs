using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public FadeController titleFadeController;
    public FadeController mainMenuFadeController;
    public FadeController exitConfirmFadeController;
    public AudioSource music;

    void Start()
    {
        SetInitialAlpha();
        ShowMainMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetInitialAlpha()
    {
        // Set initial alpha to 0 for all CanvasGroup components to ensure the fade in takes place
        titleFadeController.GetComponent<CanvasGroup>().alpha = 0f;
        mainMenuFadeController.GetComponent<CanvasGroup>().alpha = 0f;
        exitConfirmFadeController.GetComponent<CanvasGroup>().alpha = 0f;
    }


    public void ShowMainMenu()
    {
        exitConfirmFadeController.FadeOut();
        titleFadeController.FadeIn();
        mainMenuFadeController.FadeIn();
    }

    public void ShowQuitConfirmation()
    {
        mainMenuFadeController.FadeOut();
        exitConfirmFadeController.FadeIn();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {
        titleFadeController.FadeOut();
        mainMenuFadeController.FadeOut();
        StartCoroutine(FadeOutAudio(music));

        // Wait for the fade-out duration before loading the next scene
        yield return new WaitForSeconds(titleFadeController.fadeDuration);
        

        SceneManager.LoadSceneAsync(1);
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        float timer = 0f;
        while (timer < 1f)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer);
            timer += Time.deltaTime * 1;
            yield return null;
        }
        audioSource.volume = 0f;
    }
}
