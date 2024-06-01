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
                // Calculate the position to place the child on top of the sphere
                Vector3 relativePosition = new Vector3(0, parentCollider.radius * 2.065f, 0);

                // Get the name of the parent object
                string parentName = parentObject.name;
                switch (parentName)
                {
                    case "Mercury":
                        relativePosition.y = 3.88f;
                        break;
                    
                    case "Venus":
                        relativePosition.y = 2.169f;
                        break;
                    case "Earth":
                        relativePosition.y = 2.1f;
                        break;
                    case "Mars":
                        relativePosition.y = 3.13f;
                        break;
                    case "Jupiter":
                        relativePosition.y = 1.09f;
                        break;
                    case "Saturn":
                        relativePosition.y = 1.1176f;
                        break;
                    case "Uranus":
                        relativePosition.y = 1.2773f;
                        break;
                    case "Neptune":
                        relativePosition.y = 1.282f;
                        break;
                    case "Pluto":
                        relativePosition.y = 2.12f;
                        break;
                    case "Moon":
                        relativePosition.y = 5.09f;
                        break;
                    case "Sun Sphere":
                        relativePosition.y = 0.6f;
                        break;
                    default:
                        Debug.LogError("Invalid parent object name: " + parentName);
                        break;
                }

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
            Debug.LogError("Please assign all GameObjects in the inspector.");
        }
    }

 public void SetParent(GameObject newParentObject)
{
    if (childObject != null && newParentObject != null)
    {
        // Set the parent of the childObject to the new parentObject
        childObject.transform.SetParent(newParentObject.transform);

        // Set the local position of the childObject based on the name of the new parent object (celestial)
        string parentName = newParentObject.name;
        switch (parentName)
        {
            case "Mercury":
                childObject.transform.localPosition = new Vector3(0, 3.88f, 0);
                break;
            case "Venus":
                childObject.transform.localPosition = new Vector3(0, 2.169f, 0);
                break;
            case "Earth":
                childObject.transform.localPosition = new Vector3(0, 2.1f, 0);
                break;
            case "Mars":
                childObject.transform.localPosition = new Vector3(0, 3.13f, 0);
                break;
            case "Jupiter":
                childObject.transform.localPosition = new Vector3(0, 1.09f, 0);
                break;
            case "Saturn":
                childObject.transform.localPosition = new Vector3(0, 1.1176f, 0);
                break;
            case "Uranus":
                childObject.transform.localPosition = new Vector3(0, 1.2773f, 0);
                break;
            case "Neptune":
                childObject.transform.localPosition = new Vector3(0, 1.282f, 0);
                break;
            case "Pluto":
                childObject.transform.localPosition = new Vector3(0, 2.12f, 0);
                break;
            case "Moon":
                childObject.transform.localPosition = new Vector3(0, 5.09f, 0);
                break;
            case "Sun Sphere":
                childObject.transform.localPosition = new Vector3(0, 0.6f, 0);
                break;
            default:
                Debug.LogError("Invalid parent object name: " + parentName);
                break;
        }

        // Reset local rotation if needed
        childObject.transform.localRotation = Quaternion.identity;
    }
    else
    {
        Debug.LogError("Please assign the childObject and newParentObject.");
    }
}

}
