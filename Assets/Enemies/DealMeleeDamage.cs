using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealMeleeDamage : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats player;
    private EnemyStats enemy;

    bool Repeatable = false;

    public float WaitTime = 1.5f;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = this.gameObject.GetComponent<EnemyStats>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            this.player = collision.gameObject.GetComponent<PlayerStats>();
            //Add Damage animation to sprite here
            //Add sound effects to enemy here
            //Add particle effects to enemy here

            //Refactor code later here
            if (player.hasShield == true) {
                player.SubtractCurrentShields(enemy.GetEnemyDamage());
            }
            else {
                player.SubtractCurrentHealth(enemy.GetEnemyDamage());
            }
            Repeatable = true;
        }
        if (Repeatable) {
            InvokeRepeating("DealDamage", 1.5f, WaitTime);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        Repeatable = false;
    }

    void DealDamage() {
        //Add Damage animation to sprite here
        //Add sound effects to enemy here
        //Add particle effects to enemy here
        if (this.player.hasShield == true) {
            this.player.SubtractCurrentShields(enemy.GetEnemyDamage());
        }
        else {
            this.player.SubtractCurrentHealth(enemy.GetEnemyDamage());
        }
    }
}
