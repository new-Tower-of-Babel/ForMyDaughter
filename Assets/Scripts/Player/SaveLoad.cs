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

            saveFilePath = Application.persistentDataPath + "/saveData.json"; // 초기화
        }
        else
        {
            Destroy(gameObject); // 중복 방지
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
            string json = JsonUtility.ToJson(data, true); // JSON으로 변환
            File.WriteAllText(saveFilePath, json); // 파일에 쓰기
            Debug.Log($"스테이지 '{stageName}'이 {saveFilePath}에 저장되었습니다.");
        }
        catch (IOException e)
        {
            Debug.LogError($"스테이지 저장 중 오류 발생: {e.Message}");
        }
    }

    public string LoadSavedStage()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath); // 파일 읽기
                SaveData data = JsonUtility.FromJson<SaveData>(json); // JSON 파싱
                Debug.Log($"저장된 스테이지 '{data.savedStage}'가 로드되었습니다.");
                return data.savedStage; // 저장된 스테이지 반환
            }
            else
            {
                Debug.LogWarning("저장 파일이 존재하지 않아 기본값('Stage1')을 반환합니다.");
                return "Stage1"; // 기본값
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"스테이지 로드 중 오류 발생: {e.Message}");
            return "Stage1"; // 기본값
        }
    }
}
