using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTileDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // Ÿ�ϸ� ����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� Ÿ�ϸ����� Ȯ��
        if (collision.collider.CompareTag("Tilemap"))
        {
            Vector2 hitPosition = Vector2.zero;

            // �浹 �������� Ÿ���� ����
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x;
                hitPosition.y = hit.point.y;

                Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
                tilemap.SetTile(cellPosition, null); // Ÿ�� ����
            }
        }
    }
}


