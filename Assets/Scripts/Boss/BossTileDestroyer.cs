using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTileDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
    public GameObject destructionEffect; // 파괴 효과
    public AudioClip destructionSound; // 파괴 사운드

    private Collider2D bossCollider; // 보스의 Collider2D

    private void Start()
    {
        bossCollider = GetComponent<Collider2D>();
        if (bossCollider == null)
        {
            Debug.LogError("Collider2D가 보스 오브젝트에 필요합니다!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (tilemap != null && bossCollider != null)
        {
            // Collider2D의 bounds를 사용해 영역 계산
            Bounds bounds = bossCollider.bounds;

            // Collider 영역 내 모든 타일 제거
            RemoveTilesInArea(bounds);
        }
    }

    private void RemoveTilesInArea(Bounds bounds)
    {
        // Bounds의 최소 및 최대 위치를 기준으로 타일맵 좌표 계산
        Vector3Int minCell = tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

        // 영역 내 모든 타일 제거
        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(cellPosition))
                {
                    // 파괴 효과 생성
                    if (destructionEffect != null)
                    {
                        Instantiate(destructionEffect, tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
                    }

                    // 타일 제거
                    tilemap.SetTile(cellPosition, null);

                    // 파괴 사운드 재생
                    if (destructionSound != null)
                    {
                        AudioSource.PlayClipAtPoint(destructionSound, tilemap.GetCellCenterWorld(cellPosition));
                    }
                }
            }
        }
    }
}
