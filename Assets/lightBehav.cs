using UnityEngine;

public class lightBehav : MonoBehaviour
{
    public float maxIntensity = 10000f; // Adjust this value as needed

    private Light sunLight;

    private void Start()
    {
        sunLight = GetComponent<Light>();
    }

    private void Update()
    {
        // Calculate the distance from the light source to the GameObject
        float distance = Vector3.Distance(transform.position, sunLight.transform.position);

        // Calculate the intensity based on the distance
        float intensity = maxIntensity / Mathf.Pow(distance, 2f);

        // Clamp the intensity to avoid extremely high values
        intensity = Mathf.Clamp(intensity, 0f, maxIntensity);

        // Set the light intensity
        if (sunLight != null)
        {
            sunLight.intensity = intensity;
        }
        else
        {
            Debug.LogError("sunLight is null.");
        }
    }
}
