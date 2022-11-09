using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTarget : MonoBehaviour
{

    [SerializeField] public GameObject EnemyExplode;

    public void ReactToHit()
    {
        GameObject EnemySpawnerObject = GameObject.Find("EnemySpawner");
        EnemySpawner spawnerScript = EnemySpawnerObject.GetComponent<EnemySpawner>();
        spawnerScript.SpawnNewEnemy();

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        GameObject DieParticle = Instantiate(EnemyExplode);
        DieParticle.transform.position = transform.position;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(DieParticle);
        Destroy(gameObject);
    }

    void Update()
    {
        if (GameObject.Find("Botonera").GetComponent<Botonera>() != null)
        {
            if (GameObject.Find("Botonera").GetComponent<Botonera>().jugando == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
