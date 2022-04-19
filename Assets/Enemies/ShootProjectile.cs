using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyProjectile;

    private EnemyBullet bulletInfo;
    private PlayerStats player;
    private Transform locationToMove;
    private EnemyStats enemy;
    private Rigidbody2D rb;

    private Vector2 Movement;
    public float Speed = 2f;
    
    public float WaitTime = 2.0f;
    bool Repeatable = false;
    bool InPosition = false;

    void Start() {
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = false;
        enemy = this.gameObject.GetComponent<EnemyStats>();
        bulletInfo = EnemyProjectile.GetComponent<EnemyBullet>();

        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();
        GameObject temp = FindClosestLocation();
        Transform tempLoc = temp.GetComponent<Transform>();
        locationToMove = tempLoc;
    }

    GameObject FindClosestLocation() {
        GameObject[] locations;
        locations = GameObject.FindGameObjectsWithTag("ShootingLocation");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 Position = transform.position;
        foreach (GameObject location in locations) {
            Vector3 diff = location.transform.position - Position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = location;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Update() {
        //Check if enemy is in position to shoot
        if (InPosition) {

        }
        //If not continue rotating and moving to location
        else {
            //Calculate direction and angle for enemies to look at player
            Vector3 Direction = locationToMove.transform.position - this.transform.position;
            Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
            //Normalize direction for movement to smooth transition into rotations
            Direction.Normalize();
            Movement = Direction;
        }
    }

    void FixedUpdate() {
        MoveEnemy(Movement);
    }

    //Update enemy movement
    void MoveEnemy(Vector2 Direction) {
        rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ShootingLocation") {
            this.player = collision.gameObject.GetComponent<PlayerStats>();
            Shoot();
            Repeatable = true;
        }

        if (Repeatable) {
            InvokeRepeating("ShootProjectile", 2.0f, WaitTime);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        Repeatable = false;
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = true;
    }

    void Shoot() {
        bulletInfo.SetDamage(enemy.GetEnemyDamage());
        int damage = bulletInfo.Damage;
        GameObject bullet = Instantiate(EnemyProjectile, this.transform.position, this.transform.rotation);
    }
}
