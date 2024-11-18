using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTileDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 타일맵인지 확인
        if (collision.collider.CompareTag("Tilemap"))
        {
            Vector2 hitPosition = Vector2.zero;

            // 충돌 지점에서 타일을 제거
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x;
                hitPosition.y = hit.point.y;

                Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
                tilemap.SetTile(cellPosition, null); // 타일 삭제
            }
        }
    }
}


