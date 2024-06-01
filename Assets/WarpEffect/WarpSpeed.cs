using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class WarpSpeed : MonoBehaviour
{
    public VisualEffect warpSpeedVFX;
    public float rate = 0.02f;
    private bool warpActive;

    public CanvasGroup canvasGroup;  // Reference to the CanvasGroup component
    public float fadeRate = 0.1f;   // Rate at which the CanvasGroup fades in

    void Start()
    {
        warpSpeedVFX.Stop();
        warpSpeedVFX.SetFloat("WarpAmount", 0);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;  // Ensure the canvas starts fully transparent
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !warpActive)
        {
            StartCoroutine(ActivateWarpEffect());
        }
    }

    IEnumerator ActivateWarpEffect()
    {
        warpActive = true;

        // Start the warp-in effect
        StartCoroutine(ActivateParticles());

        // Wait for 5 seconds
        yield return new WaitForSeconds(3f);

        // Manage the canvas
        StartCoroutine(ManageCanvas());

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Start the fade-out effect
        StartCoroutine(FadeOutCanvas());
    }

    IEnumerator ManageCanvas()
    {
        float timer = 0f;

        // Fade in the canvas
        while (timer < 1.5f)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / 1.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        timer = 0f;

        // Fade out the canvas
        while (timer < 1.5f)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / 1.5f);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ActivateParticles()
    {
        warpSpeedVFX.Play();
        float timer = 0f;
        float amount = 0f;

        while (timer < 5f && warpActive)
        {
            amount = Mathf.Lerp(0f, 1f, timer / 2f);
            warpSpeedVFX.SetFloat("WarpAmount", amount);
            timer += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator FadeOutCanvas()
    {
        if (canvasGroup == null) yield break;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeRate * Time.deltaTime;
            yield return null;
        }

        warpSpeedVFX.Stop();
        warpSpeedVFX.SetFloat("WarpAmount", 0f); // Ensure warp amount is reset
        warpActive = false; // Reset warp state
    }
}
