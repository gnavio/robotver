using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float score;
    [SerializeField] public float startingTime = 20f;
    [SerializeField] public TMPro.TMP_Text countdownText;
    [SerializeField] public TMPro.TMP_Text scoreText;
    public float killScoreBonus = 100;

    public bool timerActivado = false; 

    void Start()
    {
        countdownText.gameObject.SetActive(false);
        currentTime = startingTime;
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString("0");

        if (timerActivado)
        {
            countdownText.gameObject.SetActive(true);
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.00");

            if (currentTime <= 0)
            {
                timerActivado = false;
                GameObject.Find("Botonera").GetComponent<Botonera>().jugando = false;
                countdownText.gameObject.SetActive(false);
                currentTime = startingTime;
            }
        }
        
    }
}

