using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartuchoItem : MonoBehaviour
{
    public int cartuchosSumados = 1;
    [SerializeField] GameObject player;
    private Impulso impulso;

    void Start()
    {
        impulso = player.GetComponent<Impulso>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Estamos dentro");

            impulso.SumarCartucho(cartuchosSumados);
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
