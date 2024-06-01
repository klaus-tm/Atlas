using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextReveal : MonoBehaviour
{
    public float revealSpeed = 0.1f; // Time between revealing each letter
    public Button pulsatingButton; // Reference to the button
    public float fadeSpeed = 1f; // Speed of the fading effect
    public AudioSource backgroundAudio; // Reference to the background audio source
    public KeyCode skipKey = KeyCode.Space; // Key to skip the text reveal

    private CanvasGroup buttonCanvasGroup;
    private CanvasGroup[] textCanvasGroups;
    private bool skipTextReveal;

    void Start()
    {
        // Find all GameObjects with the tag "tutorial"
        GameObject[] tutorialObjects = GameObject.FindGameObjectsWithTag("tutorial");

        // Sort the array based on the name or another criteria
        tutorialObjects = tutorialObjects.OrderBy(obj => obj.name).ToArray();
        System.Array.Reverse(tutorialObjects);

        foreach (var tutorialObject in tutorialObjects)
        {
            tutorialObject.SetActive(false);
        }

        // Get the CanvasGroup component from the button
        buttonCanvasGroup = pulsatingButton.GetComponent<CanvasGroup>();
        if (buttonCanvasGroup == null)
        {
            buttonCanvasGroup = pulsatingButton.gameObject.AddComponent<CanvasGroup>();
        }

        // Initially set the button to be inactive
        pulsatingButton.gameObject.SetActive(false);

        // Initialize text CanvasGroups
        textCanvasGroups = new CanvasGroup[tutorialObjects.Length];
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            CanvasGroup cg = tutorialObjects[i].GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = tutorialObjects[i].AddComponent<CanvasGroup>();
            }
            cg.alpha = 1; // Ensure texts are fully visible initially
            textCanvasGroups[i] = cg;
        }

        // Start the coroutine to reveal all texts
        StartCoroutine(RevealAllTexts(tutorialObjects));

        // Add the button click listener
        pulsatingButton.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        // Check for the skip key press
        if (Input.GetKeyDown(skipKey))
        {
            skipTextReveal = true;
        }
    }

    private IEnumerator RevealAllTexts(GameObject[] tutorialObjects)
    {
        foreach (GameObject obj in tutorialObjects)
        {
            TMP_Text tmpText = obj.GetComponent<TMP_Text>();
            if (tmpText != null)
            {
                tmpText.gameObject.SetActive(true); // Activate the GameObject before revealing text
                skipTextReveal = false;
                yield return StartCoroutine(RevealText(tmpText));
            }
            else
            {
                Debug.LogError("TextMeshPro component is missing on " + obj.name);
            }
        }

        // Activate the button and start the fading effect after all texts are revealed
        pulsatingButton.gameObject.SetActive(true);
        StartCoroutine(FadeButton());
    }

    private IEnumerator RevealText(TMP_Text tmpText)
    {
        string fullText = tmpText.text;
        tmpText.text = ""; // Clear the initial text

        for (int i = 0; i <= fullText.Length; i++)
        {
            if (skipTextReveal)
            {
                tmpText.text = fullText;
                break;
            }

            tmpText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(revealSpeed);
        }
    }

    private IEnumerator FadeButton()
    {
        bool fadingOut = false;

        while (true)
        {
            float timer = 0f;
            while (timer < 1f)
            {
                buttonCanvasGroup.alpha = fadingOut ? 1f - timer : timer;
                timer += Time.deltaTime * fadeSpeed;
                yield return null;
            }
            fadingOut = !fadingOut;
        }
    }

    private void OnButtonClick()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        // Fade out texts
        foreach (CanvasGroup cg in textCanvasGroups)
        {
            StartCoroutine(FadeOut(cg));
        }

        // Fade out button
        StartCoroutine(FadeOut(buttonCanvasGroup));

        // Fade out background audio
        yield return StartCoroutine(FadeOutAudio(backgroundAudio));

        // Wait a bit to ensure all fade out animations are complete
        yield return new WaitForSeconds(1f);

        // Load the scene
        SceneManager.LoadSceneAsync(2);
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float timer = 0f;
        while (timer < 1f)
        {
            canvasGroup.alpha = 1f - timer;
            timer += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        float timer = 0f;
        while (timer < 1f)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer);
            timer += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        audioSource.volume = 0f;
    }

    public void StartGame()
    {
        Debug.Log("Button Pressed, starting fade out...");
        OnButtonClick();
    }
}
