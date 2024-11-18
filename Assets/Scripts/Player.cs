using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private float speed;
    private int lives;
    private int shooting;
    private bool hasShield;

    public GameManager gameManager;

    public GameObject bullet;
    public GameObject explosion;
    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        shooting = 1;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput,0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenSize || transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30f)); 
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
        }
    }

    public void LoseALife()
    {
        if (hasShield == false)
        {
            lives--;
        } else if (hasShield == true)
        {
            //lose the shield
            //no longer have a shield
        }

        if (lives == 0)
        {
            gameManager.Gameover();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(4f);
        shooting = 1;
        gameManager.UpdatePowerupText("");
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(4f);
        shield.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            int powerupType = Random.Range (1,4); //This can be 1, 2, or 3
            switch(powerupType)
            {
                case 1:
                    //Speed powerup
                    speed = 9f;
                    gameManager.UpdatePowerupText("Speed Boost");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //Shield powerup
                    gameManager.UpdatePowerupText("Shield");
                    shield.gameObject.SetActive(true);
                    break;
                case 3:
                    //Multishot powerup
                    shooting = 3;
                    gameManager.UpdatePowerupText("Multishot");
                    StartCoroutine(ShootingPowerDown());
                    break;
            }
            Destroy(whatIHit.gameObject);
        }
    }
}
