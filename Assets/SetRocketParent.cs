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
                Vector3 relativePosition = new Vector3(0, parentCollider.radius+0.1f, 0);

                // Set the child's local position
                childObject.transform.localPosition = relativePosition;

                // Reset local rotation if needed
                childObject.transform.localRotation = Quaternion.identity;

            }
            else
            {
                Debug.LogError("Parent object does not have a SphereCollider component.");
            }
        }
        else
        {
            Debug.LogError("Please assign both the child and parent GameObjects in the inspector.");
        }
    }
}
