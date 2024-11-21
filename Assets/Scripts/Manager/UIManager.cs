using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingsUI;  // ���� UI
    public GameObject hpUI;        // HP UI
    public GameObject pauseMenu;   // �Ͻ����� �޴�
    private bool isPaused = false; // ���� �Ͻ����� ����

    private void Awake()
    {
        // ���� UI�� ��� ������ ����
        DontDestroyOnLoad(settingsUI);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        // Esc Ű�� ���� �޴� ����/�ݱ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �������� �� Ȯ��
        if (scene.name.Contains("Stage"))
        {
            hpUI.SetActive(true); // HP UI Ȱ��ȭ
        }
        else
        {
            hpUI.SetActive(false); // HP UI ��Ȱ��ȭ
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;  // ���� �Ͻ�����
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;  // ���� �簳
            pauseMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // �ð� �ʱ�ȭ
    }

    public void QuitToMainMenu()
    {
        // ���� �޴��� �̵�
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f; // �ð� �ʱ�ȭ
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
