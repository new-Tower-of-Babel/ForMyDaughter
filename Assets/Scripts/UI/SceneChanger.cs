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

    //private static SceneChanger instance;
    private SaveLoad saveLoad;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        if (FindObjectOfType<SaveLoad>() != null)
        {
            saveLoad = FindObjectOfType<SaveLoad>();
        }
        else
        {
            Debug.LogError("SaveLoad.cs가 씬에 없습니다!");
        }
    }

    public void StartButton()
    {
        LoadSceneWithFade("StoryScene");
    }

    public void LoadGame()
    {
        if (saveLoad != null)
        {
            string savedStage = saveLoad.LoadSavedStage();
            LoadSceneWithFade(savedStage);
        }
        else
        {
            Debug.LogError("SaveLoad 인스턴스를 찾을 수 없습니다");
        }
    }

    public void GoToTitle()
    {
        if (saveLoad != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            saveLoad.SaveCurrentStage(currentScene);
        }

        LoadSceneWithFade("TitleScene");
    }

    public void NextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        if (currentScene == "StoryScene") nextScene = "Stage1";
        else if (currentScene == "Stage1") nextScene = "Stage2";
        else if (currentScene == "Stage2") nextScene = "Stage3";
        else if (currentScene == "Stage3") nextScene = "StoryScene";

        LoadSceneWithFade(nextScene);
    }
    
    public void LoadSceneWithFade(string scene)
    {
        SceneManager.LoadScene(scene);
        //StartCoroutine(FadeOutAndLoadScene(scene));
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
