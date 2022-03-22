using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollide : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats player;

    bool triggered = false;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            this.player = collision.gameObject.GetComponent<PlayerStats>();
            if (player.hasShield == true) {
                player.SubtractCurrentShields(25);
            }
            else {
                player.SubtractCurrentHealth(25);
            }
            triggered = true;
        }
        else {
            triggered = false;
        }
        
        if (triggered) {
            Destroy(this.gameObject);
        }
    }
}
