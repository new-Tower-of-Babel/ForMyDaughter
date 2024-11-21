using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱��� ����

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
        // ���� ���� ���� �߰� (UI, ȿ���� ��)
        Invoke("QuitGame", 2f); // 2�� �� ���� ����
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Debug.Log("Returning to Main Menu...");
        // ���� �޴��� ���ư��ų� ����. �ӽ÷� ���� ó��
        SceneManager.LoadScene("TitleScene"); // Ÿ��Ʋ ������
    }
}
