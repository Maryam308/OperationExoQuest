using UnityEngine;
using UnityEngine.UI;

public class RunningToIdleAnimation : MonoBehaviour
{
    private Animator animator;
    public Transform targetPosition;  // The final position where the character stops
    public float speed = 5f;          // Movement speed
    private bool isRunning = true;
    public static bool allIdle = false; // Track if all characters are idle
    public Button startButton; // Reference to the Start button

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }

        animator.SetBool("isRunning", true);
        startButton.interactable = false; // Disable Start button initially
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
            {
                isRunning = false;
                animator.SetBool("isRunning", false);
                CheckAllCharactersIdle();
            }
        }
    }

    private void CheckAllCharactersIdle()
    {
        RunningToIdleAnimation[] allCharacters = FindObjectsOfType<RunningToIdleAnimation>();

        foreach (var character in allCharacters)
        {
            if (character.isRunning) return; // If any character is still running, exit
        }

        allIdle = true;
        startButton.interactable = true; // Enable Start button when all are idle
    }
}
