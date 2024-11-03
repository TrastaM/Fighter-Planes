using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject enemyB;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("CreateEnemyB", 3f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-9f, 9f), 9f, 0), Quaternion.identity);
    }
    // spawn enemy at y6f to y2.5f
    void CreateEnemyB()
    {
        Instantiate(enemyB, new Vector3(-14f, Random.Range(6f, 2.5f), 0), Quaternion.identity);
    }
}
