using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class LevelUpCanvas : MonoBehaviour
{
    public GameObject LevelUpUI;
    private UIBars UIBars;
    private GameObject player;

    public TextMeshProUGUI RandomBuff1;
    public TextMeshProUGUI RandomBuff2;
    public TextMeshProUGUI RandomBuff3;
    
    // List to store the current random buff choices
    private List<Buff> currentBuffChoices;

    // Enum to define different types of buffs
    public enum BuffType
    {
        FreezeOnHit,
        AoEDamage,
        IncreaseAttack,
        IncreaseSpeed,
        BonusXP
    }

    // Buff class to represent a buff with description and type
    public class Buff
    {
        public string description;
        public BuffType type;

        // Constructor to initialize a Buff instance
        public Buff(string desc, BuffType t)
        {
            description = desc;
            type = t;
        }
    }

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

    // Randomly pick 3 buffs and display them to the player
    public void RandomBuff()
    {
        List<Buff> allBuffs = new List<Buff>
        {
            new Buff("10% chance of freezing an enemy when \nstriking an enemy for a certain amount of time", BuffType.FreezeOnHit),
            new Buff("Deal damage in an area every 10 seconds", BuffType.AoEDamage),
            new Buff("Base Attack damage increases by 5%", BuffType.IncreaseAttack),
            new Buff("Speed increases 5%", BuffType.IncreaseSpeed),
            new Buff("Gain 5% more experience from enemies", BuffType.BonusXP)
        };

        // Shuffle :D
        currentBuffChoices = allBuffs.OrderBy(b => Random.value).Take(3).ToList();

        RandomBuff1.text = currentBuffChoices[0].description;
        RandomBuff2.text = currentBuffChoices[1].description;
        RandomBuff3.text = currentBuffChoices[2].description;
    }


    // Apply the selected buff to the player
    public void ApplyBuff(BuffType buff)
    {
        switch (buff)
        {
            case BuffType.FreezeOnHit:
                Debug.Log("FreezeOnHit");
                break;


            case BuffType.AoEDamage:
                Debug.Log("AoEDamage");
                break;


            case BuffType.IncreaseAttack:
                Debug.Log("IncreaseAttack");
                break;


            case BuffType.IncreaseSpeed:
                Debug.Log("IncreaseSpeed");
                break;


            case BuffType.BonusXP:
                Debug.Log("BonusXP");
                break;
        }
    }

    // Called when the player click 1 of the buttons
    public void SelectBuff(int index)
    {
        Debug.Log("SelectBuff called with index: " + index);
        Buff selectedBuff = currentBuffChoices[index];
        ApplyBuff(selectedBuff.type);
    }


}
