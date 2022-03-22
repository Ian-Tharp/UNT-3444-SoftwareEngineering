using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealMeleeDamage : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats player;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

    }
}
