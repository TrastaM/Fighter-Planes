using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player attributes
    private float speed;
    private int lives = 3;
    private int score = 0;

    public GameObject bullet;

    private float screenWidth;
    private float screenHeight;
    private float minY, maxY;

    void Start()
    {
        speed = 5f;

        // Get the screen boundaries based on the camera's orthographic size
        Camera cam = Camera.main;
        screenHeight = 2f * cam.orthographicSize;
        screenWidth = screenHeight * cam.aspect;

        // Set movement boundaries
        minY = cam.transform.position.y - screenHeight / 2;   // Bottom of screen
        maxY = cam.transform.position.y;                       // Middle of screen
    }

    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0);

        // Restrict player's Y position
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Wrap player's X position
        if (newPosition.x < -screenWidth / 2)
            newPosition.x = screenWidth / 2;
        else if (newPosition.x > screenWidth / 2)
            newPosition.x = -screenWidth / 2;

        // Apply the restricted position
        transform.position = newPosition;

        // Additional horizontal wrapping from the original Player script
        if (transform.position.x > 11f || transform.position.x <= -11f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Create a bullet
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
