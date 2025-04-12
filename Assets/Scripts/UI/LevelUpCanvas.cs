using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;


public class LevelUpCanvas : MonoBehaviour
{
    public GameObject LevelUpUI;
    private UIBars UIBars;
    private GameObject player;

    public TextMeshProUGUI RandomBuff1;
    public TextMeshProUGUI RandomBuff2;
    public TextMeshProUGUI RandomBuff3;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        UIBars = player.GetComponent<UIBars>();


        LevelUpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIBars.leveledUp)
        {
            ShowLevelUp();
            RandomBuff();
            UIBars.leveledUp = false;
        }
    }


    public void ShowLevelUp()
    {
        LevelUpUI.SetActive(true);
        Time.timeScale = 0f;


    }

    public void Resume()
    {
        LevelUpUI.SetActive(false);
        Time.timeScale = 1f;
    }


    public void RandomBuff()
    {
        string[] buffs = {
        "10% chance of freezing an enemy when \nstriking an enemy for a certain amount of time",
        "Deal damage in an area every 10 seconds",
        "Base Attack damage increases by 5%",
        "Speed increases 5%",
        "Gain 5% more experience from enemies"
    };

        List<string> shuffledBuffs = new List<string>(buffs);
        shuffledBuffs = shuffledBuffs.OrderBy(b => Random.value).ToList();

        RandomBuff1.text = shuffledBuffs[0];
        RandomBuff2.text = shuffledBuffs[1];
        RandomBuff3.text = shuffledBuffs[2];
    }


}
