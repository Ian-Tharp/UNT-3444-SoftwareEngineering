using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private EnemyStats enemy;

    private Vector2 Movement;
    public float Speed = 2f;

    void Start() {
        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        Transform tempLoc = temp.GetComponent<Transform>();
        player = tempLoc;
        enemy = this.GetComponent<EnemyStats>();

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
        //Calculate direction and angle for enemies to look at player
        Vector3 Direction = player.transform.position - this.transform.position;
        Quaternion Rotation = Quaternion.LookRotation(Vector3.forward, Direction);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Rotation, Time.deltaTime * Speed);
        //Normalize direction for movement to smooth transition into rotations
        Direction.Normalize();
        Movement = Direction;
    }

    void FixedUpdate() {
        if (enemy.GetEnemyType() == 2 || enemy.GetEnemyType() == 3) {
            MoveEnemy(Movement);

        }
        else {
            MoveEnemy(Movement);
        }
    }

    //Update enemy movement
    void MoveEnemy(Vector2 Direction) {
        if (enemy.GetEnemyType() == 2 || enemy.GetEnemyType() == 3) {
            rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
        }
        else {
            rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
        }
    }
}
