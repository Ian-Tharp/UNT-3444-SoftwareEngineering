using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int WaveNumber;
    public int EnemyCount;
    public int TotalEnemyCount;
    public int EnemyIndex;
    public double WaveMultiplier;
    public int RemainingEnemyCount;
    public bool CanSpawnWave = true;

    private bool killingEnemies;
    private EnemyStats es;

    [SerializeField]
    private GameObject statsObj;

    [SerializeField]
    private GameObject BasicMelee; //Basic Melee Enemy Type

    [SerializeField]
    private GameObject BasicMelee_b;

    [SerializeField]
    private GameObject BasicRanged; //Basic Ranged Enemy Type

    [SerializeField]
    private GameObject BasicRanged_b;
    
    [SerializeField]
    private GameObject Exploder; //Exploder 1 Enemy Type

    [SerializeField]
    private GameObject HeavyMelee; //Heavy Melee Enemy Type

    [SerializeField]
    private GameObject HeavyRanged; //Heavy Ranged Enemy Type

    GameObject[] Enemies; //GameObject list of enemies that are present or spawned in

    private GameObject player;
    PlayerStats ps;

    //Used to reset wave manager or restart game without going back to main menu
    public void ResetWaveManager() {
        WaveNumber = 0;
        EnemyCount = 0;
        TotalEnemyCount = 0;
        RemainingEnemyCount = 0;
        WaveMultiplier = 0;
        CanSpawnWave = true;
    }

    //Spacing out enemy spawn timer
    IEnumerator SpaceOutSpawn() {
        randomizer = Random.Range(1, 2);
        if (randomizer == 1) {
            randomizer = Random.Range(2, 7);
            yield return new WaitForSeconds(randomizer);
        }
        else {
            randomizer = Random.Range(3, 10);
            yield return new WaitForSeconds(randomizer);
        }
    }

    IEnumerator WaitToSpawnWave() {
        yield return new WaitForSeconds(2.5f);
    }

    //Calculate how many enemies to spawn per wave
    void DetermineWaveSize() {
        if (WaveNumber == 1) {
            EnemyCount = 5;
        }
        else if (WaveNumber == 2) {
            EnemyCount = 10;
        }
        else if (WaveNumber == 5) {
            EnemyCount = 30;
        }
        //Wave 10 All Exploders
        else if (WaveNumber == 10) {
            EnemyCount = 50;
        }
        else {
            EnemyCount += ((WaveNumber * 2) - WaveNumber) + 2;
        }
        TotalEnemyCount = EnemyCount;
    }

    //Public functions to instantiate enemies
    //Basic Melee Enemies
    public void SpawnBasicMelee1AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicMelee, Location, transform.rotation);
    }
    public void SpawnBasicMelee2AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicMelee_b, Location, transform.rotation);
    }

    //Basic Ranged Enemies
    public void SpawnBasicRanged1AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicRanged, Location, transform.rotation);
    }
    public void SpawnBasicRanged2AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(BasicRanged_b, Location, transform.rotation);
    }

    //Exploder Enemies
    public void SpawnExploderAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(Exploder, Location, transform.rotation);
    }

    //Heavy Melee Enemies
    public void SpawnHeavyMeleeAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyMelee, Location, transform.rotation);
    }

    //Heavy Ranged Enemies
    public void SpawnHeavyRangedAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyRanged, Location, transform.rotation);
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
            //SpawnExploderAtLocation(Location);
            //SpawnBasicMelee1AtLocation(Location);
            //SpawnBasicRanged1AtLocation(Location);
            randomizer = Random.Range(0, 2);
            if (randomizer == 0) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else {
                SpawnBasicMelee2AtLocation(Location);
            }
        }
        //Wave 4-9
        //20% chance to spawn basic ranged enemy
        //80% chance to spawn basic melee enemy
        else if (WaveNumber <= 9 && WaveNumber > 3 && WaveNumber != 5) {
            randomizer = Random.Range(0, 6);
            if (randomizer == 0 || randomizer == 3) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 1 || randomizer == 2) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else if (randomizer == 4) {
                SpawnBasicRanged2AtLocation(Location);
            }
            else {
                SpawnBasicMelee2AtLocation(Location);
            }
        }
        //Wave 5
        //All exploder enemies
        else if (WaveNumber == 5) {
            SpawnExploderAtLocation(Location);
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
        else if (WaveNumber > 10 && WaveNumber < 15) {
            randomizer = Random.Range(0, 9);
            if (randomizer == 0) {
                SpawnExploderAtLocation(Location);
            }
            else if (randomizer == 1 || randomizer == 2 || randomizer == 3) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer >= 4 && randomizer <= 6) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else {
                SpawnBasicMelee2AtLocation(Location);
            }
        }
        //Wave 15
        //50% chance to spawn exploder enemy
        //50% chance to spawn heavy melee
        else if (WaveNumber == 15) {
            randomizer = Random.Range(0, 1);
            if (randomizer == 0) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnHeavyMeleeAtLocation(Location);
            }
        }
        //Wave 16-19
        //25% chance to spawn basic melee enemy
        //13% chance to spawn heavy melee enemy
        //25% chance to spawn basic ranged enemy
        //25% chance to spawn heavy ranged enemy
        //13% chance to spawn exploder enemy
        else if (WaveNumber > 15 && WaveNumber < 20) {
            randomizer = Random.Range(0, 7);
            if (randomizer == 0) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else if (randomizer == 7) {
                SpawnBasicMelee2AtLocation(Location);
            }
            else if (randomizer == 1) {
                SpawnHeavyMeleeAtLocation(Location);
            }
            else if (randomizer == 2 || randomizer == 3) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 4 || randomizer == 5) {
                SpawnHeavyRangedAtLocation(Location);
            }
            else {
                SpawnExploderAtLocation(Location);
            }
        }
        //Wave 20 and above
        //will change spawning later but just continue old spawn percentages
        //to make it endless
        else {
            randomizer = Random.Range(0, 6);
            if (randomizer == 0) {
                randomizer = Random.Range(0, 1);
                if (randomizer == 0) {
                    SpawnBasicMelee1AtLocation(Location);
                }
                else {
                    SpawnBasicMelee2AtLocation(Location);
                }
            }
            else if (randomizer == 1) {
                SpawnHeavyMeleeAtLocation(Location);
            }
            else if (randomizer == 2 || randomizer == 3) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 4 || randomizer == 5) {
                SpawnHeavyRangedAtLocation(Location);
            }
            else {
                SpawnExploderAtLocation(Location);
            }
        }
    } 

    //Public Getters
    public int GetWaveNumber() {
        return WaveNumber;
    }

    public void StartWave() {
        CanSpawnWave = false;
        ps.AddCurrentHealth(ps.regenAmount);

        WaveNumber++;
        DetermineWaveSize();

        if (WaveNumber < 5)
        {
            WaveMultiplier = 1;
        }
        else
        {
            WaveMultiplier = 1 + (WaveNumber - 5) * .25;
        }

        //SpawnEnemy functions here
        for (int i = 0; i < EnemyCount; i++) {
            LocationToSpawnEnemy = DetermineSpawnLocation();
            SpawnEnemyAtLocation(LocationToSpawnEnemy);
            SpaceOutSpawn();
        }
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        RemainingEnemyCount = Enemies.Length;
        UnityEngine.Debug.Log("Enemies: " + Enemies.Length);
    }
    //--------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start() {
        killingEnemies = false;
        WaveNumber = 0;
        player = GameObject.Find("HQ - Player");
        ps = player.GetComponent<PlayerStats>();
        StartWave();
    }

    public void KillAllEnemies() {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemies.Length; i++) {
            GameObject EnemyToKill = Enemies[i];
            es = Enemies[i].GetComponent<EnemyStats>();
            es.Health = 0;
        }
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
            
        }

        if (ps.isDead && !killingEnemies)
        {
            killingEnemies = true;    
            KillAllEnemies();
        }
    }
}
