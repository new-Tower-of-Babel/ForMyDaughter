using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2.0f; // ���� �̵� �ӵ�

    private void Update()
    {
        // ������ �׻� ���������� �̵� (Ȥ�� �ִϸ��̼� �� �߰� ����)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
