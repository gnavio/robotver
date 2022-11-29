using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public string menuSceneName = "menurobotver";
    [SerializeField] AudioSource countdownAudio;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } 
            else { Pause(); }
        }    
    }

    public void Resume() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        countdownAudio.UnPause();
    }

    void Pause() 
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 

        if (countdownAudio.isPlaying)
        {
            countdownAudio.Pause();
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
