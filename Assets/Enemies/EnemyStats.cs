using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class EnemyStats : MonoBehaviour
{
    public ParticleSystem parts;
    public ParticleSystem blood;
    public ParticleSystem explode;

    private GameObject player;
    PlayerStats ps;

    private WaveManager waveManager;

    public int Health = 0;
    public int Damage = 0;
    public int EnemyType = 0;
    public int EnemyValue = 0;
    public float ProjectileSpeed = 0;

    // Start is called before the first frame update
    void Start() {
        GameObject gameObj = GameObject.Find("WaveManagerSystem");
        waveManager = gameObj.GetComponent<WaveManager>();

        player = GameObject.Find("HQ - Player");
        ps = player.GetComponent<PlayerStats>();

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
    public float GetProjectileSpeed() {
        return ProjectileSpeed;
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
                if (waveManager.GetWaveNumber() > 5) {
                    Health += 1;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 2:
                Health = 5;
                Damage = 2;
                EnemyType = 2;
                EnemyValue = 2;
                ProjectileSpeed = 4.0f;
                if (waveManager.GetWaveNumber() > 5) {
                    Health += 1;
                }
                if (waveManager.GetWaveNumber() > 10) {
                    Damage += 1;
                }
                break;
            case 3:
                Health = 5;
                Damage = 3;
                EnemyType = 3;
                EnemyValue = 3;
                ProjectileSpeed = 5.0f;
                if (waveManager.GetWaveNumber() > 5) {
                    Health += 1;
                }
                if (waveManager.GetWaveNumber() > 10) {
                    Damage +=1;
                }
                break;
            case 4:
                Health = 8;
                Damage = 5;
                EnemyType = 4;
                EnemyValue = 5;
                if (waveManager.GetWaveNumber() > 5) {
                    Health += 2;
                    Damage += 1 + waveManager.GetWaveNumber() / 2;
                }
                break;
            case 5:
                Health = 14;
                Damage = 8;
                EnemyType = 5;
                EnemyValue = 8;
                if (waveManager.GetWaveNumber() > 5) {
                    Health += 2;
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
            
            ps.explosions += 1;
            ps.bloodSpilled += Random.Range(1.0f, 3.0f);

            if(!ps.isDead)
            {
                ps.AddScore((int)(EnemyValue * 10 * waveManager.WaveMultiplier * (Time.fixedTime/10)));
                ps.killed +=1;
                if (EnemyValue > 1)
                    ps.eliteKilled +=1;
            }
            Destroy(gameObject);
        }

    }


    void Hurt() {
        ParticleSystem effect = Instantiate(parts, transform.position, Quaternion.identity);
        ParticleSystem bloodfx = Instantiate(blood, transform.position, Quaternion.FromToRotation(transform.position, Vector3.zero)); 
        ps.bloodSpilled += Random.Range(0.1f, 2.0f);
    }
}
