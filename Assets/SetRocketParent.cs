using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRocketParent : MonoBehaviour
{
    public GameObject childObject;
    public GameObject parentObject;

    void Start()
    {
        if (childObject != null && parentObject != null)
        {
            // Set the parent of the childObject to parentObject
            childObject.transform.SetParent(parentObject.transform);

            // Get the size of the parent object
            SphereCollider parentCollider = parentObject.GetComponent<SphereCollider>();
            if (parentCollider != null)
            {
                float parentRadius = parentCollider.radius * parentObject.transform.localScale.y; // Adjust for potential scaling

                // Calculate the position to place the child on top of the sphere
                Vector3 relativePosition = new Vector3(0, parentCollider.radius*2.065f, 0);

                // Set the child's local position
                childObject.transform.localPosition = relativePosition;

                // Reset local rotation if needed
                childObject.transform.localRotation = Quaternion.identity;

                // Move the camera to the same position as the childObject
                // Camera.main.transform.localPosition = new Vector3(0,parentCollider.radius*2.1f,0); // Position the camera at the center of the childObject
                // Camera.main.transform.localRotation = Quaternion.identity; // Reset camera rotation
            }
            else
            {
                Debug.LogError("Parent object does not have a SphereCollider component.");
            }
        }
        else
        {
            Debug.LogError("Please assign all GameObjects in the inspector.");
        }
    }
}
