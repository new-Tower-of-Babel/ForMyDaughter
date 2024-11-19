using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 2.0f; // 보스 이동 속도

    private void Update()
    {
        // 보스는 항상 오른쪽으로 이동 (혹은 애니메이션 등 추가 가능)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
