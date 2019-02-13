using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Addison
public class Level1 : Level {

    // Written by Cullen
    // Updated by Addison to be in a subclass
    public override void SetupLevel() {

        possibleEnemySpawnLocations.Add(new Vector2(-1.15f, -1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(1.15f, .3f));
        possibleEnemySpawnLocations.Add(new Vector2(.3f, 1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(.8f, -1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(-.3f, -1.15f));
        possibleEnemySpawnLocations.Add(new Vector2(-1.15f, .8f));
        possibleEnemySpawnLocations.Add(new Vector2(-.6f, 1.15f));
    }
}
