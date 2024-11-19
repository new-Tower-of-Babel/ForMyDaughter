using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damagePerSecond;
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
            }

            yield return new WaitForSeconds(1f); // 1�� �������� ������ ����
        }
    }
*/
}
