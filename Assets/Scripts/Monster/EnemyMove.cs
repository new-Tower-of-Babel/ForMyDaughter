using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public int nextMove; // ���� (-1, 1)
    public float moveSpeed = 2f; // �̵� �ӵ�
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �ʱ� Think ȣ��
        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);

        // Raycast�� ���� ���� Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            // ���� ��ȯ
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == -1; // ��������Ʈ ������
            CancelInvoke();
            Invoke("Think", 5);
        }
    }

    void Think()
    {
        // ���� ���� ���� (-1 �Ǵ� 1)
        nextMove = Random.Range(0, 2) == 0 ? -1 : 1;

        // ���� �ӵ� ���� (1f ~ 3f ����)
        moveSpeed = Random.Range(1f, 3f); // ���ϴ� �ӵ� ���� ����

        // �ִϸ��̼� �� ��������Ʈ ���� ����
        anim.SetInteger("WalkSpeed", Mathf.Abs(nextMove));
        spriteRenderer.flipX = nextMove == -1;

        // ���� Think ȣ�� �ð� ����
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
}
