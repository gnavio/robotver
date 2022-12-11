using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHabilidad : MonoBehaviour
{
    private Constantes constantes = new Constantes();
    private CambiarBala cambiarBala;
    [SerializeField] public Animator anim;
    [HideInInspector] public int habSelected;
    [SerializeField] AudioSource CambioHabAudio;
    [HideInInspector] public bool changingHab;

    [Header("HUD Manager")]
    [SerializeField] GameObject ImpulseReady;
    [SerializeField] GameObject TeleportReady;
    [SerializeField] GameObject ImpulseOFF;
    [SerializeField] GameObject TeleportOFF;
    int cartuchosImpulso;
    int cartuchosTeletransporte;
    bool impulseActivado = true;
    bool teleportActivado = false;

    void Start()
    {
        cambiarBala = GetComponent<CambiarBala>();
    }
    void Update()
    {
        HUDUpdater();
    }

    
    public void NoHacerNada()
    {
        anim.SetBool("CambioCartucho", false); // Por defecto desactivado, para que si m?s abajo lo activamos que se reproduzca la animaci?n una sola vez
    }

    public void CambioBala(string antigua, string nueva)
    {
        if (!changingHab && !cambiarBala.changing)
        {
            string luzAntigua = "", luzNueva = "";
            switch (antigua)
            {
                case "Impulso":
                    luzAntigua = constantes.LUZ_AZUL;
                    break;
                case "Teletransporte":
                    luzAntigua = constantes.LUZ_MORADO;
                    break;
                default:
                    throw new Exception("El string antigua no contiene ningún valor que corresponda a los tipos de bala");
            }
            switch (nueva)
            {
                case "Impulso":
                    luzNueva = constantes.LUZ_AZUL;
                    //ImpulseReady.SetActive(true);
                    impulseActivado = true;
                    teleportActivado = false;
                    break;
                case "Teletransporte":
                    luzNueva = constantes.LUZ_MORADO;
                    //TeleportReady.SetActive(true);
                    teleportActivado = true;
                    impulseActivado = false;
                    break;
                default:
                    throw new Exception("El string nueva no contiene ningún valor que corresponda a los tipos de bala");
            }
            anim.SetBool(constantes.CAMBIO_CARTUCHO, true); // Por defecto desactivado, para que si m?s abajo lo activamos que se reproduzca la animaci?n una sola vez
            StartCoroutine(TiempoCambioHab());
            anim.SetBool(luzAntigua, false);
            anim.SetBool(luzNueva, true);
        }
    }

    IEnumerator TiempoCambioHab()
    {
        changingHab = true;
        CambioHabAudio.Play(0);
        yield return new WaitForSeconds(constantes.TIEMPO_DURACION_CAMBIO_BALA);
        changingHab = false;
    }

    void HUDUpdater()
    {
        cartuchosImpulso = GetComponent<Habilidades>().cartuchosImpulso;
        cartuchosTeletransporte = GetComponent<Habilidades>().cartuchosTeletransporte;

        if (impulseActivado)
        {
            if (cartuchosImpulso < 1)
            {
                //Debug.Log("No quedan Impulsos");
                ImpulseReady.SetActive(false);
                ImpulseOFF.SetActive(true);
            }
            else
            {
                ImpulseReady.SetActive(true);
                ImpulseOFF.SetActive(false);
            }
        }
        else
        {
            ImpulseReady.SetActive(false);
            ImpulseOFF.SetActive(false);
        }

        if (teleportActivado)
        {
            if (cartuchosImpulso < 1)
            {
                //Debug.Log("No quedan Teleports");
                TeleportReady.SetActive(false);
                TeleportOFF.SetActive(true);
            }
            else
            {
                TeleportReady.SetActive(true);
                TeleportOFF.SetActive(false);
            }
        }
        else
        {
            TeleportReady.SetActive(false);
            TeleportOFF.SetActive(false);
        }
    }
}
