using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;

    private Vector2 Movement;
    public float Speed = 2f;

    void Start()
    {
        //Setup rigidbody component to allow for change of rotation
        rb = this.GetComponent<Rigidbody2D>();
 
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
        MoveEnemy(Movement);
    }

    //Update enemy movement
    void MoveEnemy(Vector2 Direction) {
        rb.MovePosition((Vector2)transform.position + (Direction * Speed * Time.deltaTime));
    }
}
