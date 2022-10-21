using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{ 

    private int minValue, maxValue;
    private bool hasBeenCalculated;

    // Start is called before the first frame update
    void Start()
    {
        minValue = -4;
        maxValue = 4;
        hasBeenCalculated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenCalculated && !GetComponent<Renderer>().isVisible)
        {
            Debug.Log("Cambio de posición");
            transform.position = RandomPosition();
            hasBeenCalculated = true;
        }
        else if (GetComponent<Renderer>().isVisible)
        {
            hasBeenCalculated = false;
        }
    }

    private Vector3 RandomPosition()
    {
        int randomValue = Random.Range(minValue, maxValue);
        return new Vector3(randomValue, transform.position.y, transform.position.z);
    }
}
