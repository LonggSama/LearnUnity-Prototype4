using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController Ins;

    [SerializeField] float playerSpeed = 5.0f;

    private PowerUp powerUp;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    [SerializeField] private GameObject powerupIndicator;

    public bool isGround;
    public bool isPowerUp;

    // Start is called before the first frame update
    void Awake()
    {
        //Ins = this;
        powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PowerUpIndicatorMove();
    }

    // Move player Forward by up and down
    void PlayerMove()
    {
        if (isGround)
        {
            float forwardInput = Input.GetAxis("Vertical");
            float horizontalTnput = Input.GetAxis("Horizontal");
            playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed);
            playerRb.AddForce(focalPoint.transform.right * horizontalTnput * playerSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && isPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerUp.powerUpStrength, ForceMode.Impulse);
            Debug.Log("An " + collision.gameObject.name + " with powerup set to " + isPowerUp + " and Power Strength is "+ powerUp.powerUpStrength);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            isPowerUp = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(5);
        isPowerUp = false;
        powerupIndicator.SetActive(false);
    }

    void PowerUpIndicatorMove()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.25f,0);
    }
}
