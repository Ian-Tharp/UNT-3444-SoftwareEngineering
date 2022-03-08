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
    private GameObject BasicMelee; //Basic Melee Enemy Type

    [SerializeField]
    private GameObject BasicRanged; //Basic Ranged Enemy Type
    
    [SerializeField]
    private GameObject Exploder; //Ecploder 1 Enemy Type

    [SerializeField]
    private GameObject HeavyMelee; //Heavy Melee Enemy Type

    [SerializeField]
    private GameObject HeavyRanged; //Heavy Ranged Enemy Type

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

    //Spacing out enemy spawn timer
    IEnumerator SpaceOutSpawn() {
        int randomizer = Random.Range(1, 10);
        yield return new WaitForSeconds(randomizer);
    }

    IEnumerator WaitToSpawnWave() {
        yield return new WaitForSeconds(3.0f);
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

    //Declared int globally for optimization, less calls when instantiating new enemies
    int randomizer;
    public void SpawnEnemyAtLocation(Vector2 Location) {
        //Wave 1-3
        if (WaveNumber <= 3) {
            SpawnBasicMelee1AtLocation(Location);
        }
        //Wave 4-9
        //20% chance to spawn basic ranged enemy
        //80% chance to spawn basic melee enemy
        else if (WaveNumber <= 9 && WaveNumber > 3) {
            randomizer = Random.Range(0, 5);
            if (randomizer == 0) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else {
                SpawnBasicRanged1AtLocation(Location);
            }
        }
        //Wave 10
        //All exploder enemies
        else if (WaveNumber == 10) {
            SpawnExploderAtLocation(Location);
        }
        //Wave 11-14
        //10% chance to spawn exploder enemy
        //30% chance to spawn basic ranged enemy
        //60% chance to spawn basic melee enemy
        else if (WaveNumber >= 10 && WaveNumber < 15) {
            randomizer = Random.Range(0, 10);
            if (randomizer == 0) {
                SpawnExploderAtLocation(Location);
            }
            else if (randomizer == 1 || randomizer == 2 || randomizer == 3) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else {
                SpawnBasicMelee1AtLocation(Location);
            }
        }
    }

    //Public functions to instantiate enemies
    public void SpawnBasicMelee1AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicMelee, Location, transform.rotation);
        //Debug.Log("Vector2 location of enemy: " + Location);
    }
    public void SpawnBasicRanged1AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicRanged, Location, transform.rotation);
        //Debug.Log("Vector2 location of enemy: " + Location);
    }
    public void SpawnExploderAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(Exploder, Location, transform.rotation);
        //Debug.Log("Vector2 location of enemy: " + Location);
    }
    public void SpawnHeavyMeleeAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyMelee, Location, transform.rotation);
        //Debug.Log("Vector2 location of enemy: " + Location);
    }
    public void SpawnHeavyRangedAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyRanged, Location, transform.rotation);
        //Debug.Log("Vector2 location of enemy: " + Location);
    }  

    
    public void StartWave() {
        CanSpawnWave = false;
        WaveNumber++;
        DetermineWave();

        //SpawnEnemy functions here
        for (int i = 0; i < EnemyCount; i++) {
            LocationToSpawnEnemy = DetermineSpawnLocation();
            SpawnEnemyAtLocation(LocationToSpawnEnemy);
            SpaceOutSpawn();
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
            WaitToSpawnWave();
            StartWave();
        }

        //Check for if can spawn wave is available
        //If so, trigger the shop UI
        if (CanSpawnWave) {
            //Open Shop UI


        }
    }
}
