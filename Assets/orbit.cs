using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    public Transform target; // the object to orbit around; assign this to the sun in the Inspector
    public float orbitSpeed = 20f; // the speed of the orbit
    public float rotationSpeed = 50f; // the speed of the rotation

    void Update()
    {
        if (target != null)
        {
            // Rotate around the target
            transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }

        // Rotate on its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
