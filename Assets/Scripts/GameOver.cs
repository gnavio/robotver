using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverMenu;
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
        Cursor.lockState = CursorLockMode.Locked;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        gameOverAudio.UnPause();
        
        // Reiniciar nivel actual
        SceneManager.LoadScene(actualSceneName);

    }

    public void ActiveGameOverMenu() 
    {
        Cursor.lockState = CursorLockMode.Confined;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true; 

        if (gameOverAudio.isPlaying)
        {
            gameOverAudio.Pause();
        }
    }
}
