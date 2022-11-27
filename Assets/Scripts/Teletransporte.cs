using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint;
    Rigidbody m_Rigidbody;
    [SerializeField] private Camera _camera;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;
    [SerializeField] Transform orientation;
    [SerializeField] Animator anim;
    [SerializeField] public GameObject OverlayPrefab;
    [SerializeField] public GameObject overlayPos;
    
    public int cartuchos = 1;

    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.T; //Añadir sistema para alternar entre Impulso y Teletransporte

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GameObject game = GameObject.FindGameObjectWithTag("BulletTP");
        if (game == null && Input.GetKeyDown(DashKey) && cartuchos > 0) 
        {
            GameObject teleportBullet = bullet;
            teleportBullet = GameObject.Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            teleportBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, yOffset, 0));
            teleportBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * zOffset);
        }
    }

    public void SumarCartucho(int x)
    {
        cartuchos += x;
    }
}
