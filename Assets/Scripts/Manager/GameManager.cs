using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 패턴

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Invoke("RestartGame", 2f);
    }

    public void FinishGame()
    {
        Debug.Log("You Win!");
        // 게임 성공 연출 추가 (UI, 효과음 등)
        Invoke("QuitGame", 2f); // 2초 후 게임 종료
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Debug.Log("Returning to Main Menu...");
        // 메인 메뉴로 돌아가거나 종료. 임시로 종료 처리
        SceneManager.LoadScene("TitleScene"); // 타이틀 씬으로
    }
}
