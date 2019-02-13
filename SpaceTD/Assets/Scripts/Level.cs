using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Cullen
// Updated by Addison to not just be for Level1, but to work for any level using polymorphism.
public abstract class Level : MonoBehaviour
{

   public List<Enemy> possibleEnemies = new List<Enemy>();
   public List<Vector2> possibleEnemySpawnLocations = new List<Vector2>();
   public float enemySpawnRate;

   private float timeToNextSpawn = 0;

   // Start is called before the first frame update
   // Written by Addison
   void Start()
   {
      SetupLevel();
   }

   public virtual void SetupLevel()
   {
      // Subclasses should override this and change the level.
      // Like which enemies will spawn, where, and how often.
   }

   // Update is called once per frame
   void Update()
   {
      CheckEnemySpawn();
      CheckInput();
   }

   // Written by Cullen.
   // Updated by Addison to work for any number of enemies and spawn locations.
   private void CheckEnemySpawn()
   {
      if (timeToNextSpawn <= 0)
      {
         int enemyIndex = Random.Range(0, possibleEnemies.Count);
         Enemy e = Instantiate(possibleEnemies[enemyIndex], new Vector3(-20, -20, 0), Quaternion.identity);
         Vector2 spawnLocation = possibleEnemySpawnLocations[Random.Range(0, possibleEnemySpawnLocations.Count)];
         e.transform.position = new Vector3(spawnLocation.x, spawnLocation.y, 0);
         timeToNextSpawn = enemySpawnRate;

         // The rate at which enemies spawn speeds up during the level.
         if (enemySpawnRate > .1f)
         {
            enemySpawnRate -= .05f;
         } else
         {
            enemySpawnRate = .1f;
         }
      } else
      {
         timeToNextSpawn -= Time.deltaTime;
      }
   }

   // Written by Addison
   private void CheckInput()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #else
            Application.Quit();
         #endif
      }
   }
}
