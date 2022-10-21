using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public int timeInterval;
    public float speed, increment, movement, jump;

    private bool isOnGround, isOnWall;
    private string objectCollision;
    private Vector3 startPoint;
    private Transform transformEnterCollision,
        transformExitCollision;
    private Vector3 force;

    void Start()
    {
        Debug.Log("Character created.");
        startPoint = transform.position;
        isOnGround = true;
        isOnWall = false;
        SpeedUpHandler();
        InvokeRepeating("SpeedUpHandler",timeInterval, timeInterval);
        force = new(movement * Time.deltaTime, 0, 0);
    }

    void Update()
    {
        GroundCollisionerHandler();
        WallCollisionerHandler();
        MoveObjectHandler();
        StartPointHandler();
        WallRunHandler();
    }


    private void MoveObjectHandler()
    {
        Vector3 force = new(0, 0, speed * -1 * Time.deltaTime);
        rigidbody.AddForce(force);
        MoveHandler();
    }

    private void StartPointHandler()
    {
        double zOffset = transform.position.z;
        if (zOffset < -10) ReturnToStartPoint();
    }

    private void SpeedUpHandler()
    {
        speed = speed + 1 - (increment / Mathf.Pow(1.3f, speed));
    }

    void ReturnToStartPoint()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, startPoint.z);
    }

    void MoveHandler()
    {
        force = new(movement * Time.deltaTime, 0, 0);
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            force.Set(-force.x, force.y, force.z);
            rigidbody.AddForce(force);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rigidbody.AddForce(force);
        }
        if (isOnGround && Input.GetKey("space"))
        {
            Debug.Log("Salto: " + isOnGround + ", " + isOnWall);
            Vector3 jumpVector = new(0, jump * Time.deltaTime, 0);
            rigidbody.AddForce(jumpVector);
        }
    }

    private void WallCollisionerHandler()
    {
        isOnWall = IsInCollisionWith("Wall");
    }

    private void GroundCollisionerHandler()
    {
        isOnGround = IsInCollisionWith("Ground");
    }
    
    private bool IsInCollisionWith(string tag)
    {
        Debug.Log("IsOnGround: " + isOnGround + ", IsOnWall: " + isOnWall);
        if (transformExitCollision == null)
        {
            if (tag.Equals("Ground")) return true;
            else { return false; }
        }
        if (transformEnterCollision.CompareTag(tag) && objectCollision.Equals("Entered"))
        {
            return true;
        }
        else return false;
    }


    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        transformEnterCollision = collision.transform;
        objectCollision = "Entered";
    }

    private void OnCollisionExit(Collision collision)
    {
        transformExitCollision = collision.transform;
        objectCollision = "Exited";
    }

    private void WallRunHandler()
    {
        if (isOnWall)
        {
            Debug.Log("Gravedad desactivada");
            rigidbody.useGravity = false;
        }
        else
        {
            Debug.Log("Gravedad activada");
            rigidbody.useGravity = true;
            
        }
    }
}


