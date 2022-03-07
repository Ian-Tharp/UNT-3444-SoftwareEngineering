using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f; // standard bullet speed, subject to change

    [SerializeField]
    private int damage = 1;

    public TrailRenderer trail;

    private void OnEnable() // allows for bullets to destroy after they are off map
    {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() // destory bullet after its gone for so long
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
    
    void Update() 
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // shoots projectile
    }

    private void OnTriggerEnter2D(Collider2D other) // triggers when runs into another collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyStats enemy = other.gameObject.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.SubtractHealth(damage);
            }
        }

        if (other.gameObject.tag != "Player") // for when bullet leaves player
        {
        Destroy(gameObject);
        }
    }

     
      void OnDestroy() //when bullet is destroyed, unparent trail so it lingers
    {
        trail.transform.parent = null;
        trail.autodestruct = true;
        trail = null;
    }
}
