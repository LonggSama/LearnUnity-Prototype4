using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketsBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rocketStrength;
    [SerializeField] float aliveTimer;

    private Transform target;
    private bool homing;

    public void Fire (Transform homingTarget)
    {
        target = homingTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    private void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDir = (target.transform.position - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
