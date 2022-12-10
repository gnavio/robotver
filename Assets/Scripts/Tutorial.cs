using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] GameObject[] checkpoint;
    [HideInInspector] public int activeCP;

    // Start is called before the first frame update
    void Start()
    {      
        activeCP = 0;
        checkpoint[activeCP].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiaCheckPoint()
    {
        checkpoint[activeCP].SetActive(false);

        if(activeCP < checkpoint.Length - 1)
        {
            activeCP++;
            checkpoint[activeCP].SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("DetectaCollisionChild");
            CambiaCheckPoint();
        }
    }

}
