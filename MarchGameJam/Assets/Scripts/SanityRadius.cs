using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityRadius : MonoBehaviour
{
    public float radius = 5f;
    public float timeLimit = 10f;

    private float timer = 0f;
    private bool isControlledByPlayer = false;

    private PlayerMovement playerMovement;
    private PlayerScript playerScript;

    private void Start()
    {
        // Get the PlayerMovement and PlayerScript components from the same GameObject
        playerMovement = GetComponent<PlayerMovement>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        CheckSanity();
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
                DisableScripts();
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void DisableScripts()
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
