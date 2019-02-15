using System.Collections.Generic;
using UnityEngine;



// Written by Cullen
// Updated by Addison to not just be for Level1, but to work for any level using polymorphism.
public abstract class Level : MonoBehaviour {

    public List<Enemy> possibleEnemies = new List<Enemy>();
    public List<Vector2> possibleEnemySpawnLocations = new List<Vector2>();
    public List<int> waves = new List<int>();
    public float enemySpawnRate;
    public float waveRate;

    private float timeToNextSpawn = 0;
    private float waveTimer = 0;
    private int waveNum;



    // Start is called before the first frame update
    // Written by Addison
    void Start() {
        
        SetupLevel();
        waveNum = 0;
        waveTimer = waveRate;
    }

    public virtual void SetupLevel() {
        // Subclasses should override this and change the level.
        // Like which enemies will spawn, where, and how often.
    }

    // Update is called once per frame
    void Update() {
        CheckEnemySpawn();
        CheckInput();
    }

    // Written by Cullen.
    // Updated by Addison to work for any number of enemies and spawn locations.
    private void CheckEnemySpawn() {
        if (waveNum >= waves.Count) {
            return;
            //waveNum %= waves.Count;
        }
        if (waveTimer <= 0) {
            if (timeToNextSpawn <= 0) {
                int enemyIndex = Random.Range(0, possibleEnemies.Count);
                Enemy e = Instantiate(possibleEnemies[enemyIndex], new Vector3(-20, -20, 0), Quaternion.identity);
                Vector2 spawnLocation = possibleEnemySpawnLocations[waveNum];
                e.transform.position = Camera.allCameras[0].ViewportToWorldPoint(new Vector3(spawnLocation.x, spawnLocation.y));
                e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y); //set enemy z to 0
                timeToNextSpawn = enemySpawnRate;

                // The rate at which enemies spawn speeds up during the level.
                if (enemySpawnRate > .1f) {
                    enemySpawnRate -= .05f;
                } else {
                    enemySpawnRate = .1f;
                }
                waves[waveNum]--;
                if (waves[waveNum] == 0) {
                    waveTimer = waveRate;
                    waveNum++;
                }

            } else {
                timeToNextSpawn -= Time.deltaTime;
            }
        } else {
            waveTimer -= Time.deltaTime;
        }
    }

    


    // Written by Addison
    private void CheckInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
