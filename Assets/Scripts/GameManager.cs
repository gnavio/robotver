using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Scene actualScene;
    string actualSceneName;
    GameOver gameOver;

    void Start () 
    {
        actualScene = SceneManager.GetActiveScene();
        actualSceneName = actualScene.name;
        GameObject canvasGameOver = GameObject.Find("CanvasGameOver");
        gameOver = canvasGameOver.GetComponent<GameOver>();

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
}