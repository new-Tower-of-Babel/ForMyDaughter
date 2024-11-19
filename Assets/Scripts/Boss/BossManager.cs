using UnityEngine;

public class BossManager : MonoBehaviour
{
    public BossTileDestroyer tileDestroyer; // 타일 파괴 관리
    public BossMovement movement;          // 보스 이동 관리

    private void Start()
    {
        // 타일 파괴 스크립트 초기화 (필요한 경우)
        if (tileDestroyer == null)
        {
            tileDestroyer = GetComponent<BossTileDestroyer>();
        }

        // 이동 스크립트 초기화 (필요한 경우)
        if (movement == null)
        {
            movement = GetComponent<BossMovement>();
        }

        if (tileDestroyer == null || movement == null)
        {
            Debug.LogError("BossManager: BossTileDestroyer 또는 BossMovement가 할당되지 않았습니다!");
            return;
        }

        Debug.Log("BossManager: 보스 관리 시작");
    }

    private void Update()
    {
        // 필요하다면 여기서 두 스크립트를 제어할 수 있습니다.
        // 예: 특정 조건에서 보스 움직임 멈춤
        if (SomeConditionToStopBoss())
        {
            StopBoss();
        }
    }

    private bool SomeConditionToStopBoss()
    {
        // 임의 조건 추가 (예: 플레이어와 일정 거리 등)
        return false;
    }

    public void StopBoss()
    {
        movement.enabled = false;          // 이동 멈춤
        tileDestroyer.enabled = false;     // 타일 파괴 비활성화
        Debug.Log("BossManager: 보스 정지");
    }

    public void ResumeBoss()
    {
        movement.enabled = true;           // 이동 재개
        tileDestroyer.enabled = true;      // 타일 파괴 활성화
        Debug.Log("BossManager: 보스 재시작");
    }
}
