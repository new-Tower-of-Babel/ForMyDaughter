using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    EnemyStats stats;

    public int nextMove; // 방향 (-1, 1)

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<EnemyStats>();

        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove * stats.MoveSpeed, rigid.velocity.y);

        // Raycast로 앞쪽 발판 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == -1;
            CancelInvoke();
            Invoke("Think", 5);
        }
    }

    void Think()
    {
        nextMove = Random.Range(0, 2) == 0 ? -1 : 1;
        stats.MoveSpeed = Random.Range(1f, 3f); // EnemyStats의 MoveSpeed 업데이트
        anim.SetInteger("WalkSpeed", Mathf.Abs(nextMove));
        spriteRenderer.flipX = nextMove == -1;
        Invoke("Think", Random.Range(2f, 5f));
    }
}
