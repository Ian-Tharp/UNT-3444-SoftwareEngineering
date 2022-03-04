using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int WaveNumber;
    public int EnemyCount;
    public int TotalEnemyCount;
    public int EnemyIndex;
    public int WaveScore;
    public int RemainingEnemyCount;
    public bool CanSpawnWave = true;

    [SerializeField]
    private GameObject EnemyToSpawn; //First Enemy Type

    GameObject[] Enemies; //GameObject list of enemies that are present or spawned in

    //Used to reset wave manager or restart game without going back to main menu
    public void ResetWaveManager() {
        WaveNumber = 0;
        EnemyCount = 0;
        TotalEnemyCount = 0;
        RemainingEnemyCount = 0;
        WaveScore = 0;
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

    //Handle all locations to spawn enemies 
    GameObject[] SpawnLocations;
    Vector2 LocationToSpawnEnemy;

    //Find random spawn location -> set spawn location for enemy at found location
    Vector2 DetermineSpawnLocation() {
        SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        int LocationsRange = SpawnLocations.Length;
        int RandomSelector = Random.Range(0, LocationsRange - 1);
        Vector2 WhereToSpawn = SpawnLocations[RandomSelector].transform.position;
        return WhereToSpawn;
    }

    public void SpawnEnemyAtLocation(Vector2 Location) {
        //Create new enemy gameobject here
        GameObject Enemy = Instantiate(EnemyToSpawn, Location, transform.rotation);
        Debug.Log("Vector2 location of enemy: " + Location);
    }

    public void StartWave() {
        CanSpawnWave = false;
        WaveNumber++;
        DetermineWave();

        //SpawnEnemy functions here
        for (int i = 0; i < EnemyCount; i++) {
            LocationToSpawnEnemy = DetermineSpawnLocation();
            SpawnEnemyAtLocation(LocationToSpawnEnemy);
        }
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        RemainingEnemyCount = Enemies.Length;
        Debug.Log("Enemies: " + Enemies.Length);
    }
    //--------------------------------------------------------------------------------
    //Public Getters
    public int GetWaveNumber() {
        return WaveNumber;
    }

    public int GetWaveScore() {
        return WaveScore;
    }

    // Start is called before the first frame update
    void Start() {
        WaveNumber = 0;
        StartWave();
    }

    // Update is called once per frame
    void Update() {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        RemainingEnemyCount = Enemies.Length;
        if (RemainingEnemyCount > 0) {
            CanSpawnWave = false;
        }
        else if (Enemies.Length == 0) {
            CanSpawnWave = true;
            StartWave();
        }

        //Check for if can spawn wave is available
        //If so, trigger the shop UI
        if (CanSpawnWave) {
            //Open Shop UI


        }
    }
}
