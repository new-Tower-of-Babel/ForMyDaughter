using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float damagePerSecond = 5f;
    private bool isDamaging = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDamaging)
        {
            //StartCoroutine(ApplyDamage(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            isDamaging = false;
        }
    }
/*
    private IEnumerator ApplyDamage(Collider player)
    {
        isDamaging = true;
        
        while (isDamaging)
        {
            
            PlayerInfo playerInfo = player.GetComponent<PlayerInfo>(); // PlayerInfo ��ũ��Ʈ�� �ִٰ� ����
            if (playerInfo != null)
            {
                playerInfo.health -= damagePerSecond;
                Debug.Log("Player damaged! Current health: " + playerInfo.health);
            }

            yield return new WaitForSeconds(1f); // 1�� �������� ������ ����
            
        }
    }
*/
}
