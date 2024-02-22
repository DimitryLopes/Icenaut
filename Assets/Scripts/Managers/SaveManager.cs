using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string SAVE_FILE_PATH_SUFFIX = "/savedata.dat";

    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }


    public SavedData GetCurrentGameData()
    {
        SavedData data = new SavedData();

        GameManager gameManager = GameManager.Instance;
        float playerHealth = gameManager.CurrentPlayer.CurrentHealth;

        PowerUp currentPowerUp = PowerUpManager.Instance.CurrentPowerUp;
        float powerUpDuration = ClothingManager.Instance.RemainingClothingTimer;
        int powerUpId = currentPowerUp != null ? currentPowerUp.Data.ID : -1;

        ItemManager itemManager = ItemManager.Instance;
        int coinAmount = itemManager.GetItemAmount(ItemType.Coin);
        int healthPacks = itemManager.GetItemAmount(ItemType.HealthPack);

        int checkPointId = LevelManager.Instance.CurrentCheckpointID;

        data.activeBuffId = powerUpId;
        data.remainingBuffDuration = powerUpDuration;
        data.checkPointId = checkPointId;
        data.playerHealth = playerHealth;
        data.coins = coinAmount;
        data.healthPacks = healthPacks;

        return data;
    }

    public void SaveGame()
    {
        string path = GetSaveFilePath();
        SavedData data = GetCurrentGameData();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(path);
        Debug.Log("Saved at: " + path);
        formatter.Serialize(file, data);
        file.Close();
    }

    public SavedData LoadSave()
    {
        string path = GetSaveFilePath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SavedData data = (SavedData)formatter.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }

    private string GetSaveFilePath()
    {
        string path = Application.persistentDataPath + SAVE_FILE_PATH_SUFFIX;
        return path;
    }
}

[Serializable]
public class SavedData
{
    public float playerHealth;
    public float remainingBuffDuration;
    public int activeBuffId;
    public int checkPointId;
    public int coins;
    public int healthPacks;
}
