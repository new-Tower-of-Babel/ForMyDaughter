using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingsUI;  // 설정 UI
    public GameObject hpUI;        // HP UI
    public GameObject pauseMenu;   // 일시정지 메뉴
    private bool isPaused = false; // 게임 일시정지 상태

    private void Awake()
    {
        // 설정 UI는 모든 씬에서 유지
        DontDestroyOnLoad(settingsUI);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        // Esc 키로 설정 메뉴 열기/닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 스테이지 씬 확인
        if (scene.name.Contains("Stage"))
        {
            hpUI.SetActive(true); // HP UI 활성화
        }
        else
        {
            hpUI.SetActive(false); // HP UI 비활성화
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;  // 게임 일시정지
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;  // 게임 재개
            pauseMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // 시간 초기화
    }

    public void QuitToMainMenu()
    {
        SaveCurrentScene();
        // 메인 메뉴로 이동
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f; // 시간 초기화
    }

    private void SaveCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // SaveLoad의 SaveStage 메서드 호출
        SaveLoad saveLoad = FindObjectOfType<SaveLoad>();
        if (saveLoad != null)
        {
            saveLoad.SaveCurrentStage(currentScene); // 현재 씬 저장
            Debug.Log($"현재 씬 '{currentScene}'이 저장되었습니다.");
        }
        else
        {
            Debug.LogError("SaveLoad 스크립트를 찾을 수 없습니다. 저장 실패.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
