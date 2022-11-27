using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRayShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject turret;
    private GameObject player;
    public float BulletTime;
    public Transform gunBase;
    float rotSpeed = 2;
    float speed = 15;
    private bool isReachable;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        player = GameObject.FindGameObjectWithTag("Player");
        isReachable = false;

    }
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * gunBase.forward;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 30)
        {
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
            isReachable = true;
            gunBase.transform.LookAt(player.transform.position);
        }
        else isReachable = false;

    }

    IEnumerator waiter()
    {
        
        for (int i = 0; i < 100; i++)
        {
            if (isReachable)
            {
                CreateBullet();
            }
            yield return new WaitForSecondsRealtime(BulletTime);
        }
        
    }
}
