using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    
    GameManager gameManager;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {   
            // Código antiguo (checkpoints)
            //GameObject.FindGameObjectWithTag("Player").transform.position = PlayerMovement.lastCheckPointPosition;

            // Método GameOver de GameManager (nuevo código)
            gameManager.GameOver();
        }
    }
}
