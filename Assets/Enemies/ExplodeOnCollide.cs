using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollide : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
        if (player.hasShield == true) {
            player.SubtractCurrentShields(25);
        }
        Destroy(this);
    }
}
