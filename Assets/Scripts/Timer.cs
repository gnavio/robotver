using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //Temporizador Preparaci�n
    float currentPrepTime;
    float startingPrepTime = 6f;
    [SerializeField] public TMPro.TMP_Text countdownPrepText;

    //Temporizador Nivel
    float currentTime;
    [SerializeField] public float startingTime = 20f;
    [SerializeField] public TMPro.TMP_Text countdownText;

    [HideInInspector] public bool timerPrepActivado = true;
    [HideInInspector] public bool timerActivado = false;

    [SerializeField] public Level1Manager level1Manager;
    // Pantalla Nivel Completo
    public static float finalTimeLevel;
    GameManager gameManager;

    [SerializeField] AudioSource Music;


    void Start()
    {
        countdownPrepText.gameObject.SetActive(false);
        currentPrepTime = startingPrepTime;

        countdownText.gameObject.SetActive(false);
        currentTime = startingTime;

        // Pantalla Nivel Completo
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (timerPrepActivado && !PauseMenu.GameIsPaused)
        {
            countdownPrepText.gameObject.SetActive(true);
            currentPrepTime -= 1 * Time.deltaTime;
            float currentPrepSecs = Mathf.FloorToInt(currentPrepTime % 60); // Conversi�n a segundos enteros
            countdownPrepText.text = currentPrepSecs.ToString("0");

            if (currentPrepTime < 1)
            {
                timerPrepActivado = false;

                Music.Play(0);

                timerActivado = true; // Activamos Timer Nivel
                countdownPrepText.gameObject.SetActive(false);
                currentPrepTime = startingPrepTime;
            }

            // DESACTIVAR MOVIMIENTO Y DISPARO

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("MainCamera").GetComponent<RayShooter>().enabled = false;
        }
        else
        {
            countdownPrepText.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("MainCamera").GetComponent<RayShooter>().enabled = true;
        }

        if (timerActivado)
        {
            countdownText.gameObject.SetActive(true);
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.00");

            // SI COMPLETAS LA MISI�N...
            if (level1Manager.nivelCompletado)
            {
                float finalTime = startingTime - currentTime; 
                
                //// Pantalla Nivel Completo ///////
                finalTimeLevel = finalTime;
                gameManager.NivelCompleto();
                ///////////////////////////////////
                
                countdownText.gameObject.SetActive(false); // desactivamos timer gameObject

                timerActivado = false;
            }

            if (currentTime <= 0)
            {
                timerActivado = false;
                countdownText.gameObject.SetActive(false);
                currentTime = startingTime;

                // AQU� HAY QUE PONER QU� PASA SI SE ACABA EL TIEMPO
                gameManager.GameOver();
            }
        }
        
    }

}

