using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP = 100; // 몬스터 체력
    public int AttackPower = 10; // 몬스터 공격력
    public float MoveSpeed = 2f; // 이동 속도

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
        // 사망 처리 (애니메이션, 오브젝트 삭제 등)
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
