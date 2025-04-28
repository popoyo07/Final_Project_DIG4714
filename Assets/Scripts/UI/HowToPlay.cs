using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject panel;
    public AudioClip SFXPanel;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
        audioSource.clip = SFXPanel;
        audioSource.Play();
    }
}
