using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolling : MonoBehaviour
{
    private GameObject player;
    public GameObject[] waypoints;
    int currentWP;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position)<3)
        {
            currentWP++;
        }
        if (currentWP >= waypoints.Length)
            currentWP = 0;

        if (Vector3.Distance(this.transform.position, player.transform.position) > 30)
        {
            Quaternion lookWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookWP, Time.deltaTime * speed);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
            
    }
}
