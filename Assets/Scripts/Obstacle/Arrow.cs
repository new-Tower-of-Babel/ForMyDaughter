using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float playerRange = 5f;
    public float arrowSpeed = 5f;
    public float maxDistance = 15f;
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
            float distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
            if (distanceToPlayer <= playerRange)
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
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹 ó��
        if (collision.CompareTag("Player"))
        {
            // �÷��̾�� �������� �ش�
            PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.health -= damage;
            }

            // ȭ�� �ı�
            Destroy(gameObject);
        }
    }
    */
}
