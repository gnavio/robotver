using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject player;
    public GameObject[] wayPoints;
    int currentWP = 0;
    public Transform gunBase;
    float rotSpeed = 2;
    float speed = 15;
    float moveSpeed = 3;
    float imunes = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * gunBase.forward;
    }
    float? RotateGun()
    {
        float? angle = CalculateAngle(true);
        if (angle != null)
        {
            gunBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = player.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude - 1;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);
        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 30)
        {
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
            float? angle = RotateGun();
            if (angle != null)
            {
                CreateBullet();
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, wayPoints[currentWP].transform.position) < 3)
            {
                currentWP++;
            }
            if(currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
            Quaternion lookWP = Quaternion.LookRotation(wayPoints[currentWP].transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookWP, Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, 3 * Time.deltaTime);
        }
       
    }

}
