using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager backgroundMusic;

    // List of scene names where the music should continue
    public string[] allowedScenes;

    void Awake()
    {
        // Singleton pattern to prevent duplicates
        if (backgroundMusic != null && backgroundMusic != this)
        {
            Destroy(gameObject);
            return;
        }

        backgroundMusic = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool allowed = false;

        // Check if the current scene is one of the allowed ones
        foreach (string sceneName in allowedScenes)
        {
            if (scene.name == sceneName)
            {
                allowed = true;
                break;
            }
        }

        // If not allowed, destroy the music manager
        if (!allowed)
        {
            Destroy(gameObject);
        }
    }
}
