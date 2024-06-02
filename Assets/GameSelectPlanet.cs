using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectPlanet : MonoBehaviour
{
    public GameObject rocket; // Assign the Rocket object in the Inspector
    public Canvas canvas; // Assign the Canvas object in the Inspector
    private GameObject currentParent; // Track the current parent object
    private GameObject newParent;
    private TMP_Text selectedPlanetText; // Reference to the "Selected planet" TextMesh Pro component
    private TMP_Text planetInfoText; // Reference to the "Planet Info" TextMesh Pro component
    private const float RADIUS = 5f; // Radius in pixels
    private AudioManager audioManager;

    void Start()
    {
        // Find the "Selected planet" and "Planet Info" TextMesh Pro components in the Canvas
        if (canvas != null)
        {
            selectedPlanetText = canvas.transform.Find("Selected planet").GetComponent<TMP_Text>();
            planetInfoText = canvas.transform.Find("Planet Info").GetComponent<TMP_Text>();
            if (selectedPlanetText == null)
            {
                Debug.LogError("Selected planet text not found in Canvas. Ensure it exists and is named correctly.");
            }
            if (planetInfoText == null)
            {
                Debug.LogError("Planet Info text not found in Canvas. Ensure it exists and is named correctly.");
            }
        }
        else
        {
            Debug.LogError("Canvas not assigned. Please assign the Canvas in the Inspector.");
        }

        selectedPlanetText.text = "";
        planetInfoText.text = "";

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

            // Display the planet info for the initial parent
            DisplayPlanetInfo(currentParent);
        }
        else
        {
            Debug.LogError("Earth object not found. Ensure it exists and is named correctly.");
        }

        PlayCurrentPlanetTheme();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayCurrentPlanetTheme()
    {
        if (currentParent != null)
        {
            // Get the name of the current planet
            string currentPlanetName = currentParent.name;

            // Check the current planet name and play the corresponding theme
            switch (currentPlanetName)
            {
                case "Mercury":
                    audioManager.playTheme(audioManager.mercuryTheme);
                    break;
                case "Venus":
                    audioManager.playTheme(audioManager.venusTheme);
                    break;
                case "Earth":
                    audioManager.playTheme(audioManager.earthTheme);
                    break;
                case "Mars":
                    audioManager.playTheme(audioManager.marsTheme);
                    break;
                case "Jupiter":
                    audioManager.playTheme(audioManager.jupiterTheme);
                    break;
                case "Saturn":
                    audioManager.playTheme(audioManager.saturnTheme);
                    break;
                case "Uranus":
                    audioManager.playTheme(audioManager.uranusTheme);
                    break;
                case "Neptune":
                    audioManager.playTheme(audioManager.neptuneTheme);
                    break;
                default:
                    Debug.LogWarning("No theme found for the current planet: " + currentPlanetName);
                    break;
            }
        }
        else
        {
            Debug.LogError("Current parent not set.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector3 mousePosition = Input.mousePosition;
            if (CheckForCelestialObject(mousePosition))
            {
                if (selectedPlanetText != null)
                {
                    selectedPlanetText.text = "Press R to fly towards " + newParent.name;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SetNewParentAfterDelay(newParent, selectedPlanetText));
            audioManager.StopMusic();
        }
    }

    private bool CheckForCelestialObject(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Perform the initial raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (CheckHitObject(hit))
            {
                return true;
            }
        }

        // Cast rays in a circular pattern around the mouse position
        int rayCount = 8; // Number of rays to cast around the circle
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * Mathf.PI * 2 / rayCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * RADIUS;
            ray = Camera.main.ScreenPointToRay(mousePosition + offset);

            if (Physics.Raycast(ray, out hit))
            {
                if (CheckHitObject(hit))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckHitObject(RaycastHit hit)
    {
        GameObject clickedObject = hit.transform.gameObject;
        if (clickedObject.CompareTag("Celestial") && clickedObject != currentParent)
        {
            Debug.Log("Clicked on Celestial object: " + clickedObject.name);
            newParent = clickedObject;
            return true;
        }
        return false;
    }

    IEnumerator SetNewParentAfterDelay(GameObject newParent, TMP_Text selectedPlanetText)
    {
        yield return new WaitForSeconds(4.5f); // Wait for 4.5 seconds

        SetRocketParent setRocketParent = rocket.GetComponent<SetRocketParent>();
        if (setRocketParent != null)
        {
            setRocketParent.SetParent(newParent);
            currentParent = newParent;

            // Display the planet info
            DisplayPlanetInfo(newParent);

            selectedPlanetText.text = "";

            // Play the theme for the new parent planet
            PlayCurrentPlanetTheme();
        }
        else
        {
            Debug.LogError("Rocket does not have SetRocketParent component.");
        }
    }

    private void DisplayPlanetInfo(GameObject planet)
    {
        PlanetInfoHolder planetInfoHolder = planet.GetComponent<PlanetInfoHolder>();
        if (planetInfoHolder != null)
        {
            PlanetInfo planetInfo = planetInfoHolder.planetInfo;
            if (planetInfoText != null && planetInfo != null)
            {
                string infoType = planet.name == "Sun" ? "Steaua" : "Planeta";
                planetInfoText.text = $"{infoType}: {planetInfo.planetName}\nInformatii:\n{string.Join("\n", planetInfo.facts)}";
                Debug.Log($"Displayed info for {planetInfo.planetName}");
            }
            else
            {
                Debug.LogError("PlanetInfo or planetInfoText is null.");
            }
        }
        else
        {
            Debug.LogError("Planet does not have PlanetInfoHolder component.");
        }
    }
}
