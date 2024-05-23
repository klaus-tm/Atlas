using UnityEngine;

public class lightBehav : MonoBehaviour
{
    public float maxIntensity = 10000f; // Adjust this value as needed
    public float minIntensity = 0f; // Minimum intensity, if needed
    public float maxDistance = 100f; // Adjust based on your scene's scale

    private Light sunLight;

    private void Start()
    {
        sunLight = GetComponent<Light>();
        if (sunLight != null)
        {
            sunLight.range = maxDistance; // Ensure the light range covers the required distance
        }
    }

    private void Update()
    {
        if (sunLight == null)
        {
            Debug.LogError("sunLight is null.");
            return;
        }

        // Calculate the distance from the light source to the GameObject
        float distance = Vector3.Distance(transform.position, sunLight.transform.position);

        // Calculate the intensity based on the distance, using a linear relationship
        float intensity = Mathf.Lerp(maxIntensity, minIntensity, distance / maxDistance);


        // Set the light intensity
        sunLight.intensity = intensity;
    }
}
