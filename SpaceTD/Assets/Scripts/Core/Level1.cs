using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Addison
public class Level1 : Level {

    // Written by Cullen
    // Updated by Addison to be in a subclass
    //updated by Daniel to be offscreen and include the number of enemies in the wave
    public override void SetupLevel() {

        possibleEnemySpawnLocations.Add(new Vector2(-1.0f, -1.0f));
        waves.Add(4);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(1.0f, .3f));
        waves.Add(5);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(.3f, 1.0f));
        waves.Add(5);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(.8f, -1.0f));
        waves.Add(6);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(-.3f, -1.0f));
        waves.Add(6);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(-1.0f, .8f));
        waves.Add(8);
        dirs.Add(new Vector2(1, 0));
        possibleEnemySpawnLocations.Add(new Vector2(-.6f, 1.0f));
        waves.Add(10);
        dirs.Add(new Vector2(1, 0));
    }
}
