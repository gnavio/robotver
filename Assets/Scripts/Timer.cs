using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //Temporizador Preparación
    float currentPrepTime;
    float startingPrepTime = 6f;
    [SerializeField] public TMPro.TMP_Text countdownPrepText;

    //Temporizador Nivel
    float currentTime;
    [SerializeField] public float startingTime = 20f;
    [SerializeField] public TMPro.TMP_Text countdownText;
    [SerializeField] public TMPro.TMP_Text finalTimeText;

    [HideInInspector] public bool timerPrepActivado = true;
    [HideInInspector] public bool timerActivado = false;

    [HideInInspector] public int killedEnemies = 0;

    public int requiredKills;

    void Start()
    {
        countdownPrepText.gameObject.SetActive(false);
        currentPrepTime = startingPrepTime;

        countdownText.gameObject.SetActive(false);
        currentTime = startingTime;

        finalTimeText.gameObject.SetActive(false);
    }

    void Update()
    {

        if (timerPrepActivado)
        {
            countdownPrepText.gameObject.SetActive(true);
            currentPrepTime -= 1 * Time.deltaTime;
            float currentPrepSecs = Mathf.FloorToInt(currentPrepTime % 60); // Conversión a segundos enteros
            countdownPrepText.text = currentPrepSecs.ToString("0");

            if (currentPrepTime < 1)
            {
                timerPrepActivado = false;
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

            // SI COMPLETAS LA MISIÓN...
            if (killedEnemies >= requiredKills)
            {
                float finalTime = startingTime - currentTime;  
                countdownText.gameObject.SetActive(false); // desactivamos timer gameObject

                finalTimeText.text = finalTime.ToString("0.00");
                finalTimeText.gameObject.SetActive(true); // activamos finalTime gameObject

                timerActivado = false;
            }

            if (currentTime <= 0)
            {
                timerActivado = false;
                countdownText.gameObject.SetActive(false);
                currentTime = startingTime;

                // AQUÍ HAY QUE PONER QUÉ PASA SI SE ACABA EL TIEMPO
            }
        }
        
    }
}

