using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healthRestore = 20;
    private PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.Heal(healthRestore);
            Destroy(this.gameObject);
        }
    }
}
