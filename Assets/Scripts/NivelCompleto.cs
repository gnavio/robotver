using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NivelCompleto : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameCompletedMenu;
    public GameObject generalCanvasMenu;
    [SerializeField] TextMeshProUGUI textoTiempo;
    Scene actualScene;
    string actualSceneName;
    [SerializeField] AudioSource gameOverAudio;
    public string siguienteNivel;

    private void Start() {
        actualScene = SceneManager.GetActiveScene();
        actualSceneName = actualScene.name;
    }

    void Update()
    {
        if(GameIsOver)
        {
            ActiveGameCompletedMenu();
        } 
    }    

    public void TryAgain() 
    {
        generalCanvasMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        gameCompletedMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        Cursor.visible = false;
        gameOverAudio.UnPause();
        
        // Reiniciar nivel actual
        SceneManager.LoadScene(actualSceneName);

    }

    public void NextLevel() 
    {
        generalCanvasMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        gameCompletedMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        Cursor.visible = false;
        gameOverAudio.UnPause();
        
        // Reiniciar nivel actual
        SceneManager.LoadScene(siguienteNivel);

    }

    public void ActiveGameCompletedMenu() 
    {
        textoTiempo.SetText(Timer.finalTimeLevel.ToString("0.00"));
        generalCanvasMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        gameCompletedMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true; 
        Cursor.visible = true;

        if (gameOverAudio.isPlaying)
        {
            gameOverAudio.Pause();
        }
    }
}
