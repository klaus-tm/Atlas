using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace
using UnityEngine.UI; // Import the UI namespace

public class SpaceMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f; // The sensitivity of the mouse
    public float zoomFOV = 30f; // The field of view when zoomed in
    public float zoomSpeed = 10f; // The speed of zoom transition
    public Image crosshairImage; // The crosshair image
    public float zoomedMouseSensitivityFactor = 0.5f; // Factor to scale down the mouse sensitivity while zoomed
    public GameObject fadePanel; // The fade panel
    public float fadeDuration = 1f; // Duration of the fade effect

    private float defaultFOV;
    private float targetFOV;
    private float xRotation = 0f;
    private bool isPaused = false; // Boolean to track if the game is paused
    private float currentMouseSensitivity;
    private CanvasGroup fadePanelCanvasGroup;

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the default FOV
        defaultFOV = Camera.main.fieldOfView;
        targetFOV = defaultFOV;

        // Initialize the current mouse sensitivity
        currentMouseSensitivity = mouseSensitivity;

        // Enable the crosshair
        if (crosshairImage != null)
        {
            crosshairImage.enabled = true;
        }

        // Ensure the fade panel starts transparent
        if (fadePanel != null)
        {
            fadePanelCanvasGroup = fadePanel.GetComponent<CanvasGroup>();
            if (fadePanelCanvasGroup == null)
            {
                fadePanelCanvasGroup = fadePanel.AddComponent<CanvasGroup>();
            }
            fadePanelCanvasGroup.alpha = 0f;
        }
    }

    void Update()
    {
        // Handle right-click to stop time
        if (Input.GetMouseButtonDown(1))
        {
            isPaused = !isPaused;
            ToggleCelestialMovement(isPaused);
        }

        // Get the mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * currentMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * currentMouseSensitivity * Time.deltaTime;

        // Calculate the new rotation around the x-axis (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply the rotation around the y-axis (left and right) to the parent (if the camera is a child of another GameObject)
        if (transform.parent != null)
        {
            transform.parent.Rotate(Vector3.up * mouseX);
        }

        // Handle zoom functionality
        if (Input.GetKey(KeyCode.Z))
        {
            targetFOV = zoomFOV;
            currentMouseSensitivity = mouseSensitivity * zoomedMouseSensitivityFactor;
        }
        else
        {
            targetFOV = defaultFOV;
            currentMouseSensitivity = mouseSensitivity;
        }

        // Smoothly transition to the target FOV
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        // Handle ESC key to trigger fade out and scene transition
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    void ToggleCelestialMovement(bool isPaused)
    {
        Orbit[] orbits = FindObjectsOfType<Orbit>();
        foreach (Orbit orbit in orbits)
        {
            if (isPaused)
            {
                orbit.PauseMovement();
            }
            else
            {
                orbit.ResumeMovement();
            }
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        // Fade out
        if (fadePanelCanvasGroup != null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadePanelCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }
        }

        // Load scene 0
        SceneManager.LoadScene(0);
    }
}
