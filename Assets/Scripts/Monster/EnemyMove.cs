using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public int nextMove; // 방향 (-1, 1)
    public float moveSpeed = 2f; // 이동 속도
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 초기 Think 호출
        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);

        // Raycast로 앞쪽 발판 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            // 방향 전환
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == -1; // 스프라이트 뒤집기
            CancelInvoke();
            Invoke("Think", 5);
        }
    }

    void Think()
    {
        // 랜덤 방향 설정 (-1 또는 1)
        nextMove = Random.Range(0, 2) == 0 ? -1 : 1;

        // 랜덤 속도 설정 (1f ~ 3f 사이)
        moveSpeed = Random.Range(1f, 3f); // 원하는 속도 범위 지정

        // 애니메이션 및 스프라이트 방향 설정
        anim.SetInteger("WalkSpeed", Mathf.Abs(nextMove));
        spriteRenderer.flipX = nextMove == -1;

        // 다음 Think 호출 시간 설정
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
}
