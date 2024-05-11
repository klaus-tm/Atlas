using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    public float speed = 10f; // the speed of the movement
    public float mouseSensitivity = 100f; // the sensitivity of the mouse

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate the new rotation around the y-axis (left and right)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        // Calculate the new rotation around the x-axis (up and down)
        yRotation += mouseX;

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Get the keyboard movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the new position based on the camera's forward and right vectors
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Apply the movement
        transform.position += movement * speed * Time.deltaTime;
    }
}
