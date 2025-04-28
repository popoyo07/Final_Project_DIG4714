using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;



public class LevelUpCanvas : MonoBehaviour
{

    // References
    public GameObject LevelUpUI;
    private UIBars UIBars;
    private GameObject player;
    private Weapons weapons;
    private MusicManager musicManager;
    AudioSource audiosource;
    GameManager gameManager;


    [Header("SFX")]
    public AudioClip SFXlevelup;

    [Header("3 Random Buffs")]
    public TextMeshProUGUI RandomBuff1;
    public TextMeshProUGUI RandomBuff2;
    public TextMeshProUGUI RandomBuff3;

    [Header("3 Different weapons")]
    public bool bookUnlocked;
    public bool spikesUnlocked;
    public bool spearUnlocked;

    [Header("Player")]
    public GameObject[] allPlayers;


    // List to store the current random buff choices
    private List<Buff> currentBuffChoices;

    // Enum to define different types of buffs
    public enum BuffType
    {
        BookUnlock,
        SpikesUnlock,
        //SpearUnlock,
        BookDamage,
        SpikesDamage,
        //SpearDamage,
        BookRate,
        SpikesRate,
        //SpearRate,
        BookDuration,
        SpikesDuration,
        //SpearDuration,
        BaseDamage,
        BaseRate,
        BaseDuration,
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

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }

    void Start()
    {
        // Search each GameObject in the allPlayers array 
        foreach (GameObject p in allPlayers)
        {
            if (p.CompareTag("Player"))
            {
                player = p;
                break;
            }
        }
        player = allPlayers[gameManager.pChoice];

        weapons = player.GetComponent<Weapons>();
        UIBars = player.GetComponent<UIBars>();
        musicManager = FindObjectOfType<MusicManager>();
        audiosource = GetComponent<AudioSource>();

        LevelUpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bookUnlocked = weapons.bookAttack;
        spikesUnlocked = weapons.spikesAttack;
        spearUnlocked = weapons.spearAttack;

        if (UIBars.leveledUp)
        {
            ShowLevelUp();          // 1. Open UI
            EventSystem.current.SetSelectedGameObject(null); // 2. THEN clear selection
            RandomBuff();           // 3. Assign buffs
            audiosource.clip = SFXlevelup;
            audiosource.Play();
            UIBars.leveledUp = false;
        }

    }


    public void ShowLevelUp()
    {
        LevelUpUI.SetActive(true);
        musicManager.PauseMusic();

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        LevelUpUI.SetActive(false);
        musicManager.ResumeMusic();
        Time.timeScale = 1f;
    }

