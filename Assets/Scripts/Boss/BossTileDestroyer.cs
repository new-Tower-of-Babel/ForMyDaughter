using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTileDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // Ÿ�ϸ� ����
    public GameObject destructionEffect; // �ı� ȿ��
    public AudioClip destructionSound; // �ı� ����

    private Collider2D bossCollider; // ������ Collider2D
    private PlayerStats playerStats;

    private void Start()
    {
        bossCollider = GetComponent<Collider2D>();
        if (bossCollider == null)
        {
            Debug.LogError("Collider2D�� ���� ������Ʈ�� �ʿ��մϴ�!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (tilemap != null && bossCollider != null)
        {
            // Collider2D�� bounds�� ����� ���� ���
            Bounds bounds = bossCollider.bounds;

            // Collider ���� �� ��� Ÿ�� ����
            RemoveTilesInArea(bounds);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (playerStats != null && other.collider.CompareTag("Player"))
        {
            playerStats.Death();
        }
    }

    private void RemoveTilesInArea(Bounds bounds)
    {
        // Bounds�� �ּ� �� �ִ� ��ġ�� �������� Ÿ�ϸ� ��ǥ ���
        Vector3Int minCell = tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

        // ���� �� ��� Ÿ�� ����
        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(cellPosition))
                {
                    // �ı� ȿ�� ����
                    if (destructionEffect != null)
                    {
                        Instantiate(destructionEffect, tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
                    }

                    // Ÿ�� ����
                    tilemap.SetTile(cellPosition, null);

                    // �ı� ���� ���
                    if (destructionSound != null)
                    {
                        AudioSource.PlayClipAtPoint(destructionSound, tilemap.GetCellCenterWorld(cellPosition));
                    }
                }
            }
        }
    }
}
