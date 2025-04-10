using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlockUI : MonoBehaviour
{
    [Header("Buttons for Characters")]
    public Button[] characterButtons; //place buttons

    [Header("Coin Info")]
    public int[] characterPrices = new int[3]; // Price for characters 0,1,2 (0 is free, always unlocked)
    //public int playerCoins = 100; 
    // Simulated coin value, will be replaced by the actually coin in the game in the future
    public GameSaveData gameSaveData;
    public int playerCoins;

    private string saveFilePath;
    private List<bool> characterUnlocked;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "unlocked_characters.txt";
        //Debug.Log(Application.persistentDataPath); //Check where it saved
        characterUnlocked = new List<bool> { true, false, false }; // Default state
        LoadUnlockData();
        UpdateButtonStates();
        HookUpButtonEvents();
    }

    private void Update()
    {
        playerCoins = gameSaveData.coinsCollected;
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
        if (characterUnlocked[index])
        {
            Debug.Log($"Character {index + 1} selected. Starting game...");
            //SceneManager.LoadScene(ActualGame);
            //start the game
        }
        else if (playerCoins >= characterPrices[index])
        {
            playerCoins -= characterPrices[index];
            characterUnlocked[index] = true;
            SaveUnlockData();
            UpdateButtonStates();
            Debug.Log($"Character {index + 1} unlocked and selected. Starting game...");
            //SceneManager.LoadScene(ActualGame);
            //start the game

        }
        else
        {
            Debug.Log("Not enough coins to unlock this character.");
            //Show UI feedback
        }
    }


    private void UpdateButtonStates()
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // Determine if the button should be interactable:
            // It's interactable if the character is already unlocked,
            // OR if the player has enough coins to afford unlocking it
            bool interactable = characterUnlocked[i] || playerCoins >= characterPrices[i];

            // Set the interactable state of the button accordingly
            characterButtons[i].interactable = interactable;
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
