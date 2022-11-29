using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidades : MonoBehaviour
{
    public readonly String IMPULSO = "Impulso"
        , TELETRANSPORTE = "Teletransporte"
        , MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";

    public bool impulso, teletransporte;
    private String[] habilidades;
    private int posicion;
    public int NUM_HABILIDADES;
    private ControlHabilidad controlHabilidad;

    //Impulso
    [SerializeField] Transform orientation;
    [SerializeField] Animator anim;
    [SerializeField] GameObject OverlayPrefab;
    [SerializeField] GameObject overlayPos;
    [SerializeField] AudioSource BlasterAudio;
    public float dash = 20f;
    public int cartuchosImpulso;
    private Rigidbody m_Rigidbody;
    [Header("Tecla")]
    [SerializeField] KeyCode DashKey = KeyCode.Mouse1;

    //Teletransporte
    public GameObject bullet;
    public GameObject firePoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;
    public int cartuchosTeletransporte;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        posicion = 0;
        habilidades = new string[] { IMPULSO, TELETRANSPORTE};
        controlHabilidad = new ControlHabilidad();
    }

    void CambiarPosicion()
    {
        if (Input.GetAxis(MOUSE_SCROLLWHEEL) != 0)
        {
            String antiguo = habilidades[posicion];
            int cambio = Input.GetAxis(MOUSE_SCROLLWHEEL) > 0 ? 1 : -1;
            if (cambio < 0) cambio = NUM_HABILIDADES - 1;
            posicion = (posicion + cambio) % NUM_HABILIDADES;
            String nuevo = habilidades[posicion];
            Debug.Log("Cambio de posicion: " + posicion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CambiarPosicion();
        //Inicio Impulso
        if (habilidades[posicion] == IMPULSO)
        {
            anim.SetBool("Impulso", false);

            if (Input.GetKeyDown(DashKey) && cartuchosImpulso >= 1)
            {
                BlasterAudio.Play(0);
                m_Rigidbody.AddForce(orientation.forward * -1 * dash);
                cartuchosImpulso -= 1;
                anim.SetBool("Impulso", true);
                Debug.Log("ImpulsoTrue");
                StartCoroutine(ImpulsoOverlay());
            }
        }
        //Fin Impulso

        //Inicio Teletransporte
        if (habilidades[posicion] == TELETRANSPORTE) { 
            GameObject game = GameObject.FindGameObjectWithTag("BulletTP");
            if (game == null && Input.GetKeyDown(DashKey) && cartuchosTeletransporte > 0)
            {
                cartuchosTeletransporte--;
                GameObject teleportBullet = bullet;
                teleportBullet = GameObject.Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                teleportBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, yOffset, 0));
                teleportBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * zOffset);
            }
        }
        //Fin Teletransporte
    }

    public void DispararImpulso()
    {
        anim.SetBool("Impulso", false);
        if (Input.GetKeyDown(DashKey) && cartuchosImpulso >= 1)
        {
            BlasterAudio.Play(0);
            m_Rigidbody.AddForce(orientation.forward * -1 * dash);
            cartuchosImpulso -= 1;
            anim.SetBool("Impulso", true);
            Debug.Log("ImpulsoTrue");
            StartCoroutine(ImpulsoOverlay());
        }
    }

    public void DispararTeletransporte()
    {
        if (habilidades[posicion] == TELETRANSPORTE)
        {
            GameObject game = GameObject.FindGameObjectWithTag("BulletTP");
            if (game == null && Input.GetKeyDown(DashKey) && cartuchosTeletransporte > 0)
            {
                cartuchosTeletransporte--;
                GameObject teleportBullet = bullet;
                teleportBullet = GameObject.Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                teleportBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, yOffset, 0));
                teleportBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * zOffset);
            }
        }
    }

    public void Disparar(String balas)
    {
        switch (balas)
        {
            case "Impulso":
                DispararImpulso();
                break;
            case "Teletransporte":
                DispararTeletransporte();
                break;
            default:
                throw new Exception("El string bala no contiene ningún valor que corresponda a los tipos de bala");
        }
    }

    public void SumarCartuchoImpulso(int x)
    {
        cartuchosImpulso += x;
    }

    public void SumarCartuchoTeletransporte(int x)
    {
        cartuchosTeletransporte += x;
    }

    IEnumerator ImpulsoOverlay()
    {
        GameObject overlay = Instantiate(OverlayPrefab, overlayPos.transform.position, overlayPos.transform.rotation);
        overlay.transform.parent = overlayPos.transform;
        yield return new WaitForSeconds(1);
        Destroy(overlay);
    }
}
