using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private Transform finishZone; // Finish zone position
    [SerializeField] private GameObject winUI; // UI to show when player wins

    private float moveX = 0f; // Horizontal movement
    private float moveY = 0f; // Vertical movement

    private void Start()
    {
        if (winUI != null)
        {
            winUI.SetActive(false); // Hide the Win UI initially
        }
    }

    private void Update()
    {
        HandleMovement();
        CheckFinishZone();
    }

    private void HandleMovement()
    {
        // Reset movement
        moveX = 0f;
        moveY = 0f;

        // Get input for movement
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Normalize movement to prevent faster diagonal movement
        float magnitude = Mathf.Sqrt((moveX * moveX) + (moveY * moveY));
        if (magnitude > 1f)
        {
            moveX /= magnitude;
            moveY /= magnitude;
        }

        // Apply movement
        transform.position = new Vector3(
            transform.position.x + moveX * speed * Time.deltaTime,
            transform.position.y + moveY * speed * Time.deltaTime,
            transform.position.z
        );
    }

    private void CheckFinishZone()
    {
        if (finishZone == null) return;

        // Calculate distance to the finish zone
        float distanceX = finishZone.position.x - transform.position.x;
        float distanceY = finishZone.position.y - transform.position.y;
        float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        if (distance <= 1f)
        {
            // Show win UI
            if (winUI != null)
            {
                winUI.SetActive(true);
            }

            // Stop player movement
            enabled = false;
        }
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
