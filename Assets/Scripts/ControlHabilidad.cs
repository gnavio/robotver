using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHabilidad : MonoBehaviour
{
    private Constantes constantes = new Constantes();
    [SerializeField] public Animator anim;
    [SerializeField] KeyCode CambiaHabKey = KeyCode.Q;
    [HideInInspector] public int habSelected;
    [SerializeField] AudioSource CambioHabAudio;
    [HideInInspector] public bool changingHab;
    private int numHabilidades = 2;
    private bool reloading;

    public void NoHacerNada()
    {
        anim.SetBool("CambioCartucho", false); // Por defecto desactivado, para que si m?s abajo lo activamos que se reproduzca la animaci?n una sola vez
    }
    /*
    void CambiaHabilidad()
    {
        anim.SetBool("CambioCartucho", false); // Por defecto desactivado, para que si m?s abajo lo activamos que se reproduzca la animaci?n una sola vez

        //Debug.Log(habSelected);

        if (Input.GetKeyUp(CambiaHabKey) && !reloading)
        {
            CambioHabAudio.Play(0);
            StartCoroutine(TiempoCambioHab()); // Para luego en el script de RayShooter cancelar poder disparar mientras cambia cartucho
            anim.SetBool("CambioCartucho", true);

            if(habSelected < numHabilidades)
            {
                habSelected += 1;
            }
            else
            {
                habSelected = 1;
            }

            if (habSelected == 1) // Cambiamos a Impulso
            {
                anim.SetBool("Luz_Morado", false);
                anim.SetBool("Luz_Azul", true);
            }

            if (habSelected == 2) // Cambiamos a Tele
            {
                anim.SetBool("Luz_Azul", false);
                anim.SetBool("Luz_Morado", true);
            }
        }
    }
    */

    IEnumerator TiempoCambioHab()
    {
        changingHab = true;
        CambioHabAudio.Play(0);
        yield return new WaitForSeconds(1.5f);
        changingHab = false;
    }

    public void CambioBala(string antigua, string nueva)
    {
        if (!changingHab)
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
                    break;
                case "Teletransporte":
                    luzNueva = constantes.LUZ_MORADO;
                    break;
                default:
                    throw new Exception("El string nueva no contiene ningún valor que corresponda a los tipos de bala");
            }
            anim.SetBool(constantes.CAMBIO_CARTUCHO, true); // Por defecto desactivado, para que si m?s abajo lo activamos que se reproduzca la animaci?n una sola vez
            StartCoroutine(TiempoCambioHab());
            anim.SetBool(luzAntigua, false);
            anim.SetBool(luzNueva, true);
        }
        else {
            int x = 1;
        }
    }
}
