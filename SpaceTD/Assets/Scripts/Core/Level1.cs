using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

    public Enemy enemy;
    public float enemySpawn;
    private float timeToNextSpawn;
    private Vector2[] potentialSpawns;

    // Start is called before the first frame update
    void Start() {
        //Cullen
        timeToNextSpawn = 0;
        potentialSpawns = new Vector2[7];
        potentialSpawns[0] = new Vector2(-1.15f, -1.15f);
        potentialSpawns[1] = new Vector2(1.15f, .3f);
        potentialSpawns[2] = new Vector2(.3f, 1.15f);
        potentialSpawns[3] = new Vector2(.8f, -1.15f);
        potentialSpawns[4] = new Vector2(-.3f, -1.15f);
        potentialSpawns[5] = new Vector2(-1.15f, .8f);
        potentialSpawns[6] = new Vector2(-.6f, 1.15f);
    }

    // Update is called once per frame
    void Update() {
        //To be implemented - pack spawning/waves
        //Cullen
        if (timeToNextSpawn <= 0) {
            Enemy e = Instantiate(enemy, new Vector3(-20, -20, 0), Quaternion.identity);
            Vector2 spawn = potentialSpawns[Random.Range(0, 7)];
            e.transform.position = Camera.allCameras[0].ViewportToWorldPoint(new Vector3(spawn.x, spawn.y));
            e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y);
            timeToNextSpawn = enemySpawn;
            if (enemySpawn > .1f) {
                enemySpawn -= .05f;
            } else {
                enemySpawn = .1f;
            }

        } else {
            timeToNextSpawn -= Time.deltaTime;
        }

        if (Input.GetAxis("Cancel") == 1) {
            Application.Quit(0);
        }
    }


}
