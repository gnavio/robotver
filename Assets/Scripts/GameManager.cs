using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Scene actualScene;
    string actualSceneName;
    GameOver gameOver;
    NivelCompleto nivelCompleto;
    public string siguienteNivel;

    void Start () 
    {
        actualScene = SceneManager.GetActiveScene();
        actualSceneName = actualScene.name;
        GameObject canvasGameOver = GameObject.Find("CanvasGameOver");
        gameOver = canvasGameOver.GetComponent<GameOver>();
        GameObject canvasNivelCompleto = GameObject.Find("CanvasNivelCompleto");
        nivelCompleto = canvasNivelCompleto.GetComponent<NivelCompleto>();

    }

    void Update()
    {
        // Reiniciar el nivel actual con la tecla N
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(actualSceneName);
        }
    }

    public void GameOver()
    {
        gameOver.ActiveGameOverMenu();
    }

    public void NivelCompleto()
    {   
        // Nombre de la escena que contiene el siguiente nivel
        nivelCompleto.siguienteNivel = siguienteNivel;
        nivelCompleto.ActiveGameCompletedMenu();
    }
}