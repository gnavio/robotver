using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerseguir : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = goal.transform.position - this.transform.position;
        if (direction.magnitude < 40 && direction.magnitude > 28)
        {
            this.transform.LookAt(goal.transform.position);
            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
    }
}
