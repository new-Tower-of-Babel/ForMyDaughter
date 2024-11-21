using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 도착했는지 확인
        {
            GameManager.Instance.FinishGame(); // 게임 종료 처리
        }
    }
}

