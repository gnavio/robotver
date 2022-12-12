using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalNivel : MonoBehaviour
{
    [SerializeField] string nivel;
    bool confirmacionDisponible = false;

    [SerializeField] KeyCode teclaTeleport = KeyCode.E;

    [SerializeField] GameObject MensajeConfirmacion;
    //SceneManager.LoadScene(scenename);
    // Start is called before the first frame update
    void Start()
    {
        MensajeConfirmacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmacionDisponible)
        {
            MensajeConfirmacion.SetActive(true);
            if(Input.GetKeyUp(teclaTeleport))
            {
                SceneManager.LoadScene(nivel);
            }
        } 
        else { MensajeConfirmacion.SetActive(false); }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            confirmacionDisponible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            confirmacionDisponible = false;
        }
    }
}
