using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIExplode : MonoBehaviour
{

    [SerializeField] public GameObject EnemyExplode;

    public void ReactToHit()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        GameObject DieParticle = Instantiate(EnemyExplode);
        DieParticle.transform.position = transform.position;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject);
        gameObject.GetComponent<AIRayShoot>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(DieParticle);
        Destroy(gameObject);
    }
}
