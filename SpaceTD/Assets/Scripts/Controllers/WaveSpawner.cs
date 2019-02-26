using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //enum that stores the state of waves. Spawning, waiting for player,
    //counting down to next wave.
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;

    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (EnemyIsAlive() == false)
            {
                Debug.Log("Inside");
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

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        //Check if all waves are done
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
                    nextWave++;

        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        Debug.Log("EnemyAL");
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //Search for objects named "Enemy"
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawn Wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn enemy
        Instantiate(_enemy, new Vector3(30, 0, 0), transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }

}
