using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    private Transform locationToMove;
    private EnemyStats enemy;
    private Rigidbody2D rb;
    private Vector2 Movement;

    public float Speed = 2.0f;
    public bool InPosition = false;

    void Start() {
        this.gameObject.GetComponent<DealMeleeDamage>().enabled = false;
        enemy = this.gameObject.GetComponent<EnemyStats>();

        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();
        GameObject temp = FindClosestLocation();
        Transform tempLoc = temp.GetComponent<Transform>();
        locationToMove = tempLoc;
    }

    void DetermineSpeed() {
        if (enemy.GetEnemyType() == 2) {
            Speed = 1.85f;
        }
        else if (enemy.GetEnemyType() == 3) {
            Speed = 1.75f;
        }
        else if (enemy.GetEnemyType() == 4) {
            Speed = 1.55f;
        }
        else if (enemy.GetEnemyType() == 5) {
            Speed = 1.65f;
        }
    }

    void Update() {
        if (InPosition == false) {
            //Calculate direction and angle for enemies to look at player
            Vector3 Direction = locationToMove.transform.position - this.transform.position;
            Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
            //Normalize direction for movement to smooth transition into rotations
            Direction.Normalize();
            Movement = Direction;
        }
    }

    //Returns the GameObject w/tag "ShootingLocation" that is the closest to the enemy
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

    //Update enemy movement
    void MoveEnemy(Vector2 Direction) {
        if (InPosition == false) {
            rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
        }
        else {
            rb.MovePosition((Vector2)transform.position + (Direction * 0 * Time.deltaTime));
        }
    }

    void FixedUpdate() {
        MoveEnemy(Movement);

        if (this.transform.position == locationToMove.position) {
            InPosition = true;
            Debug.Log("InPosition: " + InPosition);
        }
    }
}
