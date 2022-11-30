using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarBala : MonoBehaviour
{
    private Constantes constantes;
    private ControlHabilidad controlHabilidad;
    private Habilidades habilidad;
    private String[] balas;
    private int posicion;
    private int NUM_HABILIDADES;
    public bool reloading, changing;

    // Start is called before the first frame update
    void Start()
    {
        constantes = new Constantes();
        balas = new string[] { constantes.IMPULSO, constantes.TELETRANSPORTE };
        habilidad = GetComponent<Habilidades>();
        controlHabilidad = GetComponent<ControlHabilidad>();
        NUM_HABILIDADES = balas.Length;
        posicion = 0;
        changing = false;
    }

    // Update is called once per frame
    void Update()
    {
        CambioBala();
        DispararBala();
    }

    IEnumerator CargandoBala()
    {
        changing = true;
        yield return new WaitForSeconds(1.5f);
        changing = false;
    }

    void CambioBala()
    {
        controlHabilidad.NoHacerNada();
        reloading = GameObject.Find("MainCamera").GetComponent<RayShooter>().reloading;
        if (Input.GetAxis(constantes.MOUSE_SCROLLWHEEL) != 0 && !reloading && !controlHabilidad.changingHab)
        {
            String antiguo = balas[posicion];
            int cambio = Input.GetAxis(constantes.MOUSE_SCROLLWHEEL) > 0 ? 1 : -1;
            if (cambio < 0) cambio = NUM_HABILIDADES - 1;
            posicion = (posicion + cambio) % NUM_HABILIDADES;
            String nuevo = balas[posicion];
            controlHabilidad.CambioBala(antiguo, nuevo);
            StartCoroutine(CargandoBala());
        }
    }

    void DispararBala()
    {
        switch (balas[posicion])
        {
            case "Impulso":
                habilidad.Disparar(balas[posicion]);
                break;
            case "Teletransporte":
                habilidad.Disparar(balas[posicion]);
                break;
            default:
                throw new Exception("El string habilidad[posicion] no contiene ningún valor que corresponda a los tipos de bala");
        }
    }
}
