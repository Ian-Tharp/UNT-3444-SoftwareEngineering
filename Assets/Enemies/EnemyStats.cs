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
    public AudioSource source;
    private AudioClip DeathSFX;
    

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

        source = GetComponent<AudioSource>();
        DeathSFX = source.clip;

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
            //Basic Melee 1
            case 1:
                Health = 1;
                Damage = 1;
                EnemyType = 1;
                EnemyValue = 1;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                    Damage += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 30) {
                    Health = 30;
                }
                if (Damage > 4) {
                    Damage = 4;
                }
                break;
            //Basic Ranged 1
            case 2:
                Health = 3;
                Damage = 2;
                EnemyType = 2;
                EnemyValue = 2;
                ProjectileSpeed = 3.0f;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                }
                if (waveManager.GetWaveNumber() > 30) {
                    Damage += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 30) {
                    Health = 30;
                }
                if (Damage > 5) {
                    Damage = 5;
                }
                break;
            //Heavy Ranged 1
            case 3:
                Health = 4;
                Damage = 3;
                EnemyType = 3;
                EnemyValue = 3;
                ProjectileSpeed = 4.0f;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                }
                if (waveManager.GetWaveNumber() > 30) {
                    Damage +=1;
                }
                //Maximums for difficulty scaling
                if (Health > 50) {
                    Health = 50;
                }
                if (Damage > 6) {
                    Damage = 6;
                }
                break;
            //Heavy Melee 1
            case 4:
                Health = 8;
                Damage = 5;
                EnemyType = 4;
                EnemyValue = 5;
                if (waveManager.GetWaveNumber() > 30) {
                    Health += 2;
                    Damage += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 60) {
                    Health = 60;
                }
                if (Damage > 6) {
                    Damage = 6;
                }
                break;
            //Basic Melee 2
            case 5:
                Health = 3;
                Damage = 2;
                EnemyType = 5;
                EnemyValue = 3;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                    Damage += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 30) {
                    Health = 30;
                }
                if (Damage > 6) {
                    Damage = 6;
                }
                break;
            //Exploder 2
            case 6:
                Health = 5;
                Damage = 1;
                EnemyType = 6;
                EnemyValue = 10;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 25) {
                    Health = 25;
                }
                break;
            //Exploder 1
            case 7:
                Health = 1;
                Damage = 1;
                EnemyType = 7;
                EnemyValue = 2;
                if (waveManager.GetWaveNumber() > 20) {
                    Health += 1;
                }
                //Maximums for difficulty scaling
                if (Health > 20) {
                    Health = 20;
                }
                break;
            //Swarmer
            case 8:
                Health = 1;
                Damage = 1;
                EnemyType = 8;
                EnemyValue = 1;
                if (waveManager.GetWaveNumber() > 10) {
                    Health += 1;
                }
                if (Health > 30) {
                    Health = 30;
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
            if (EnemyType < 6 || EnemyType > 8) {
                ParticleSystem effect = Instantiate(parts, transform.position, Quaternion.identity);
                ParticleSystem bloodfx = Instantiate(blood, transform.position, Quaternion.FromToRotation(transform.position, Vector3.zero));
            }

            ParticleSystem explofx = Instantiate(explode, transform.position, Quaternion.identity);
            source.PlayOneShot(DeathSFX, 1.0f);

            ps.explosions += 1;
            ps.bloodSpilled += Random.Range(1.0f, 3.0f);

            if(!ps.isDead)
            {
                ps.AddScore((int)(EnemyValue * 5 * waveManager.WaveMultiplier * (Time.fixedTime/10)));
                ps.killed +=1;
                if (EnemyValue > 1)
                    ps.eliteKilled +=1;
            }
            Destroy(gameObject);
        }

    }

    void Hurt() {
        if (EnemyType < 6 || EnemyType > 7) {
            ParticleSystem effect = Instantiate(parts, transform.position, Quaternion.identity);
            ParticleSystem bloodfx = Instantiate(blood, transform.position, Quaternion.FromToRotation(transform.position, Vector3.zero)); 
            ps.bloodSpilled += Random.Range(0.1f, 2.0f);
        }
    }
}
