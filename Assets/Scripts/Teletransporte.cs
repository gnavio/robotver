using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public GameObject bullet;
    Rigidbody m_Rigidbody;
    [SerializeField] private Camera _camera;

    [SerializeField] Transform orientation;
    [SerializeField] Animator anim;
    [SerializeField] public GameObject OverlayPrefab;
    [SerializeField] public GameObject overlayPos;

    public int cartuchos = 30;

    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.T; //Añadir sistema para alternar entre Impulso y Teletransporte

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        anim.SetBool("Impulso", false);
        if (Input.GetKeyDown(DashKey) && cartuchos >= 1) 
        {   
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_Rigidbody.position = hit.point; //Teletransporte
            }
            
            cartuchos -= 1;
            anim.SetBool("Impulso", true);
            Debug.Log("ImpulsoTrue");
            StartCoroutine(ImpulsoOverlay());
        }
        
    }

    public void SumarCartucho(int x)
    {
        cartuchos += x;
    }

    IEnumerator ImpulsoOverlay()
    {
        GameObject overlay = Instantiate(OverlayPrefab, overlayPos.transform.position, overlayPos.transform.rotation);
        overlay.transform.parent = overlayPos.transform;
        yield return new WaitForSeconds(1);
        Destroy(overlay);
    }
}
