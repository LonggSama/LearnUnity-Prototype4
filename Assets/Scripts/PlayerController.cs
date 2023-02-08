using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController Ins;

    public PowerUpType currentPowerUp = PowerUpType.None;

    [SerializeField] float playerSpeed = 5.0f;

    [SerializeField] float powerUpStrength = 15f;

    [SerializeField] private GameObject powerupIndicator;

    [SerializeField] GameObject rocketPrefab;

    private GameObject tmpRocket;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private Coroutine powerupCountdown;
    private bool isFire = true;

    public bool isGround;
    public bool isPowerUp;

    // Start is called before the first frame update
    void Awake()
    {
        //Ins = this;
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PowerUpIndicatorMove();

        if (currentPowerUp == PowerUpType.Rockets && isFire)
        {
            LaunchRockets();
        }
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

        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Player collied with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, transform.rotation);
            tmpRocket.GetComponent<RocketsBehaviour>().Fire(enemy.transform);
            isFire = false;
        }
        StartCoroutine(LaunchRocketCountDown());
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
        if (other.CompareTag("PowerUp"))
        {
            isPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerUpCountDown());
        }
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(5);
        isPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.SetActive(false);
    }

    IEnumerator LaunchRocketCountDown()
    {
        yield return new WaitForSeconds(1);
        isFire = true;
    }

    void PowerUpIndicatorMove()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.25f,0);
    }
}
