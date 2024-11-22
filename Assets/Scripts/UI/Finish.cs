using UnityEngine;

public class Finish : MonoBehaviour
{
    public static bool AllClearCheck = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        AllClearCheck = true;
        if (other.CompareTag("Player")) // �÷��̾ �����ߴ��� Ȯ��
        {
            GameManager.Instance.FinishGame(); // ���� ���� ó��
        }
    }
}

