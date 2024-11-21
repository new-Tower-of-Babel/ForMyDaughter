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

    // 게임 완료 처리
    public void FinishGame()
    {
        // 게임 성공 연출 추가 (UI, 효과음 등)
        SceneManager.LoadScene("StoryScene");
    }

}
