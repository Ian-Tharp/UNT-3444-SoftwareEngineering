using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyProjectile;

    public GameObject playerHQ;
    private PlayerStats player;
    private Transform playerLocation;
    private EnemyStats enemy;
    private Rigidbody2D rb;
    private WaveManager waveManager;
    private MoveToLocation mtl;

    private Vector2 Movement;
    public float Speed = 2f;

    public float FireRate = 20.0f;
    private float ROF = 0.0f;
    public bool Firing;
    public float WaitTime = 2.0f;

    void Start() {
        this.gameObject.GetComponent<MoveTowardsPlayer>().enabled = false;
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = true;
        //this.gameObject.GetComponent<RotateAroundObject>().enabled = false;
        enemy = this.gameObject.GetComponent<EnemyStats>();

        if (enemy.GetEnemyType() == 2) {
            FireRate = 10.0f;
            Firing = false;
        }
        else if (enemy.GetEnemyType() == 3) {
            FireRate = 20.0f;
            Firing = false;
        }
        
        //Get player location
        GameObject tempP = GameObject.FindGameObjectWithTag("Player");
        Transform tempLocP = tempP.GetComponent<Transform>();
        playerLocation = tempLocP;

        GameObject wave = GameObject.FindGameObjectWithTag("WaveManager");
        waveManager = wave.GetComponent<WaveManager>();

        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();

        playerHQ = tempP;
    }

    void Update() {
        this.gameObject.GetComponent<MoveTowardsPlayer>().enabled = true;

        if (waveManager.GetRemainingEnemyCount() <= 6) {
            this.gameObject.GetComponent<MoveTowardsPlayer>().enabled = true;
            //this.gameObject.GetComponent<RotateAroundObject>().enabled = false;
        }
    }

    void FixedUpdate() {
        ROF = 60 / FireRate;
        if (playerHQ != null) {
            Shoot();
        }
    }

    IEnumerator WaitToShoot() {
        Firing = true;
        float RandomTime = Random.Range(3, 7);
        yield return new WaitForSeconds(RandomTime);
        Firing = false;
    }

    //Instantiate actual bullet being shot
    void Shoot() {
        if (!Firing) {
            GameObject bullet = Instantiate(EnemyProjectile, this.transform.position, this.transform.rotation);
            StartCoroutine(WaitToShoot());
        }
    }
}
