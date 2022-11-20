using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBullet : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.T;
    [SerializeField] float _InitialVelocity;
    [SerializeField] float _Angle;
    [SerializeField] float _gravity;
    [SerializeField] LineRenderer _Line;
    [SerializeField] float _Step;
    [SerializeField] Transform _FirePoint;
    public int cartuchos = 30;
    private Ray ray;

    private void Update()
    {
        float angle = _Angle * Mathf.Deg2Rad;
        DrawPath(_InitialVelocity, angle, _Step);
        if (Input.GetKeyDown(DashKey) && cartuchos > 0)
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            ray = _camera.ScreenPointToRay(point);
            StopAllCoroutines();
            StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));
        }
    }

    private void DrawPath(float v0, float angle, float step)
    {
        step = Mathf.Max(0.01f, step);
        float totalTime = 10;
        _Line.positionCount = (int)(totalTime / step) + 2;
        int count = 0;
        for (float i = 0; i < totalTime; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - _gravity * -Physics.gravity.y * Mathf.Pow(i, 2);
            float z = v0 * i * Mathf.Cos(angle);
            _Line.SetPosition(count, _FirePoint.position + new Vector3(x, y, z));
            count++;
        }
        float xfinal = v0 * totalTime * Mathf.Cos(angle);
        float yfinal = v0 * totalTime * Mathf.Sin(angle) - _gravity * -Physics.gravity.y * Mathf.Pow(totalTime, 2);
        float zfinal = v0 * totalTime * Mathf.Cos(angle);
        _Line.SetPosition(count, _FirePoint.position + new Vector3(xfinal, yfinal, zfinal));
    }

    IEnumerator Coroutine_Movement(float v0, float angle)
    {
        float t = 0;
        while (t < 100)
        {            
            float x = v0 * t * Mathf.Cos(angle) * ray.direction.x;
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2) * ray.direction.y;
            float z = v0 * t * Mathf.Cos(angle) * ray.direction.z;
            transform.position = _FirePoint.position + new Vector3(x, y, z);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
