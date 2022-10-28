using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLookAtRobotver : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject bulletObj;
    public Transform transGun;
    public Transform gun;
    Vector3 direction;
    public float speed = 5f;

    void CalcularAngulo()
    {
        Vector3 robotForward = transform.up;
        Vector3 bulletDirection = bulletObject.transform.position - this.transform.position;

        Debug.DrawRay(this.transform.position, robotForward * 10, Color.green, 5);
        Debug.DrawRay(this.transform.position, bulletDirection, Color.red, 5);

        float dot = robotForward.x * bulletDirection.x + robotForward.y * bulletDirection.y;
        float angle = Mathf.Acos(dot / (robotForward.magnitude * bulletDirection.magnitude));
        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle: " + Vector3.Angle(robotForward, bulletDirection));
        this.transform.Rotate(0, angle * Mathf.Rad2Deg, 0);
    }
        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        direction = bulletObject.transform.position - this.transform.position;
        if (direction.magnitude < 15)
        {
            //CalcularAngulo();
            //Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            //this.transform.position = this.transform.position + velocity;
            Instantiate(bulletObj, gun.position, gun.rotation);
        }
    }
}
