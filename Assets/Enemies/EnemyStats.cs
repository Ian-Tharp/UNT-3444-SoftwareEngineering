using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public ParticleSystem parts;
    public ParticleSystem blood;
    public ParticleSystem explode;

    private WaveManager waveManager;

    public int Health = 0;
    public int Damage = 0;
    public int EnemyType = 0;
    public int EnemyValue = 0;

    // Start is called before the first frame update
    void Start() {
        GameObject gameObj = GameObject.Find("WaveManagerSystem");
        waveManager = gameObj.GetComponent<WaveManager>();
        SetEnemyType(EnemyType);

    }

    //Public getters
    public int GetEnemyHealth() {
        return Health;
    }
    public int GetEnemyDamage() {
        return Damage;
    }
    public int GetEnemyType() {
        return EnemyType;
    }
    public int GetEnemyValue() {
        return EnemyValue;
    }
    //Public functions to allow for enemy stats to change during gameplay
    //Additions
    public void AddHealth(int Amount) {
        Health += Amount;
    }
    public void AddDamage(int Amount) {
        Damage += Amount;
    }

    //Subtractions
    public void SubtractHealth(int Amount) {
        //particle calls
        Hurt();
        Health -= Amount;
    }
    public void SubtractDamage(int Amount) {
        Damage -= Amount;
    }

    //Change enemy type function
    void SetEnemyType(int TypeSelector) {  
        //switch case here for determining health, damage, and currency based on type
        //balancing values to be determined
        switch (TypeSelector) {
            case 1:
                Health = 1;
                Damage = 1;
                EnemyType = 1;
                EnemyValue = 1;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 3;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 2:
                Health = 3;
                Damage = 2;
                EnemyType = 2;
                EnemyValue = 2;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 3;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 3:
                Health = 5;
                Damage = 3;
                EnemyType = 3;
                EnemyValue = 3;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 3;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 4:
                Health = 8;
                Damage = 5;
                EnemyType = 4;
                EnemyValue = 5;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 3;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 5:
                Health = 14;
                Damage = 8;
                EnemyType = 5;
                EnemyValue = 8;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 3;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            default:
                Health = 1;
                Damage = 1;
                EnemyType = 1;
                EnemyValue = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Health <= 0) {
            ParticleSystem effect = Instantiate(parts, transform.position, Quaternion.identity);
            ParticleSystem bloodfx = Instantiate(blood, transform.position, Quaternion.FromToRotation(transform.position, Vector3.zero));
            ParticleSystem explofx = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }


    void Hurt() {
        ParticleSystem effect = Instantiate(parts, transform.position, Quaternion.identity);
        ParticleSystem bloodfx = Instantiate(blood, transform.position, Quaternion.FromToRotation(transform.position, Vector3.zero));
        
    }
}
