using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //public SceneAsset scene;
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    private static SceneChanger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartButton()
    {
        //if (scene != null)
        //{
        //SceneManager.LoadScene("stage1");
        LoadSceneWithFade("stage1");
        //}
    }

    public void LoadGame()
    {
        string savedStage = PlayerPrefs.GetString("SavedStage", "Stage1");
        //SceneManager.LoadScene(savedStage);
        LoadSceneWithFade(savedStage);
    }

    public void GoToTitle()
    {
        SaveGame();
        //SceneManager.LoadScene("TitleScene");
        LoadSceneWithFade("TitleScene");
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

        //SceneManager.LoadScene(nextScene);
        LoadSceneWithFade(nextScene);
    }
    
    public void LoadSceneWithFade(string scene)
    {
        StartCoroutine(FadeOutAndLoadScene(scene));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        fadeImage.color = new Color(0, 0, 0, 0);
        Color color = fadeImage.color;

        // 페이드 아웃 효과
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        // 페이드 인 효과 (Optional)
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
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
