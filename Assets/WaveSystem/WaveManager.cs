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

    void StartWave() {
        CanSpawnWave = false;
        DetermineWave();
        //SpawnEnemy functions here
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
