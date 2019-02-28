using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    //Lukas
    //enum that stores the state of waves. Spawning, waiting for player,
    //counting down to next wave.
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public List<Enemy> possibleEnemies = new List<Enemy>();
    public List<Vector2> possibleEnemySpawnLocations = new List<Vector2>();

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    private int waveNum = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    //Lukas
    private void Start() {
        // GOTTA FIX LOL
        possibleEnemySpawnLocations.Add(new Vector2(-0.15f, -0.15f));
        possibleEnemySpawnLocations.Add(new Vector2(1.0f, .3f));
        possibleEnemySpawnLocations.Add(new Vector2(.3f, 1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(.8f, -0.15f));
        possibleEnemySpawnLocations.Add(new Vector2(-.3f, -0.15f));
        possibleEnemySpawnLocations.Add(new Vector2(-0.15f, .8f));
        possibleEnemySpawnLocations.Add(new Vector2(-.6f, 1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(-0.15f, -0.15f));
        waveCountdown = timeBetweenWaves;
    }
    /*
    public virtual void SetupLevel()
    {
        // Subclasses should override this and change the level.
        // Like which enemies will spawn, where, and how often.
    }
    */
    //Lukas
    private void Update() {
        if (!Core.freeze)
        {
            if (state == SpawnState.WAITING)
            {
                //Check if enemies are still alive
                if (EnemyIsAlive() == false)
                {
                    //Begin new round.
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    //Start spawinging wave
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

    }

    //Lukas
    void WaveCompleted() {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        //Check if all waves are done
        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        } else {
            nextWave++;

        }

    }

    //Lukas
    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            searchCountdown = 1f;
            //Search for objects named "Enemy"
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                return false;
            }
        }
        return true;
    }

    //Lukas
    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawn Wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        if (waveNum >= waves.Length)
        {
            waveNum = 0;
        }
        else
        {
            waveNum++;
        }
        //Spawn
        for (int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    //Lukas
    void SpawnEnemy(Transform _enemy) {
        //Spawn enemy
        Transform e = Instantiate(_enemy, new Vector3(-20, -20, 0), Quaternion.identity);
        Vector2 spawnLocation = possibleEnemySpawnLocations[waveNum];
        e.position = (Vector2)Camera.allCameras[0].ViewportToWorldPoint(new Vector3(spawnLocation.x, spawnLocation.y));
        //Instantiate(_enemy, new Vector3(30, 0, 0), transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }

}
