using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBullet : MonoBehaviour
{
    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.T;
    [SerializeField] float maxTime;
    private float currentTime;

    private void Start()
    {
        currentTime = 0f;
    }

    /**
    private void Update()
    {
        
        float angle = _Angle * Mathf.Deg2Rad;
        Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        ray = _camera.ScreenPointToRay(point);
        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));
        if (Input.GetKeyDown(DashKey))
        {
            //m_Rigidbody.position = hit.point;
            Destroy(gameObject);
        }
        
    }

    IEnumerator Coroutine_Movement(float v0, float angle)
    {
        float t = 0;
        while (t < 100)
        {            
            float x = v0 * t * Mathf.Cos(angle) * ray.direction.x;
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -_gravity * Physics.gravity.y * Mathf.Pow(t, 2) * ray.direction.y;
            float z = v0 * t * Mathf.Cos(angle) * ray.direction.z;
            transform.position = _FirePoint.position + new Vector3(x, y, z);
            t += Time.deltaTime;
            yield return null;
        }
    }
    **/

    private void Update()
    {
        if (Input.GetKeyDown(DashKey) || currentTime >= maxTime)
        {
            Teleport();
        }
        currentTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Teleport();
    }

    private void Teleport()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position + new Vector3(0, 0, 0);
        Destroy(gameObject);
    }
}
