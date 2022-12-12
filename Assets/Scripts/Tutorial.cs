using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{  
    [SerializeField] GameObject[] checkpoint;
    [HideInInspector] public int activeCP;

    [SerializeField] GameObject EnemiesTutorial;
    [SerializeField] TMPro.TMP_Text EnemyCount;
    int totalEnemies;

    [Header("Missions")]
    [SerializeField] GameObject Mission1;
    [SerializeField] GameObject Mission2;
    [SerializeField] GameObject TutorialCompleted;
    [SerializeField] GameObject Mission3;

    [Header("Videos")]
    [SerializeField] GameObject CanvasVideo;
    [SerializeField] GameObject WallrunVideo;
    [SerializeField] GameObject ImpulseVideo;

    [SerializeField] Habilidades habilidades;

    [SerializeField] GameObject ObjetosFinTutorial;

    // Start is called before the first frame update
    void Start()
    {      
        activeCP = 0;
        checkpoint[activeCP].SetActive(true);
        EnemiesTutorial.SetActive(false);
        totalEnemies = EnemiesTutorial.transform.childCount;

        Mission1.SetActive(true);
        Mission2.SetActive(false);
        TutorialCompleted.SetActive(false);
        Mission3.SetActive(false);
        ObjetosFinTutorial.SetActive(false);

        habilidades.cartuchosImpulso = 0;
        habilidades.cartuchosTeletransporte = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(EnemiesTutorial.transform.childCount);

        if (EnemiesTutorial.transform.childCount <= 0)
        {
            Debug.Log("Tutorial Completado");
            Mission2.SetActive(false);
            TutorialCompleted.SetActive(true);
            Mission3.SetActive(true);
            ObjetosFinTutorial.SetActive(true);
        }

        EnemyCount.text = totalEnemies - EnemiesTutorial.transform.childCount + "/" + totalEnemies;
    }

    public void CambiaCheckPoint()
    {
        checkpoint[activeCP].SetActive(false);

        if(activeCP < checkpoint.Length)
        {
            activeCP++;

            if(activeCP <= checkpoint.Length - 1)
            {
                checkpoint[activeCP].SetActive(true);
            }           
        }

        if (activeCP == 2)
        {
            Pause();
            WallrunVideo.SetActive(true);
            WallrunVideo.GetComponent<VideoPlayer>().Play();
        }
        if (activeCP == 3)
        {
            Pause();
            ImpulseVideo.SetActive(true);
            ImpulseVideo.GetComponent<VideoPlayer>().Play();
            habilidades.cartuchosImpulso = 6;
        }

        if (activeCP == 6)
        {
            EnemiesTutorial.SetActive(true);
            Mission1.SetActive(false);
            Mission2.SetActive(true);
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

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        CanvasVideo.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.Find("MainCamera").GetComponent<RayShooter>().enabled = true;
        WallrunVideo.SetActive(false);
        ImpulseVideo.SetActive(false);
    }

    void Pause()
    {
        CanvasVideo.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.Find("MainCamera").GetComponent<RayShooter>().enabled = false;


    }

}
