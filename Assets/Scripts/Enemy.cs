using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3.0f;

    private GameObject player;
    private Rigidbody enemyRb;

    private bool isEnemyGround;


    // Start is called before the first frame update
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().isGround && isEnemyGround)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * enemySpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isEnemyGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isEnemyGround = false;
        }
    }
}
