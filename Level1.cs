using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Addison
public class Level1 : Level
{

   // Written by Cullen
   // Updated by Addison to be in a subclass
   //updated by Daniel to be offscreen and include the number of enemies in the wave
   public override void SetupLevel()
    {
        possibleEnemySpawnLocations.Add(new Vector2(-60, -30));
        waves.Add(4);
        possibleEnemySpawnLocations.Add(new Vector2(60, -45));
        waves.Add(5);
        possibleEnemySpawnLocations.Add(new Vector2(70, 50));
        waves.Add(5);
        possibleEnemySpawnLocations.Add(new Vector2(-24, -45));
        waves.Add(6);
        possibleEnemySpawnLocations.Add(new Vector2(-10, -45));
        waves.Add(6);
        possibleEnemySpawnLocations.Add(new Vector2(-30, 45));
        waves.Add(8);
        possibleEnemySpawnLocations.Add(new Vector2(-60, 15));
        waves.Add(10);
    }
}
