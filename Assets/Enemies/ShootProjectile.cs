using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyProjectile;

    private EnemyBullet bulletInfo;
    private PlayerStats player;
    private Transform playerLocation;
    private EnemyStats enemy;
    private Rigidbody2D rb;

    private Vector2 Movement;
    public float Speed = 2f;
    private float ProjectileSpeed = 15.0f;
    
    public float WaitTime = 2.0f;
    bool Repeatable = false;
    bool InPosition = false;

    //Public getters for EnemyBullet class
    public int GetDamage() {
        return enemy.GetEnemyDamage();
    }

    public float GetProjectileSpeed() {
        return ProjectileSpeed;
    }

    void Start() {
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = false;
        enemy = this.gameObject.GetComponent<EnemyStats>();
        bulletInfo = EnemyProjectile.GetComponent<EnemyBullet>();
        
        //Get player location
        GameObject tempP = GameObject.FindGameObjectWithTag("Player");
        Transform tempLocP = tempP.GetComponent<Transform>();
        playerLocation = tempLocP;

        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();

        if (enemy.GetEnemyType() == 2) {
            ProjectileSpeed = 12.0f;
        }
        else if (enemy.GetEnemyType() == 3) {
            ProjectileSpeed = 18.0f;
        }
    }

    void Update() {
        Vector3 Direction = playerLocation.transform.position;
        Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
        /*
        else {
            //Calculate direction and angle for enemies to look at player
            Vector3 Direction = playerLocation.transform.position - this.transform.position;
            Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
            //Normalize direction for movement to smooth transition into rotations
            Direction.Normalize();
            Movement = Direction;
        }
        */
    }

    void FixedUpdate() {
        if (InPosition == false) {
            /*Vector3 direction = playerLocation.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotation, Time.deltaTime * Speed);
            Movement = direction;
            */
            //MoveEnemy(Movement);
        }
        else {
            
        }
    }

    

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ShootingLocation") {
            Debug.Log("Collided w/ ShootingLocation");
            //this.player = collision.gameObject.GetComponent<PlayerStats>();
            Shoot();
            Repeatable = true;
            InPosition = true;
        }

        if (Repeatable) {
            InvokeRepeating("ShootProjectile", 1.5f, WaitTime);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        Repeatable = false;
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = true;
    }

    void Shoot() {
        bulletInfo.SetDamage(enemy.GetEnemyDamage());
        //int damage = bulletInfo.Damage;
        GameObject bullet = Instantiate(EnemyProjectile, this.transform.position, this.transform.rotation);
    }
}
