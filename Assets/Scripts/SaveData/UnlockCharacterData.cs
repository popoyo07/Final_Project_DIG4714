﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterUnlockUI : MonoBehaviour
{
    [Header("Buttons for Characters")]
    public Button[] characterButtons; //place buttons

    [Header("Prices for Characters")]
    public TextMeshProUGUI mrsClausePrice;
    public TextMeshProUGUI RudolfPrice;

    [Header("SFX")]
    public AudioClip SFXwrong;
    AudioSource audioSource;

    [Header("Coin Info")]
    public int[] characterPrices = new int[3]; // Price for characters 0,1,2 (0 is free, always unlocked)
    //public int playerCoins = 100; 
    // Simulated coin value, will be replaced by the actually coin in the game in the future
    public JsonSaveExample gameSaveData;
    public int playerCoins;


    private string saveFilePath;
    private List<bool> characterUnlocked;
    public static int characterChosen;

    private void Start()
    {
        gameSaveData = GameObject.FindWithTag("GameManager").GetComponent<JsonSaveExample>();
        saveFilePath = Application.persistentDataPath + "unlocked_characters.txt";
        audioSource = GetComponentInParent<AudioSource>();
        //Debug.Log(Application.persistentDataPath); //Check where it saved
        characterUnlocked = new List<bool> { true, false, false }; // Default state
        LoadUnlockData();
        UpdateButtonStates();
        HookUpButtonEvents();
    }

    private void Update()
    {
        playerCoins = gameSaveData.totalCoins;
        //Press R to Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetUnlockData();
        }
    }


    private void LoadUnlockData()
    {
        // Check if the save file exists at the specified path
        if (File.Exists(saveFilePath))
        {
            string[] lines = File.ReadAllLines(saveFilePath);
            for (int i = 0; i < characterUnlocked.Count && i < lines.Length; i++)
            {
                // Try to parse the line into a boolean value indicating unlock status
                if (bool.TryParse(lines[i], out bool isUnlocked))
                {
                    // Set the character's unlock status based on the parsed value
                    characterUnlocked[i] = isUnlocked;
                }
            }
        }
        else
        {
            SaveUnlockData();
        }
    }

    private void SaveUnlockData()
    {
        List<string> lines = new List<string>();
        // Loop through each unlock status in the characterUnlocked list
        foreach (bool unlocked in characterUnlocked)
        {
            // Convert the boolean value to a string ("True" or "False") and add it to the list
            lines.Add(unlocked.ToString());
        }
        File.WriteAllLines(saveFilePath, lines);
    }

    private void HookUpButtonEvents()
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            // Add an onClick listener to the button that calls OnCharacterButtonClicked with the correct index
            characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
        }
    }

    private void OnCharacterButtonClicked(int index)
    {
        // If character is unlocked
        if (characterUnlocked[index])
        {
            characterChosen = index;
            //Debug.Log(characterChosen);

            SceneManager.LoadScene("LevelSelect");
        }

        // If there is enough coin to unlock character
        else if (playerCoins >= characterPrices[index])
        {
            // Player coins calculation
            playerCoins -= characterPrices[index];
            gameSaveData.totalCoins = playerCoins;

            // Update unlock variables based on index
            characterChosen = index;
            if (index == 1)
            {
                gameSaveData.unlocmrsClause();

                Debug.Log($"Character mrsClause unlocked and selected. Starting Level Select");

            }
            else if (index == 2)
            {
                gameSaveData.unlockRudolf();
                Debug.Log($"Character Rudolf unlocked and selected. Starting Level Select");
            }

            characterChosen = index;

            // Save current data
            gameSaveData.SaveData();
            characterUnlocked[index] = true;
            SaveUnlockData();
            UpdateButtonStates();

            SceneManager.LoadScene("LevelSelect");
        }

        // If not enough coin to unlock
        else
        {
            audioSource.clip = SFXwrong;
            audioSource.Play();
            Debug.Log("Not enough coins to unlock this character.");
        }
    }



    private void UpdateButtonStates()
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].interactable = true;
        }

        // ALSO hide the price text if unlocked
        if (characterUnlocked[1]) // MrsClause
        {
            mrsClausePrice.enabled = false;
        }
        if (characterUnlocked[2]) // Rudolf
        {
            RudolfPrice.enabled = false;
        }
    }

    //Reset all Data
    private void ResetUnlockData()
    {
        characterUnlocked = new List<bool> { true, false, false }; // Reset to default state
        SaveUnlockData();
        UpdateButtonStates();
        Debug.Log("All character unlock data has been reset.");
    }

}
