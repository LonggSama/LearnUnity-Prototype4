using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarderEnemy : MonoBehaviour
{
    [SerializeField] float pushStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 hardEnemyPush = (collision.gameObject.transform.position - transform.position);

            playerRb.AddForce(hardEnemyPush * pushStrength, ForceMode.Impulse);
        }
    }
}
