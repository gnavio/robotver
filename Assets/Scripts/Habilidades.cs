using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Habilidades : MonoBehaviour
{
    private CambiarBala cambiarBala;
    //Impulso
    [SerializeField] Transform orientation;
    [SerializeField] Animator anim;
    [SerializeField] GameObject OverlayPrefab;
    [SerializeField] GameObject overlayPos;
    [SerializeField] AudioSource BlasterAudio;
    public float dash = 20f;
    public int cartuchosImpulso;
    [SerializeField] public TMPro.TMP_Text cartuchosImpulsoTxt;
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
    [SerializeField] public TMPro.TMP_Text cartuchosTeletransporteTxt;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        cambiarBala = GetComponent<CambiarBala>();
    }

    /*
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
        //Asociar variables de cartuchos a texto UI
        cartuchosImpulsoTxt.text = cartuchosImpulso.ToString("0");
        cartuchosTeletransporteTxt.text = cartuchosTeletransporte.ToString("0");

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
    */

    public void Disparar(String balas)
    {
        anim.SetBool("Impulso", false);
        if (Input.GetKeyDown(DashKey))
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
    }

    private void DispararImpulso()
    {
        if (cartuchosImpulso >= 1)
        {
            BlasterAudio.Play(0);
            m_Rigidbody.AddForce(orientation.forward * -1 * dash);
            cartuchosImpulso -= 1;
            anim.SetBool("Impulso", true);
            Debug.Log("ImpulsoTrue");
            StartCoroutine(ImpulsoOverlay());
        }
    }
    

    private void DispararTeletransporte()
    {
        GameObject game = GameObject.FindGameObjectWithTag("BulletTP");
        if (game == null && cartuchosTeletransporte > 0)
        {
            cartuchosTeletransporte--;
            GameObject teleportBullet = bullet;
            teleportBullet = GameObject.Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            teleportBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, yOffset, 0));
            teleportBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * zOffset);
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
