using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSaveData
{
    public string playerName;
    public int coinsCollected;
    public bool rudolf;
    public bool mrsClause;
}

public class JsonSaveExample : MonoBehaviour
{
    private static JsonSaveExample instance;
    public string playerName = "DefaultPlayer";
    public int lastCoins = 0;

    //public CollectableManager manager;
    private string path;  // Declared at class level

    private string lastPlayerName;
    private int lastHighScore;
    public int totalCoins;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        path = Application.persistentDataPath + "/gamesave.json";  // ✅ Initialize early
    }

    void Start()
    {
        LoadData(); // Load existing data
    }
    public void addCoinsEnd() // reference when player loses to save new coins ammount 
    {
        totalCoins += lastCoins;
    }
    void Update()
    {
        string theName = SceneManager.GetActiveScene().name;
        if ( theName == "MainManu" || theName == "SelectScene")
        {
            lastCoins = 0;
        }
        if (playerName != lastPlayerName) // might remove entirely 
        {
            SaveData();
            lastPlayerName = playerName;
            //totalCoins = lastCoins;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.R)) // reset game data 
        {
            ResetData();
            LoadData();
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

        GameSaveData saveData = new GameSaveData { playerName = playerName, coinsCollected = totalCoins };
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
            totalCoins = loadedData.coinsCollected;
            Debug.Log($"JSON Loaded: {playerName}, {lastCoins}");
        }
    }
}
