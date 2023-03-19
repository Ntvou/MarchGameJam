using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool isControllingPlayer1 = false;

    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;

    private void Start()
    {
        player1Movement = player1.GetComponent<PlayerMovement>();
        player2Movement = player2.GetComponent<PlayerMovement>();
        
        player1Movement.enabled = false;
        player2Movement.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isControllingPlayer1 = !isControllingPlayer1;
            player1Movement.enabled = isControllingPlayer1;
            player2Movement.enabled = !isControllingPlayer1;
        }
    }
}