    // Randomly pick 3 buffs and display them to the player
    public void RandomBuff()
    {
        List<Buff> allBuffs = new List<Buff>
        {
            new Buff("Unlock Book Attack", BuffType.BookUnlock),
            new Buff("Unlock Spikes Attack", BuffType.SpikesUnlock),
            //new Buff("Unlock Spear Attack", BuffType.SpearUnlock),
            new Buff("Book Attack damage increases by 5%", BuffType.BookDamage),
            new Buff("Spikes Attack damage increases by 5%", BuffType.SpikesDamage),
            //new Buff("Spear Attack damage increases by 5%", BuffType.SpearDamage),
            new Buff("Book Attack rate decreases by 2%", BuffType.BookRate),
            new Buff("Spikes Attack rate decreases by 2%", BuffType.SpikesRate),
            //new Buff("Spear Attack rate decreases by 2%", BuffType.SpearRate),
            new Buff("Book Attack duration decreases by 3%", BuffType.BookDuration),
            new Buff("Spikes Attack duration decreases by 3%", BuffType.SpikesDuration),
            //new Buff("Spear Attack duration decreases by 3%", BuffType.SpearDuration),
            new Buff("Base Attack damage increases by 5%", BuffType.BaseDamage),
            new Buff("Base Attack rate decreases by 2%", BuffType.BaseRate),
            new Buff("Base Attack duration increases by 3%", BuffType.BaseDuration),
            new Buff("Speed increases 5%", BuffType.IncreaseSpeed),
            new Buff("Gain 10% more xp from enemies", BuffType.BonusXP)
    };

        // Remove buffs that can't be applied
        allBuffs = allBuffs.Where(buff =>
            // Don't show "Unlock" buffs if already unlocked
            (buff.type != BuffType.BookUnlock || !bookUnlocked) && (buff.type != BuffType.SpikesUnlock || !spikesUnlocked) && /*(buff.type != BuffType.SpearUnlock || !spearUnlocked) && */

            // Remove damage, rate, duration buffs if their attack is not unlocked
            (bookUnlocked || !(buff.type == BuffType.BookDamage || buff.type == BuffType.BookRate || buff.type == BuffType.BookDuration)) &&
            (spikesUnlocked || !(buff.type == BuffType.SpikesDamage || buff.type == BuffType.SpikesRate || buff.type == BuffType.SpikesDuration)) /*&&
            (spearUnlocked || !(buff.type == BuffType.SpearDamage || buff.type == BuffType.SpearRate || buff.type == BuffType.SpearDuration))*/
        ).ToList();

        // Shuffle the buffs in the list
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
            case BuffType.BookUnlock:
                Debug.Log("Unlocked Book Attack");
                weapons.UnlockBook();
                break;

            case BuffType.SpikesUnlock:
                Debug.Log("Unlocked Spikes Attack");
                weapons.UnlockSpike();
                break;

/*            case BuffType.SpearUnlock:
                Debug.Log("Unlocked Spear Attack");
                weapons.UnlockSpear();
            break;*/

            case BuffType.BookDamage:
                weapons.UpdateWeaponDMG("BookAttack", weapons.WeaponDMG["BookAttack"] * 0.05f);
                break;

            case BuffType.SpikesDamage:
                weapons.UpdateWeaponDMG("SpikesAttack", weapons.WeaponDMG["SpikesAttack"] * 0.05f);
                break;

/*            case BuffType.SpearDamage:
                weapons.UpdateWeaponDMG("SpearAttack", weapons.WeaponDMG["SpearAttack"] * 0.05f);
                break;*/

            case BuffType.BookRate:
                weapons.UpdateWeaponRate("BookAttack", weapons.WeaponRateSec["BookAttack"] * 0.02f);
                break;

            case BuffType.SpikesRate:
                weapons.UpdateWeaponRate("SpikesAttack", weapons.WeaponRateSec["SpikesAttack"] * 0.02f);
                break;

/*            case BuffType.SpearRate:
                weapons.UpdateWeaponRate("SpearAttack", weapons.WeaponRateSec["SpearAttack"] * 0.02f);
                break;
*/
            case BuffType.BookDuration:
                weapons.UpdateWeaponDuration("BookAttack", weapons.WeaponAttDuration["BookAttack"] * 0.03f);
                break;

            case BuffType.SpikesDuration:
                weapons.UpdateWeaponDuration("SpikesAttack", weapons.WeaponAttDuration["SpikesAttack"] * 0.03f);
                break;

/*            case BuffType.SpearDuration:
                weapons.UpdateWeaponDuration("SpearAttack", weapons.WeaponAttDuration["SpearAttack"] * 0.03f);
                break;
*/
            case BuffType.BaseDamage:
                weapons.UpdateWeaponDMG("AreaAttack", weapons.WeaponDMG["AreaAttack"] * 0.05f);
                break;

            case BuffType.BaseRate:
                weapons.UpdateWeaponRate("AreaAttack", weapons.WeaponRateSec["AreaAttack"] * 0.02f);
                break;

            case BuffType.BaseDuration:
                weapons.UpdateWeaponDuration("AreaAttack", weapons.WeaponAttDuration["AreaAttack"] * 0.03f);
                break;

            case BuffType.IncreaseSpeed:

                break;

            case BuffType.BonusXP:
                Debug.Log("Gained XP Buff");
                UIBars.xpMultiplier += 0.1f;
                break;

        }
    }

    // Called when the player click 1 of the buttons
    public void SelectBuff(int index)
    {
        Debug.Log("SelectBuff called with index: " + index);
        Buff selectedBuff = currentBuffChoices[index];
        ApplyBuff(selectedBuff.type);
        Resume();
    }



}
