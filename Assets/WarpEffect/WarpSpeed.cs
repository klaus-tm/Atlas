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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            warpActive = true;
            StartCoroutine(ActivateParticles());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            warpActive = false;
            StartCoroutine(ActivateParticles());
        }
    }

    IEnumerator ActivateParticles()
    {
        if (warpActive)
        {
            warpSpeedVFX.Play();
            float amount = warpSpeedVFX.GetFloat("WarpAmount");
            while (amount < 1 && warpActive)
            {
                amount += rate;
                warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);
            }

            if (amount >= 1)
            {
                StartCoroutine(FadeInCanvas());
            }
        }
        else
        {
            float amount = warpSpeedVFX.GetFloat("WarpAmount");
             
            StartCoroutine(FadeOutCanvas());
             
            while (amount > 0 && !warpActive)
            {
                amount -= rate;
                warpSpeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= rate + 0)
                {
                    amount = 0;
                    warpSpeedVFX.SetFloat("WarpAmount", amount);
                    warpSpeedVFX.Stop();
                }
            }
        }
    }

    IEnumerator FadeInCanvas()
    {
        if (canvasGroup == null) yield break;

        while (canvasGroup.alpha < 1)
        {
            Debug.Log( canvasGroup.alpha);
            canvasGroup.alpha += fadeRate;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOutCanvas()
    {
        if (canvasGroup == null) yield break;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeRate;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
