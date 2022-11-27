using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartuchoImpulsoItem : MonoBehaviour
{
    public int cartuchosSumados = 1;
    [SerializeField] GameObject collectParticles;
    private Habilidades habilidades;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        habilidades = player.GetComponent<Habilidades>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Estamos dentro");
            habilidades.SumarCartuchoImpulso(cartuchosSumados);

            GameObject CollectEffect = Instantiate(collectParticles);
            CollectEffect.transform.position = GameObject.Find("Player").transform.position;
            CollectEffect.transform.parent = GameObject.Find("Player").transform;

            Destroy(this.gameObject);
        }
    }
}
