using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityRadius : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public float radius = 5f;
    public float timeLimit = 10f;
    public Animator animator;

    private float timer = 0f;
    private bool isPlayerInRange = false;
    private PlayerMovement playerMovement;
    private PlayerScript playerScript;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (!isPlayerInRange)
        {
            timer += Time.deltaTime;
            Debug.Log("Insanity timer: " + timer);

            if (timer >= timeLimit)
            {
                DeactivateScripts();
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void DeactivateScripts()
    {
        Debug.Log(gameObject.name + " died due to insanity.");
        animator.SetBool("IsDead", false);
        // Deactivate PlayerMovement and PlayerScript components
        playerMovement.enabled = false;
        playerScript.enabled = false;
        Invoke("InvokeOnPlayerDeath", 3f); // invoke after 2 seconds delay
    }

    private void InvokeOnPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
