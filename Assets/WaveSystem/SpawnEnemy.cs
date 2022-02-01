using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //Public WaveManager reference variable
    //Used to access variables/functions from the referenced manager
    public WaveManager waveManager;
    //Finds all spawn locations to spawn enemies
    GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
    Vector2 LocationToSpawnEnemy;

    //Find random spawn location - set spawn location for enemy at found location
    Vector2 DetermineSpawnLocation() {
        int LocationsRange = SpawnLocations.Length;
        int RandomSelector = Random.Range(0, LocationsRange - 1);
        Vector2 WhereToSpawn = SpawnLocations[RandomSelector].transform.position;
        return WhereToSpawn;
    }

    void DetermineEnemyType() {
        int WaveNum = waveManager.GetWaveNumber();
        //Change enemy type here, including sprite, stats, etc.
    }
    
    void SpawnEnemyAtLocation(Vector2 Location) {
        //Create new enemy gameobject here
    }

    // Start is called before the first frame update
    void Start() {
        LocationToSpawnEnemy = DetermineSpawnLocation();
        SpawnEnemyAtLocation(LocationToSpawnEnemy);
    }

}
