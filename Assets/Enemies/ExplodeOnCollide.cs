using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollide : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats player;
    private EnemyStats enemy;

    [SerializeField] 
    private GameObject spawn;

    public ParticleSystem explode;

    bool triggered = false;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        enemy = this.gameObject.GetComponent<EnemyStats>();
        //Debug.Log(enemy.GetEnemyType());
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            this.player = collision.gameObject.GetComponent<PlayerStats>();
            if (player.hasShield == true) {
                player.SubtractCurrentShields(15);
            }
            else {
                player.SubtractCurrentHealth(15);
            }
            triggered = true;
        }
        else {
            triggered = false;
        }
        
        if (triggered) {
            ParticleSystem explofx = Instantiate(explode, transform.position, Quaternion.identity);
            player.explosions += 1;
            SpawnChild();
            Destroy(this.gameObject);
        }
    }
    void SpawnChild() {
        if (enemy.GetEnemyType() == 6) {
            GameObject e = Instantiate(spawn, transform.position, transform.rotation);
        }
    }
}
