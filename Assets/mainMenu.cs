using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public FadeController titleFadeController;
    public FadeController mainMenuFadeController;
    public FadeController exitConfirmFadeController;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        titleFadeController.FadeIn();
        mainMenuFadeController.FadeIn();
        exitConfirmFadeController.FadeOut();
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
        SceneManager.LoadSceneAsync(1);
    }
}
