using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityRadius : MonoBehaviour
{
    public float radius = 5f;
    public float timeLimit = 10f;
    public bool isControlledByPlayer;

    private float timer = 0f;

    private PlayerDeath playerDeath;
    private PlayerMovement playerMovement;
    private PlayerScript playerScript;

    private void Start()
    {
        // Get the PlayerDeath, PlayerMovement, and PlayerScript components from the same GameObject
        playerDeath = GetComponent<PlayerDeath>();
        playerMovement = GetComponent<PlayerMovement>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (isControlledByPlayer)
        {
            CheckSanity();
        }
    }

    private void CheckSanity()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        bool isOtherPlayerInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.gameObject.CompareTag("Player"))
            {
                isOtherPlayerInRange = true;
                break;
            }
        }

        if (!isOtherPlayerInRange)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                Die();
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died due to insanity.");

        // Deactivate PlayerMovement and PlayerScript components
        playerMovement.enabled = false;
        playerScript.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
