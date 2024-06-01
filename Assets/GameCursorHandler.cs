using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class SpaceMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f; // The sensitivity of the mouse
    public float zoomFOV = 30f; // The field of view when zoomed in
    public float zoomSpeed = 10f; // The speed of zoom transition
    public Image crosshairImage; // The crosshair image

    private float defaultFOV;
    private float targetFOV;
    private float xRotation = 0f;
    private bool isPaused = false; // Boolean to track if the game is paused

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize the default FOV
        defaultFOV = Camera.main.fieldOfView;
        targetFOV = defaultFOV;

        // Enable the crosshair
        if (crosshairImage != null)
        {
            crosshairImage.enabled = true;
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

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
        }
        else
        {
            targetFOV = defaultFOV;
        }

        // Smoothly transition to the target FOV
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
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
}
