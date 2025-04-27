using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager backgroundMusic;

    [System.Serializable]
    public class SceneAudio
    {
        public List<string> sceneNames;
        public AudioSource audioSource;
    }


    public SceneAudio[] sceneAudios;

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

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        foreach (SceneAudio sa in sceneAudios)
        {
            if (sa.audioSource == null) continue;

            if (sa.sceneNames.Contains(sceneName)) // now check if list contains
            {
                sa.audioSource.gameObject.SetActive(true);
                sa.audioSource.Play();
            }
            else
            {
                sa.audioSource.Stop();
                sa.audioSource.gameObject.SetActive(false);
            }
        }
    }

    public void PauseMusic()
    {
        foreach (SceneAudio sa in sceneAudios)
        {
            if (sa.audioSource != null && sa.audioSource.isPlaying)
            {
                sa.audioSource.Pause();
            }
        }
    }

    public void ResumeMusic()
    {
        foreach (SceneAudio sa in sceneAudios)
        {
            if (sa.audioSource != null && sa.audioSource.gameObject.activeSelf)
            {
                sa.audioSource.UnPause();
            }
        }
    }
}
