using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    private UIBars UIBars;
    public GameObject LoseScene;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        UIBars = player.GetComponent<UIBars>();
        LoseScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIBars.playerAlive == false)
        {
            LoseScene.SetActive(true);
            Time.timeScale = 0f;

        }
    }
}
