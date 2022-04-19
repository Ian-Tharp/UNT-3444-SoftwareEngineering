using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float Speed = 15.0f;

    public int Damage;
    public TrailRenderer trail;

    void OnEnable() {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

    void Start() {
        
    }

    void Update() {
        //shoot projectile
        this.transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            
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
