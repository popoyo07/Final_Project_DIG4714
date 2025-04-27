using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    bool isPaused = false;
    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        pauseMenu.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 1.0f;
        musicManager.ResumeMusic();
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 0f;
        musicManager.PauseMusic();
    }
}
