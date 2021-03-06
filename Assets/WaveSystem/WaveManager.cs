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
    private GameObject Exploder2;

    [SerializeField]
    private GameObject HeavyMelee; //Heavy Melee Enemy Type

    [SerializeField]
    private GameObject HeavyMelee2;

    [SerializeField]
    private GameObject HeavyRanged; //Heavy Ranged Enemy Type

    [SerializeField]
    private GameObject HeavyRanged2;

    [SerializeField]
    private GameObject Swarmer;

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
        if (WaveNumber > 10) {
            randomizer = Random.Range(4, 9);
            yield return new WaitForSeconds(randomizer);
        }
        else if (WaveNumber > 20) {
            randomizer = Random.Range(10, 20);
            yield return new WaitForSeconds(randomizer);
        }
        else if (WaveNumber > 30) {
            randomizer = Random.Range(15, 25);
            yield return new WaitForSeconds(randomizer);
        }
        else {
            randomizer = Random.Range(1, 2);
            if (randomizer == 1) {
                randomizer = Random.Range(3, 5);
                yield return new WaitForSeconds(randomizer);
            }
            else {
                randomizer = Random.Range(5, 10);
                yield return new WaitForSeconds(randomizer);
            }
        }
    }

    IEnumerator WaitToSpawnWave() {
        yield return new WaitForSeconds(3.5f);
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
            EnemyCount = 20;
        }
        //Wave 10 All Exploders
        else if (WaveNumber == 10) {
            EnemyCount = 40;
        }
        //Wave 20 All Exploders
        else if (WaveNumber == 20) {
            EnemyCount = 50;
        }
        //Wave 30
        else if (WaveNumber == 30) {
            EnemyCount = 75;
        }
        //Wave 40
        else if (WaveNumber == 40) {
            EnemyCount = 100;
        }
        //Wave 50
        else if (WaveNumber == 50) {
            EnemyCount = 125;
        }
        else {
            //EnemyCount += ((WaveNumber * 2) - (WaveNumber + 1)) + 2;
            EnemyCount += WaveNumber;
            if (WaveNumber > 10 && WaveNumber < 20) {
                EnemyCount -= 7;
            }
            if (WaveNumber > 20 && WaveNumber < 30) {
                EnemyCount -= 6;
            }
            if (WaveNumber > 30) {
                EnemyCount -= 5;
            }
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
    public void SpawnExploder2AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(Exploder2, Location, transform.rotation);
    }

    //Heavy Melee Enemies
    public void SpawnHeavyMeleeAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyMelee, Location, transform.rotation);
    }
    public void SpawnHeavyMelee2AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyMelee2, Location, transform.rotation);
    }

    //Heavy Ranged Enemies
    public void SpawnHeavyRangedAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyRanged, Location, transform.rotation);
    }
    public void SpawnHeavyRanged2AtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(HeavyRanged2, Location, transform.rotation);
    }

    //Swarmer Enemy
    public void SpawnSwarmerAtLocation(Vector2 Location) {
        GameObject Enemy = Instantiate(Swarmer, Location, transform.rotation);
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
            randomizer = Random.Range(0, 1);
            if (randomizer == 0) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else {
                SpawnBasicMelee2AtLocation(Location);
            }
        }
        //Wave 4-9
        //10% chance to spawn basic ranged enemy
        //50% chance to spawn basic melee enemy
        //40% chance to spawn basic melee 2
        else if (WaveNumber <= 9 && WaveNumber > 3 && WaveNumber != 5) {
            randomizer = Random.Range(1, 10);
            if (randomizer == 1) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 2 || randomizer == 3 || randomizer == 4 || randomizer == 5 || randomizer == 6) {
                SpawnBasicMelee1AtLocation(Location);
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
            randomizer = Random.Range(0, 13);
            if (randomizer == 0) {
                SpawnExploderAtLocation(Location);
            }
            else if (randomizer == 1) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 2) {
                SpawnBasicRanged2AtLocation(Location);
            }
            else if (randomizer >= 3 && randomizer <= 8) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else {
                SpawnBasicMelee2AtLocation(Location);
            }
        }
        //Wave 15
        //50% chance to spawn exploder enemy
        //50% chance to spawn exploder 2 enemy
        else if (WaveNumber == 15) {
            randomizer = Random.Range(0, 1);
            if (randomizer == 0) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }
        }
        //Wave 16-19
        //25% chance to spawn basic melee enemy
        //13% chance to spawn heavy melee enemy
        //25% chance to spawn basic ranged enemy
        //25% chance to spawn heavy ranged enemy
        //13% chance to spawn exploder enemy
        else if (WaveNumber > 15 && WaveNumber < 20) {
            randomizer = Random.Range(0, 10);
            if (randomizer == 0 || randomizer == 1) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else if (randomizer == 2 || randomizer == 3) {
                SpawnBasicMelee2AtLocation(Location);
            }
            else if (randomizer == 4) {
                SpawnHeavyMeleeAtLocation(Location);
            }
            else if (randomizer == 5 || randomizer == 6) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 7) {
                SpawnBasicRanged2AtLocation(Location);
            }
            else if (randomizer == 8) {
                SpawnHeavyRangedAtLocation(Location);
            }
            else if (randomizer == 9) {
                SpawnExploder2AtLocation(Location);
            }
            else {
                SpawnExploderAtLocation(Location);
            }
        }
        //Wave 20/30/40/...100 - All exploders
        else if (WaveNumber == 20) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }
        }
        else if (WaveNumber == 30) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }
        }
        else if (WaveNumber == 40) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }
        }
        else if (WaveNumber == 50) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,9);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
        }
        else if (WaveNumber == 60) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,8);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
        }
        else if (WaveNumber == 70) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,7);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
        }
        else if (WaveNumber == 80) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,6);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
        }
        else if (WaveNumber == 90) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,5);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
        }
        else if (WaveNumber == 100) {
            randomizer = Random.Range(1,2);
            if (randomizer == 1) {
                SpawnExploderAtLocation(Location);
            }
            else {
                SpawnExploder2AtLocation(Location);
            }

            randomizer = Random.Range(0,4);
            if (randomizer == 1) {
                SpawnExploder2AtLocation(Location);
            }
        }
        //will change spawning later but just continue old spawn percentages
        //to make it endless
        else {
            randomizer = Random.Range(0, 15);
            if (randomizer == 0) {
                SpawnBasicMelee1AtLocation(Location);
            }
            else if (randomizer == 1 || randomizer == 7) {
                SpawnHeavyMeleeAtLocation(Location);
            }
            else if (randomizer == 2) {
                SpawnBasicRanged1AtLocation(Location);
            }
            else if (randomizer == 3) {
                SpawnBasicRanged2AtLocation(Location);
            }
            else if (randomizer == 4) {
                SpawnHeavyRangedAtLocation(Location);
            }
            else if (randomizer == 6 || randomizer == 7 || randomizer == 8) {
                SpawnBasicMelee2AtLocation(Location);
            }
            else if (randomizer == 9) {
                SpawnExploder2AtLocation(Location);
            }
            else if (randomizer == 10 || randomizer == 12) {
                SpawnHeavyMelee2AtLocation(Location);
            }
            else if (randomizer == 11) {
                SpawnHeavyRanged2AtLocation(Location);
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
    public int GetRemainingEnemyCount() {
        return RemainingEnemyCount;
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
            if (WaveNumber > 10) {
                randomizer = Random.Range(1,10);
                if (randomizer == 1) {
                    SpawnSwarmerAtLocation(LocationToSpawnEnemy);
                }
            }
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

            //Clear out any stray bullets or projectiles from enemies
            //at the end of the wave
            GameObject[] Projectiles;
            Projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            for (int i = 0; i < Projectiles.Length; i++) {
                GameObject ProjectileToDestroy = Projectiles[i];
                Destroy(ProjectileToDestroy);
            }
        }

        if (ps.isDead && !killingEnemies)
        {
            killingEnemies = true;    
            KillAllEnemies();
        }
    }
}
