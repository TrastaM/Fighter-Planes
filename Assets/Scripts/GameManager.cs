using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coin;
    public GameObject powerup;

    public AudioClip powerUp;
    public AudioClip powerDown;

    public int cloudSpeed;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI lifeText; 
    public TextMeshProUGUI powerupText;

    private int score;
    private int lives =3; 

    private GameObject playerInstance;

    void Start()
    {
        playerInstance = Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f); 
        CreateSky(); 
        InvokeRepeating("CreateCoin", 1f, 4f); 
        StartCoroutine(CreatePowerup());
        score = 0;
        scoreText.text = "Score: " + score; 
        lifeText.text = "Lives: " + lives; 
        isPlayerAlive = true;
        cloudSpeed = 1;
    }

    void Update()
    {
        Restart();
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    IEnumerator CreatePowerup()
    {
        Instantiate(powerup, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        StartCoroutine(CreatePowerup());
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void CreateCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
    }

    public void EarnScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score; 
    }

    public void LoseALife()
    {
        if (lives > 0)
        {
            lives--; 
            lifeText.text = "Lives: " + lives;
            if (lives <= 0)
            {
                GameOver(); 
            }
        }
    }


    void GameOver()
    {
        CancelInvoke("CreateEnemyOne");
        CancelInvoke("CreateCoin");
        Debug.Log("Game Over!");
    }

    public void Gameover()
    {
        isPlayerAlive = false;
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUp, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDown, Camera.main.transform.position);
    }
}
