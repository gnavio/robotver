using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartuchoItem : MonoBehaviour
{
    public int cartuchosSumados = 1;
    [SerializeField] GameObject collectParticles;
    private Impulso impulso;


    void Start()
    {
        GameObject player = GameObject.Find("Player");
        impulso = player.GetComponent<Impulso>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Estamos dentro");

            impulso.SumarCartucho(cartuchosSumados);

            GameObject CollectEffect = Instantiate(collectParticles);
            CollectEffect.transform.position = GameObject.Find("Player").transform.position;
            CollectEffect.transform.parent = GameObject.Find("Player").transform;

            Destroy(this.gameObject);
        }
}
        /*
private void OnTriggerEnter(Collider other)
{

   if(other.CompareTag("Player"))
   {
       Impulso player = other.GetComponent<Impulso>();
       Debug.Log("Estamos dentro");

       //if (player != null)
       //{
           player.SumarCartucho(cartuchosSumados);
           Destroy(this.gameObject);
      // }
   }

}
*/

}
