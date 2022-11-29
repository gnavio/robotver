using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarBala : MonoBehaviour
{
    private Constantes constantes;
    private ControlHabilidad controlHabilidad;
    private Habilidades habilidad;
    private String[] habilidades;
    private int posicion;
    private int NUM_HABILIDADES;
    private bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        constantes = new Constantes();
        habilidades = new string[] { constantes.IMPULSO, constantes.TELETRANSPORTE };
        habilidad = GetComponent<Habilidades>();
        controlHabilidad = GetComponent<ControlHabilidad>();
        NUM_HABILIDADES = habilidades.Length;
        posicion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CambioBala();
        DispararBala();
    }

    void CambioBala()
    {
        if (!controlHabilidad.changingHab)
        {
            controlHabilidad.NoHacerNada();
            reloading = GameObject.Find("MainCamera").GetComponent<RayShooter>().reloading;
            if (Input.GetAxis(constantes.MOUSE_SCROLLWHEEL) != 0 && !reloading)
            {
                String antiguo = habilidades[posicion];
                int cambio = Input.GetAxis(constantes.MOUSE_SCROLLWHEEL) > 0 ? 1 : -1;
                if (cambio < 0) cambio = NUM_HABILIDADES - 1;
                posicion = (posicion + cambio) % NUM_HABILIDADES;
                String nuevo = habilidades[posicion];
                controlHabilidad.CambioBala(antiguo, nuevo);
            }
        }
    }

    void DispararBala()
    {
        switch (habilidades[posicion])
        {
            case "Impulso":
                habilidad.Disparar(habilidades[posicion]);
                break;
            case "Teletransporte":
                habilidad.Disparar(habilidades[posicion]);
                break;
            default:
                throw new Exception("El string habilidad[posicion] no contiene ningún valor que corresponda a los tipos de bala");
        }
    }
}
