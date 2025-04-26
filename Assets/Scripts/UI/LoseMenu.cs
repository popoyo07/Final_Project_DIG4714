using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    private UIBars UIBars;
    public GameObject LoseScene;
    private GameObject player;
    private JsonSaveExample save;
    bool added;

    public GameObject[] allPlayers;


    // Start is called before the first frame update
    void Start()
    {
        save = GameObject.FindWithTag("GameManager").GetComponent<JsonSaveExample>();
        foreach (GameObject p in allPlayers)
        {
            if (p.CompareTag("Player"))
            {
                player = p;
                break;
            }
        }
        UIBars = player.GetComponent<UIBars>();
        LoseScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIBars.playerAlive == false)
        {
            Debug.LogWarning(UIBars.playerAlive);

            if (!added)
            {
                added = true;
                save.addCoinsEnd();
                save.SaveData();
            }
           
            LoseScene.SetActive(true);
            Time.timeScale = 0f;

        }
    }
}
