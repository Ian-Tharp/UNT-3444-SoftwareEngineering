using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public int WaveNumber;
    public int EnemyCount;
    public int TotalEnemyCount;
    public int EnemyIndex;
    public int RemainingEnemyCount;
    public bool CanSpawnWave = false;
    int[] EnemyValue = new int[] {1, 2, 3, 5, 10};
    int TotalWaveScore;


    //Used to reset wave manager or restart game
    void ResetWaveManager() {
        WaveNumber = 0;
        EnemyCount = 0;
        TotalEnemyCount = 0;
        RemainingEnemyCount = 0;
        CanSpawnWave = false;
        TotalWaveScore = 0;
    }

    void DetermineWaveScore() {
        TotalWaveScore = 10 + WaveNumber * 3;
    }

    void SubtractWaveScore(int Amount) {
        TotalWaveScore -= Amount;
        if (TotalWaveScore <= 0) {
            TotalWaveScore = 0;
            CanSpawnWave = true;
        }
    }

    // Start is called before the first frame update
    void Start() {
        WaveNumber = 1;
        DetermineWaveScore();
    }

    // Update is called once per frame
    void Update() {
        if (TotalWaveScore > 0) {
            CanSpawnWave = false;
        }
    }
}
