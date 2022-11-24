using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHabilidad : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] KeyCode CambiaHabKey = KeyCode.Q;

    [HideInInspector] public bool changingHab;

    int numHabilidades = 2;

    bool reloading;

    [HideInInspector] public int habSelected;

    void Start()
    {
        habSelected = 1;
    }

    void Update()
    {
        reloading = GameObject.Find("MainCamera").GetComponent<RayShooter>().reloading;
        CambiaHabilidad();
    }

    void CambiaHabilidad()
    {
        anim.SetBool("CambioCartucho", false); // Por defecto desactivado, para que si más abajo lo activamos que se reproduzca la animación una sola vez

        Debug.Log(habSelected);

        if (Input.GetKeyUp(CambiaHabKey) && !reloading)
        {
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

    IEnumerator TiempoCambioHab()
    {
        changingHab = true;
        yield return new WaitForSeconds(1f);
        changingHab = false;
    }
}
