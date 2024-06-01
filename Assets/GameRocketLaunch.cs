using System.Collections;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    // The name of the animation to play
    public string animationName;

    // Flag to ensure the animation plays only once
    private bool hasPlayed = false;

    // Reference to the third child object
    public GameObject thirdChild;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        // Set the initial speed to 0 to ensure the animation does not play immediately
        animator.speed = 0;
        // Check if the Animator component is found
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject");
        }

        // Hide the third child object at the beginning
        if (thirdChild != null)
        {
            thirdChild.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the 'R' key is pressed and the animation hasn't played yet
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!hasPlayed)
            {
                // Play the animation
                animator.speed = 0.5f;  // Set to 1 for normal speed
                animator.Play(animationName);
                hasPlayed = true;  // Set the flag to true
                Debug.Log("R key pressed: Playing animation");

                // Show the third child object
                if (thirdChild != null)
                {
                    thirdChild.SetActive(true);
                }
            }
        }
        // Check if the 'S' key is pressed to reset the flag and pause the animation
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Stop the current animation
            animator.StopPlayback();

            // Play the second animation
            animator.Play("aterizare");

            hasPlayed = false;  // Reset the flag
            Debug.Log("S key pressed: Stopping current animation and playing second animation");

            // Hide the third child object after 5 seconds
            if (thirdChild != null)
            {
                StartCoroutine(HideThirdChildAfterDelay(1.5f));
            }
        }
    }

    // Coroutine to hide the third child object after a delay
    private IEnumerator HideThirdChildAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (thirdChild != null)
        {
            thirdChild.SetActive(false);
        }
    }
}
