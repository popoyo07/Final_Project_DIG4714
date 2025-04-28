using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
    }
}
