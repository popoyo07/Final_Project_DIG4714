using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("ScriptTestingUI");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCreadit()
    {
        SceneManager.LoadScene("CreaditPage");
    }

    public void LoadSelection()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
