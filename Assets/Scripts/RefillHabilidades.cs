using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillHabilidades : MonoBehaviour
{
    public bool SumarImpulsos;
    public bool SumarTeleports;

    Habilidades habilidades;

    public int cantidadMax = 20;

    void Start()
    {
        habilidades = GameObject.Find("Player").GetComponent<Habilidades>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SumarImpulsos)
            {
                habilidades.cartuchosImpulso = cantidadMax;
            }

            if (SumarTeleports)
            {
                habilidades.cartuchosTeletransporte = cantidadMax;
            }
        }
    }
}
