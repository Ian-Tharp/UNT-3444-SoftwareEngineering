using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public ParticleSystem explode;

    [SerializeField]
    private float Speed = 10.0f;

    [SerializeField]
    private int Damage;

    public TrailRenderer trail;
    private EnemyStats enemy;
    private PlayerStats player;

    void OnEnable() {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        float RandomTime = Random.Range(2, 7);
        yield return new WaitForSeconds(RandomTime);
        ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    

    void Update() {
        //Shoot projectile
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            enemy = collision.gameObject.GetComponent<EnemyStats>();
            Damage = enemy.GetEnemyDamage();
            Speed = enemy.GetProjectileSpeed();
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Player") {
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>(), false);
            player = collision.gameObject.GetComponent<PlayerStats>();
            if (player.hasShield == true) {
                player.SubtractCurrentShields(Damage);
            }
            else {
                player.SubtractCurrentHealth(Damage);
            }
            ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Collider") {
            ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Bullet") {
            ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") {
            ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(int Amount) {
        Damage = Amount;
    }

    void OnDestroy() {
        if (trail != null) {
            trail.transform.parent = null;
            trail.autodestruct = true;
            trail = null;
        }
    }
}
