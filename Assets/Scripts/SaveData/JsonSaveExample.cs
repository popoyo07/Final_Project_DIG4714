using UnityEngine;
using System.IO;

[System.Serializable]
public class GameSaveData
{
    public string playerName;
    public int coinsCollected;
}

public class JsonSaveExample : MonoBehaviour
{
    public string playerName = "DefaultPlayer";
    public int lastCoins = 0;

    //public CollectableManager manager;
    private string path;  // Declared at class level

    private string lastPlayerName;
    private int lastHighScore;

    void Awake()
    {
        path = Application.persistentDataPath + "/gamesave.json";  // ✅ Initialize early
    }

    void Start()
    {
        LoadData(); // Load existing data
    }

    void Update()
    {
        if (playerName != lastPlayerName || lastCoins != lastHighScore)
        {
            SaveData();
            lastPlayerName = playerName;
            lastHighScore = lastCoins;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.R)) // reset game data 
        {
            ResetData();
        }
        // Need to add a way to reset data
    }

    public void SaveData()
    {
        if (string.IsNullOrEmpty(path))  // ✅ Safety check
        {
            Debug.LogError("Save path is null or empty!");
            return;
        }

        GameSaveData saveData = new GameSaveData { playerName = playerName, coinsCollected = lastCoins };
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);

        Debug.Log($"JSON Saved: {playerName}, {lastCoins}");
    }
    void ResetData()
    {
        GameSaveData saveData = new GameSaveData { playerName = playerName, coinsCollected = 0 };
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    void LoadData()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            GameSaveData loadedData = JsonUtility.FromJson<GameSaveData>(jsonData);
            playerName = loadedData.playerName;
            lastCoins = loadedData.coinsCollected;
            Debug.Log($"JSON Loaded: {playerName}, {lastCoins}");
        }
    }
}
