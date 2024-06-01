using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialCameraRotation : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the camera around its Y-axis at the specified speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
