using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botonera : MonoBehaviour
{
    [SerializeField] GameObject interactOverlay;
    private bool activable = false;
    public bool jugando;

    // Start is called before the first frame update
    void Start()
    {
        interactOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Spawnea!!");
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SpawnNewEnemy();
                jugando = true;
                interactOverlay.SetActive(false);

                //Resetea Puntuación
                GameObject.Find("Canvas").GetComponent<Timer>().score = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !jugando)
        {
            activable = true;
            interactOverlay.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activable = false;
            interactOverlay.SetActive(false);
        }
    }
}
