using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToArea : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
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
    
    void Update() {
        
    }
}
