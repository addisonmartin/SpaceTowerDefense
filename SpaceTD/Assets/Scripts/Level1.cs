﻿using System.Collections;
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
        //Cullen
        timeToNextSpawn = 0;
        potentialSpawns = new Vector2[5];
        potentialSpawns[0] = new Vector2(-20, -20);
        potentialSpawns[1] = new Vector2(15, -20);
        potentialSpawns[2] = new Vector2(25, 15);
        potentialSpawns[3] = new Vector2(-12, -20);
        potentialSpawns[4] = new Vector2(5, -23);
    }

    // Update is called once per frame
    void Update()
    {
        //Cullen
        if (timeToNextSpawn <= 0) {
            Enemy e = Instantiate(enemy, new Vector3(-20, -20, 0), Quaternion.identity);
            Vector2 spawn = potentialSpawns[Random.Range(0, 5)];
            e.transform.position = new Vector3(spawn.x, spawn.y, 0);
            timeToNextSpawn = enemySpawn;
        } else {
            timeToNextSpawn -= Time.deltaTime;
        }

        if(Input.GetAxis("Cancel") == 1) {
            Application.Quit(0);
        }
    }

    
}
