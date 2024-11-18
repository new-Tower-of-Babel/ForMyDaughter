using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //public SceneAsset scene;

    public void StartButton()
    {
        //if (scene != null)
        //{
        SceneManager.LoadScene("stage1");
        //}
    }

    public void LoadGame()
    {
        string savedStage = PlayerPrefs.GetString("SavedStage", "Stage1");
        SceneManager.LoadScene(savedStage);
    }

    public void GoToTitle()
    {
        SaveGame();
        SceneManager.LoadScene("TitleScene");
    }

    private void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedStage", currentScene);
        PlayerPrefs.Save();
    }

    public void NextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        if (currentScene == "Stage1") nextScene = "Stage2";
        else if (currentScene == "Stage2") nextScene = "Stage3";
        else if (currentScene == "Stage3") nextScene = "TitleScene";

        SceneManager.LoadScene(nextScene);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
