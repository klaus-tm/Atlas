using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public Transform earth; // the Earth to orbit around; assign this in the Inspector
    public float orbitSpeed = 10f; // the speed of the orbit
    public float rotationSpeed = 50f; // the speed of the rotation

    private Vector3 earthToMoonVector;

    void Start()
    {
        // Get the initial position of the moon relative to the Earth
        earthToMoonVector = transform.position - earth.position;
    }

    void Update()
    {
        if (earth != null)
        {
            // Update the position of the moon relative to the Earth
            transform.position = earth.position + earthToMoonVector;

            // Rotate around the Earth
            transform.RotateAround(earth.position, Vector3.up, orbitSpeed * Time.deltaTime);

            // Update the Earth-to-moon vector
            earthToMoonVector = transform.position - earth.position;
        }

        // Rotate on its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
