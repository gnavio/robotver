using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject player;
    public float BulletTime;
    public Transform gunBase;
    float rotSpeed = 2;
    float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * gunBase.forward;
    }
    float? RotateGun()
    {
        float? angle = CalculateAngle(true);
        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
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
        float x = targetDir.magnitude;
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
            
        }
    }

    IEnumerator waiter()
    {
        float? angle = RotateGun();
        if (angle != null)
        {
            for (int i = 0; i < 100; i++)
            {
                CreateBullet();
                yield return new WaitForSecondsRealtime(BulletTime);
            }
        } 
    }

}
