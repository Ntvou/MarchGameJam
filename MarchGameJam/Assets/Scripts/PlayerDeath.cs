using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float deathTime = 5.0f;

    private float timer = 0.0f;
    private bool isDead = false;

    public GameObject playerMovementObject;
    public GameObject playerScriptObject;

    private PlayerMovement playerMovement;
    private PlayerScript playerScript;

    private void Start()
    {
        playerMovement = playerMovementObject.GetComponent<PlayerMovement>();
        playerScript = playerScriptObject.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (isDead)
        {
            timer += Time.deltaTime;

            if (timer >= deathTime)
            {
                // Deactivate playerMovement and playerScript
                playerMovement.enabled = false;
                playerScript.enabled = false;

                // TODO: Handle death state
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            // Reset timer if another player enters the radius
            timer = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            // Start timer if a player leaves the radius
            timer = 0.0f;
            isDead = true;
        }
    }
}
