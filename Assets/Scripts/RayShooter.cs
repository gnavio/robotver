using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RayShooter : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] public GameObject HitPrefab;
    [SerializeField] public GameObject overlayReload;
    [SerializeField] public GameObject OverlayPrefab;
    [SerializeField] public GameObject overlayPos;
    [SerializeField] public GameObject BonusPrefab;

    public Texture2D mirilla;

    public int balas = 6;
    [SerializeField] public TMPro.TMP_Text balasText;

    private Camera _camera;

    bool reloading = false;

    bool ocultaOverlay = false;

    [Header("Tecla")]
    [SerializeField] KeyCode ReloadKey = KeyCode.R;

    void Start()
    {

        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked; // deja el rat�n en el centro de la ventana
        Cursor.visible = false;
    }
    void Update()
    {
        anim.SetBool("Dispara", false);

        BalasUI();
        ReloadOverlay();
        
        if (Input.GetKeyDown(ReloadKey) && balas < 6)
        {
            StartCoroutine(Reload()); 
        }

        if (Input.GetMouseButtonDown(0) && balas >= 1 && !reloading)
        {
            anim.SetBool("Dispara", true);
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Hit " + hit.point + " (" + hit.transform.gameObject.name + ")");
                balas--;
                StartCoroutine(ShotOverlay());

                GameObject hitObject = hit.transform.gameObject;
                AIExplode targetAI = hitObject.GetComponent<AIExplode>();
                ExplodeTarget target = hitObject.GetComponent<ExplodeTarget>();



                if (targetAI != null)
                {
                    targetAI.ReactToHit();
                    //Debug.Log("Take that!");
                }

                if (target != null) 
                {
                    GameObject.Find("Canvas").GetComponent<TimerCampoTiro>().timerActivado = true;
                    GameObject.Find("Canvas").GetComponent<TimerCampoTiro>().score += GameObject.Find("Canvas").GetComponent<TimerCampoTiro>().killScoreBonus;
                    //Debug.Log("BonusInstance");
                    StartCoroutine(BonusOverlay());

                    target.ReactToHit();
                    Debug.Log("Take that!");
                }

                else
                {
                    if (hit.transform.gameObject.CompareTag("Impacto") == false)
                    {
                        //StartCoroutine(SphereIndicator(hit.point));
                        
                        StartCoroutine(HitPrefabIndicator(hit));
                        
                    }
                }
            }
        }
    }

    IEnumerator HitPrefabIndicator(RaycastHit hit)
    {
        GameObject InstanceHit = Instantiate(
                HitPrefab,
                hit.point + (hit.normal * 0.1f),
                Quaternion.FromToRotation(Vector3.up, hit.normal)
                );

        yield return new WaitForSeconds(1);
        Destroy(InstanceHit);
    }

    IEnumerator BonusOverlay()
    {
        Debug.Log("BonusInstance");
        GameObject BonusInstance = Instantiate(BonusPrefab);
        BonusInstance.transform.parent = GameObject.Find("MainCamera").transform;
        BonusInstance.transform.position = GameObject.Find("BonusOverlayPos").transform.position; // posicionar bonus en el centro del canvas

        yield return new WaitForSeconds(2);
        Destroy(BonusInstance);
    }


    IEnumerator ShotOverlay()
    {
        GameObject overlay = Instantiate(OverlayPrefab, overlayPos.transform.position, overlayPos.transform.rotation);
        overlay.transform.parent = overlayPos.transform;
        yield return new WaitForSeconds(1);
        Destroy(overlay);
    }


    IEnumerator Reload()
    {
        anim.SetBool("Reloading", true);
        reloading = true;
        yield return new WaitForSeconds(2.4f);
        balas = 6;
        anim.SetBool("Reloading", false);
        reloading = false;
    }
    /*
        IEnumerator SphereIndicator(Vector3 pos)
        {
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject sphere = Instantiate(HitPrefab);
            sphere.transform.position = pos;
            sphere.gameObject.tag = "Impacto";

            yield return new WaitForSeconds(1);
            Destroy(sphere);
        }
    
    */
        void OnGUI()
    { // se ejecuta despu�s de dibujar el frame del juego
        int size = 30;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), mirilla); // puede mostrar texto e im�genes //"*"
    }


    void BalasUI()
    {
        balasText.text = balas.ToString("0" + " / ∞");
    }

    void ReloadOverlay()
    {
        if (balas == 0 && !ocultaOverlay) // mostrar Overlay Reload [R]
        {
            overlayReload.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ocultaOverlay = true;   // esta variable sirve para ocultar el overlay nada mas pulsar R, no espera a que cambie el num de balas
        }

        if (balas > 0 || ocultaOverlay)
        {
            overlayReload.SetActive(false);
        }

        if (balas > 0)
        {
            ocultaOverlay = false;
        }
    }

}