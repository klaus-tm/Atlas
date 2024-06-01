using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlanet : MonoBehaviour
{
    public GameObject rocket; // Assign the Rocket object in the Inspector
    private GameObject currentParent; // Track the current parent object

    private GameObject newPaernt;
    void Start()
    {
        // Set the initial parent to Earth if it exists
        GameObject earth = GameObject.Find("Earth");
        if (earth != null)
        {
            currentParent = earth;
            SetRocketParent setRocketParent = rocket.GetComponent<SetRocketParent>();
            if (setRocketParent != null)
            {
                setRocketParent.SetParent(currentParent);
            }
            else
            {
                Debug.LogError("Rocket does not have SetRocketParent component.");
            }
        }
        else
        {
            Debug.LogError("Earth object not found. Ensure it exists and is named correctly.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.transform.gameObject;
                if (clickedObject.CompareTag("Celestial") && clickedObject != currentParent)
                {
                    Debug.Log("Clicked on Celestial object: " + clickedObject.name);

                    newPaernt = clickedObject;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SetNewParentAfterDelay(newPaernt));
        }
    }

    IEnumerator SetNewParentAfterDelay(GameObject newParent)
    {
        yield return new WaitForSeconds(4.5f); // Wait for 4.5 seconds

        SetRocketParent setRocketParent = rocket.GetComponent<SetRocketParent>();
        if (setRocketParent != null)
        {
            setRocketParent.SetParent(newParent);
            currentParent = newParent;
        }
        else
        {
            Debug.LogError("Rocket does not have SetRocketParent component.");
        }
    }
}
