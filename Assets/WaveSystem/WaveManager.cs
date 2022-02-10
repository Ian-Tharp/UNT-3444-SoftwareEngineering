using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int WaveNumber;
    public int EnemyCount;
    public int TotalEnemyCount;
    public int EnemyIndex;
    public int RemainingEnemyCount;
    public bool CanSpawnWave = true;

    //Used to reset wave manager or restart game
    public void ResetWaveManager() {
        WaveNumber = 1;
        EnemyCount = 0;
        TotalEnemyCount = 0;
        RemainingEnemyCount = 0;
        CanSpawnWave = true;
    }

    //Calculate how many enemies to spawn per wave
    void DetermineWave() {
        if (WaveNumber == 1) {
            EnemyCount = 5;
        }
        else if (WaveNumber == 2) {
            EnemyCount = 10;
        }
        else {
            EnemyCount = 7 + (WaveNumber * 2);
        }
        TotalEnemyCount = EnemyCount;
    }

    void DecreaseEnemyCount() {
        if (RemainingEnemyCount > 0) {
            RemainingEnemyCount--;
        }
    }

    //Handle all locations to spawn enemies 
    GameObject[] SpawnLocations;
    Vector2 LocationToSpawnEnemy;

    //Find random spawn location - set spawn location for enemy at found location
    Vector2 DetermineSpawnLocation() {
        SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        int LocationsRange = SpawnLocations.Length;
        int RandomSelector = Random.Range(0, LocationsRange - 1);
        Vector2 WhereToSpawn = SpawnLocations[RandomSelector].transform.position;
        return WhereToSpawn;
    }

    public void SpawnEnemyAtLocation(Vector2 Location) {
        //Create new enemy gameobject here
        Debug.Log("Vector2 location of enemy: " + Location);
    }

    public void StartWave() {
        CanSpawnWave = false;
        DetermineWave();

        //SpawnEnemy functions here
        for (int i = 0; i < EnemyCount; i++) {
            LocationToSpawnEnemy = DetermineSpawnLocation();
            SpawnEnemyAtLocation(LocationToSpawnEnemy);
        }

        //After SpawnEnemy decrease total count
        DecreaseEnemyCount();

    }

    //Public Getters
    public int GetWaveNumber() {
        return WaveNumber;
    }

    // Start is called before the first frame update
    void Start() {
        WaveNumber = 1;
        StartWave();
    }

    // Update is called once per frame
    void Update() {
        if (RemainingEnemyCount > 0) {
            CanSpawnWave = false;
        }
        else {
            CanSpawnWave = true;
        }
    }
}
