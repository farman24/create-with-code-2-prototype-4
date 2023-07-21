using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

using TMPro;

using Unity.VisualScripting;



using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRB;
    public GameObject focalpoint;
    bool hasPowerUp = false;
    private float powerUpStrength = 16.9f;
    public float normalCollisionForce;
    public float jumpForce;
    public float explosionForce;
    public float explosionForce2;
    public float dashForce;
    public GameObject powerRing;
    public GameObject powerRing2;
    public TextMeshProUGUI gameovertext;
    public Button restartButton;
    public GameObject backGround;
    public GameObject bullet;
    public ParticleSystem powerUp2Explosion;
    private bool ispressed = false;
    private bool ispressed2 = false;
    private bool ispressed3 = false;
    private bool ispressed4 = false;
    private bool canDash = true;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        restartScreen();
        if (transform.position.y > 8)
        {
            playerRB.AddForce(Vector3.down * jumpForce * 2 * Time.deltaTime, ForceMode.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
     
    }
    void playerMovement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalpoint.transform.forward * forwardInput * speed);
        powerRing.transform.position = transform.position+new Vector3(0, -0.5799866f,0);
        powerRing2.transform.position = transform.position;
        if (ispressed3)
        {
            playerRB.AddForce(focalpoint.transform.forward * 1 * speed);
        }
        if (ispressed4)
        {
            playerRB.AddForce(focalpoint.transform.forward * -1 * speed);
        }
      

    }
    
   
    public void forwardButtonDown()
    {
        ispressed3 = true;
    }
    public void forwardButtonUp()
    {
        ispressed3= false;
    }
    public void backButtonDown()
    {
        ispressed4 = true;
    }
    public void backButtonUp()
    {
        ispressed4= false;   
    }
    
    public void dashButton()
    {
        StartCoroutine(Dash());
    }
    void restartScreen()
    {
        if (gameObject.transform.position.y < -3)
        {
            gameovertext.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            backGround.gameObject.SetActive(true);
        }
    }
    public void restrtButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(powerUpCountDown());
            powerRing.SetActive(true);
        }

        if (other.gameObject.CompareTag("PowerUp1"))
        {

            Destroy(other.gameObject);
            StartCoroutine(sendRockets());
        }
        if (other.CompareTag("PowerUp2"))
        {
            Destroy(other.gameObject);
            StartCoroutine(powerUpCountDown());
            powerRing2.SetActive(true);
            StartCoroutine(Smash());

        }

    }

    IEnumerator powerUpCountDown()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerRing.SetActive(false);

    }

    IEnumerator sendRockets()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Code to set up power-up effect

        if (enemies.Length > 0)
        {
            for (int i = 0; i < 3; i++)
            {

                foreach (GameObject enemy in enemies)
                {
                    enemy enemyScript = GetComponent<enemy>();
                    GameObject homingRocket = Instantiate(bullet, transform.position, Quaternion.identity);
                    bulletForward rocketScript = homingRocket.GetComponent<bulletForward>();
                    rocketScript.SetTarget(enemy.transform);
                   

                }
                yield return new WaitForSeconds(1f);
            }
        }

    }
    IEnumerator Smash()
    {

        playerRB.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);

        var Enemies = FindObjectsOfType<enemy>();
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
            {
                Enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce2,
                transform.position, 20, 0.0f, ForceMode.Impulse);


            }
        }
        powerUp2Explosion.Play();
        yield return new WaitForSeconds(1);
        powerUp2Explosion.Stop();
        playerRB.velocity = Vector3.zero;

        playerRB.AddForce(Vector3.up * jumpForce * 10 * Time.deltaTime, ForceMode.Impulse);


        yield return new WaitForSeconds(0.43f);
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
            {
                Enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce2,
                transform.position, 20, 0.0f, ForceMode.Impulse);


            }
        }
        powerUp2Explosion.Play();
        yield return new WaitForSeconds(1);
        powerUp2Explosion.Stop();
        playerRB.velocity = Vector3.zero;
        powerRing2.SetActive(false);
    }
    IEnumerator Dash()
    {
        if (canDash==true)
        {
            playerRB.mass = 20;
            playerRB.AddForce(focalpoint.transform.forward * dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
            playerRB.mass = 1.5f;
            canDash= false;
        }
        
    }
    public void canDashTrue()
    {
        canDash = true;
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bouncyn = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(bouncyn * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with:" + collision.gameObject.name + "which has power up" + hasPowerUp);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bouncyn = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(bouncyn * normalCollisionForce, ForceMode.Impulse);
        }

    }
}