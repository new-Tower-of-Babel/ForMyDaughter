using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance { get; private set; }

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveFilePath = Application.persistentDataPath + "/saveData.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public string savedStage;
    }

    public void SaveCurrentStage(string stageName)
    {
        try
        {
            SaveData data = new SaveData { savedStage = stageName };
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, json);
            Debug.Log($"'{stageName}' - {saveFilePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Stage Save Error: {e.Message}");
        }
    }

    public string LoadSavedStage()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                Debug.Log($"'{data.savedStage}' Load");
                return data.savedStage;
            }
            else
            {
                return "Stage1";
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Stage Load Error: {e.Message}");
            return "Stage1";
        }
    }
}
