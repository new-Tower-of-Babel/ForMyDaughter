using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float playerRange = 5f;
    public float arrowSpeed = 10f;
    public float maxDistance = 20f;
    public float damage = 20f;

    private GameObject player;
    private bool isFlying = false;
    private Vector3 startPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
    }

    void Update()
    {
        DetectAndShoot();
        HandleArrowMovement();
    }

    private void DetectAndShoot()
    {
        if (!isFlying && player != null)
        {
            float distanceToPlayerX = Mathf.Abs(player.transform.position.x - transform.position.x);
            float distanceToPlayerY = Mathf.Abs(player.transform.position.y - transform.position.y);

            bool isPlayerInPositiveDirection = player.transform.position.x > transform.position.x;
            bool isPlayerOnSameHeight = distanceToPlayerY <= 0.5f;

            if (distanceToPlayerX <= playerRange && isPlayerInPositiveDirection && isPlayerOnSameHeight)
            {
                isFlying = true;
            }
        }
    }

    private void HandleArrowMovement()
    {
        if (isFlying)
        {
            transform.Translate(Vector3.right * arrowSpeed * Time.deltaTime);

            if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.Hit(damage);
            }

            Destroy(gameObject);
        }
    }
}
