using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    //Lukas
    //enum that stores the state of waves. Spawning, waiting for player,
    //counting down to next wave.
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public Text waveDisplay;

    [System.Serializable]
    public class Wave {
        public string name;
        public GameObject enemy;
        public int groups;
        public int perGroup;
        public float secondsBetween;
        public Vector2 location;
        public float timeUntilNextWave;
    }

    public Wave[] waves;
    //private int nextWave = 0;
    private int waveNum = 0;

    private float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    //Lukas
    public void Start() {
        //waveCountdown = timeBetweenWaves * 1.25f;
    }

    //Lukas
    public void Update() {
        if (!Core.freeze) {
            //if (state == SpawnState.WAITING) {
            //Check if enemies are still alive
            //removing the enemies alive check, this increases consistency in when each wave comes and where the obritals are
            //additionally, this creates advantages for the player, they know when the next wave will come and if they kill one wave quickly, 
            //they get more time to prepare for the next one
            //if (EnemyIsAlive() == false) {
            //    //Begin new round.
            //WaveCompleted();
            //} else {
            //    return;
            //}
            //}

            if (waveCountdown <= 0) {
                WaveCompleted();
                waveDisplay.text = waves[waveNum].name;
                if (state != SpawnState.SPAWNING && waveNum < waves.Length) {
                    //Start spawinging wave
                    StartCoroutine(SpawnWave(waves[waveNum]));
                }
                waveNum++;
            } else {
                waveCountdown -= Time.deltaTime;
            }
        }

    }

    //Lukas
    void WaveCompleted() {
        //Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;

        Core.waveComplete(waveNum);
        if (waveNum < waves.Length) {
            waveCountdown = waves[waveNum].timeUntilNextWave;
        }
        //Check if all waves are done
        //if (nextWave + 1 > waves.Length - 1) {
        //    nextWave = 0;
        //    Debug.Log("ALL WAVES COMPLETE! Looping...");
        //} else {
        //    nextWave++;
        //    
        //}

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
        //Debug.Log("Spawn Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.groups; i++) {
            spawnGroup(_wave);
            yield return new WaitForSeconds(_wave.secondsBetween);
        }
        //if (waveNum >= waves.Length - 1) {
        //waveNum = 0;
        //} else {
        //waveNum++;
        //}
        state = SpawnState.WAITING;
        //yield break; I think this causes extreme frame drops
    }

    //Lukas
    void spawnGroup(Wave wave) {
        //Cullen
        wave.enemy.GetComponent<Enemy>().spawn(wave.perGroup, wave.location, wave.enemy);

    }

}