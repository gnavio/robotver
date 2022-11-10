using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIExplode : MonoBehaviour
{

    [SerializeField] public GameObject EnemyExplode;

    public void ReactToHit()
    {
        Debug.Log("recibeImpacto");
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        GameObject DieParticle = Instantiate(EnemyExplode);
        DieParticle.transform.position = transform.position;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject.transform.Find("Eye"));
        yield return new WaitForSeconds(1);
        Destroy(DieParticle);
        Destroy(gameObject);
    }
}
