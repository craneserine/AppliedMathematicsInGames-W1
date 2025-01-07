using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGoZone : MonoBehaviour
{
    [SerializeField] private float triggerDistance = 2f; // Distance for triggering effects
    [SerializeField] private float restartDistance = 1f; // Distance to restart the scene
    [SerializeField] private Color warningColor = Color.red; // Warning color
    [SerializeField] private float shakeIntensity = 0.1f; // Intensity of the shake

    private Renderer zoneRenderer;
    private Color originalColor;

    private void Start()
    {
        // Cache the renderer and original color
        zoneRenderer = GetComponent<Renderer>();
        if (zoneRenderer != null)
        {
            originalColor = zoneRenderer.material.color;
        }
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distanceX = transform.position.x - player.transform.position.x;
        float distanceY = transform.position.y - player.transform.position.y;
        float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        if (distance <= triggerDistance)
        {
            // Trigger no-go zone effects
            StartCoroutine(ShakeZone());

            // Restart the scene if the player is too close
            if (distance <= restartDistance)
            {
                PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
                if (playerBehavior != null)
                {
                    playerBehavior.RestartScene();
                }
            }
        }
    }

    private System.Collections.IEnumerator ShakeZone()
    {
        Vector3 originalPosition = transform.position;

        // Change to warning color
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = warningColor;
        }

        // Shake effect
        for (int i = 0; i < 10; i++)
        {
            transform.position = originalPosition + new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0f
            );
            yield return new WaitForSeconds(0.05f);
        }

        transform.position = originalPosition;

        // Reset color
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = originalColor;
        }
    }
}

