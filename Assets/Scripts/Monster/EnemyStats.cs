using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP = 100; // ���� ü��
    public int AttackPower = 10; // ���� ���ݷ�
    public float MoveSpeed = 2f; // �̵� �ӵ�

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ��� ó�� (�ִϸ��̼�, ������Ʈ ���� ��)
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
