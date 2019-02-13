using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Addison
public class Level1 : Level
{

   // Written by Cullen
   // Updated by Addison to be in a subclass
   public override void SetupLevel()
   {
      possibleEnemySpawnLocations.Add(new Vector2(-20, -20));
      possibleEnemySpawnLocations.Add(new Vector2(15, -20));
      possibleEnemySpawnLocations.Add(new Vector2(35, 25));
      possibleEnemySpawnLocations.Add(new Vector2(-12, -20));
      possibleEnemySpawnLocations.Add(new Vector2(-5, -23));
      possibleEnemySpawnLocations.Add(new Vector2(-15, 30));
      possibleEnemySpawnLocations.Add(new Vector2(-30, 10));
   }
}
