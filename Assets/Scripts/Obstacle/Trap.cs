using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damagePerSecond = 10f;
    private bool isDamaging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDamaging)
        {
            StartCoroutine(ApplyDamage(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            isDamaging = false;
        }
    }

    private IEnumerator ApplyDamage(Collider2D player)
    {
        isDamaging = true;

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            yield break;
        }

        while (isDamaging)
        {
            playerStats.Hit(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }

}
