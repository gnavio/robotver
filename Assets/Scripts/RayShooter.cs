using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayShooter : MonoBehaviour
{

    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // deja el ratón en el centro de la ventana
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Hit " + hit.point + " (" + hit.transform.gameObject.name + ")");

                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                    Debug.Log("Take that!");
                }
                else
                {
                    if (hit.transform.gameObject.CompareTag("Impacto") == false)
                    {
                        StartCoroutine(SphereIndicator(hit.point));
                    }
                }
            }
        }
    }

        IEnumerator SphereIndicator(Vector3 pos)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos;
            sphere.gameObject.tag = "Impacto";

            yield return new WaitForSeconds(1);
            Destroy(sphere);
        }
    

        void OnGUI()
    { // se ejecuta después de dibujar el frame del juego
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*"); // puede mostrar texto e imágenes
    }

}