using UnityEngine;

public class BossManager : MonoBehaviour
{
    public BossTileDestroyer tileDestroyer; // Ÿ�� �ı� ����
    public BossMovement movement;          // ���� �̵� ����

    private void Start()
    {
        // Ÿ�� �ı� ��ũ��Ʈ �ʱ�ȭ (�ʿ��� ���)
        if (tileDestroyer == null)
        {
            tileDestroyer = GetComponent<BossTileDestroyer>();
        }

        // �̵� ��ũ��Ʈ �ʱ�ȭ (�ʿ��� ���)
        if (movement == null)
        {
            movement = GetComponent<BossMovement>();
        }

        if (tileDestroyer == null || movement == null)
        {
            Debug.LogError("BossManager: BossTileDestroyer �Ǵ� BossMovement�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        Debug.Log("BossManager: ���� ���� ����");
    }

    private void Update()
    {
        // �ʿ��ϴٸ� ���⼭ �� ��ũ��Ʈ�� ������ �� �ֽ��ϴ�.
        // ��: Ư�� ���ǿ��� ���� ������ ����
        if (SomeConditionToStopBoss())
        {
            StopBoss();
        }
    }

    private bool SomeConditionToStopBoss()
    {
        // ���� ���� �߰� (��: �÷��̾�� ���� �Ÿ� ��)
        return false;
    }

    public void StopBoss()
    {
        movement.enabled = false;          // �̵� ����
        tileDestroyer.enabled = false;     // Ÿ�� �ı� ��Ȱ��ȭ
        Debug.Log("BossManager: ���� ����");
    }

    public void ResumeBoss()
    {
        movement.enabled = true;           // �̵� �簳
        tileDestroyer.enabled = true;      // Ÿ�� �ı� Ȱ��ȭ
        Debug.Log("BossManager: ���� �����");
    }
}
