using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    public Enemy enemy;
    public float enemySpawn;
    private float timeToNextSpawn;
    private Vector2[] potentialSpawns;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextSpawn = 0;
        potentialSpawns = new Vector2[5];
        potentialSpawns[0] = new Vector2(-20, -20);
        potentialSpawns[1] = new Vector2(5, -10);
        potentialSpawns[2] = new Vector2(5, 15);
        potentialSpawns[3] = new Vector2(-12, -20);
        potentialSpawns[4] = new Vector2(20, -3);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToNextSpawn <= 0) {
            Instantiate(enemy, new Vector3(-20, -20, 0), Quaternion.identity);
            Vector2 spawn = potentialSpawns[(int)Random.Range(0, 4)];
            enemy.transform.position = new Vector3(spawn.x, spawn.y, 0);
            timeToNextSpawn = enemySpawn;
        } else {
            timeToNextSpawn -= Time.deltaTime;
        }
    }

    
}
