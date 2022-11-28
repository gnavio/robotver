using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverMenu;
    public GameObject generalCanvasMenu;
    Scene actualScene;
    string actualSceneName;
    [SerializeField] AudioSource gameOverAudio;

    private void Start() {
        actualScene = SceneManager.GetActiveScene();
        actualSceneName = actualScene.name;
    }

    void Update()
    {
        if(GameIsOver)
        {
            ActiveGameOverMenu();
        } 
    }    

    public void TryAgain() 
    {
        generalCanvasMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        Cursor.visible = false;
        gameOverAudio.UnPause();
        
        // Reiniciar nivel actual
        SceneManager.LoadScene(actualSceneName);

    }

    public void ActiveGameOverMenu() 
    {
        generalCanvasMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true; 
        Cursor.visible = true;

        if (gameOverAudio.isPlaying)
        {
            gameOverAudio.Pause();
        }
    }
}
