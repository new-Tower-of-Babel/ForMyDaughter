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

    // ���� �Ϸ� ó��
    public void FinishGame()
    {
        // ���� ���� ���� �߰� (UI, ȿ���� ��)
        SceneManager.LoadScene("StoryScene");
    }

}
