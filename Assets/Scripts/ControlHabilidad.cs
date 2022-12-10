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

    void Start()
    {
        cambiarBala = GetComponent<CambiarBala>();
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
    }

    IEnumerator TiempoCambioHab()
    {
        changingHab = true;
        CambioHabAudio.Play(0);
        yield return new WaitForSeconds(constantes.TIEMPO_DURACION_CAMBIO_BALA);
        changingHab = false;
    }
}
