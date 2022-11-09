using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static Vector3 lastCheckpointPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            PlayerMovement.lastCheckPointPosition = transform.position;
        }
    }
}
