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

            saveFilePath = Application.persistentDataPath + "/saveData.json"; // �ʱ�ȭ
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
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
            string json = JsonUtility.ToJson(data, true); // JSON���� ��ȯ
            File.WriteAllText(saveFilePath, json); // ���Ͽ� ����
            Debug.Log($"�������� '{stageName}'�� {saveFilePath}�� ����Ǿ����ϴ�.");
        }
        catch (IOException e)
        {
            Debug.LogError($"�������� ���� �� ���� �߻�: {e.Message}");
        }
    }

    public string LoadSavedStage()
    {
        StorySceneManager.firstStoryCount = StorySceneManager.fullStoryCount;
        try
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath); // ���� �б�
                SaveData data = JsonUtility.FromJson<SaveData>(json); // JSON �Ľ�
                Debug.Log($"����� �������� '{data.savedStage}'�� �ε�Ǿ����ϴ�.");
                return data.savedStage; // ����� �������� ��ȯ
            }
            else
            {
                Debug.LogWarning("���� ������ �������� �ʾ� �⺻��('Stage1')�� ��ȯ�մϴ�.");
                return "Stage1"; // �⺻��
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"�������� �ε� �� ���� �߻�: {e.Message}");
            return "Stage1"; // �⺻��
        }
    }
}
