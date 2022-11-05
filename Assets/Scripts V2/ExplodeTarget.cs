using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTarget : MonoBehaviour
{

    [SerializeField] public GameObject EnemyExplode;

    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        GameObject DieParticle = Instantiate(EnemyExplode);
        DieParticle.transform.position = transform.position;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(DieParticle);
        Destroy(gameObject);
    }
}
