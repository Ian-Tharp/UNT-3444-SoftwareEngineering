using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using WC;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explode;

    [SerializeField]
    private float speed = 20f; // standard bullet speed, subject to change

    [SerializeField]
    private int damage;

    public GameObject player;
    private PlayerStats pStats;
    public InventorySystem InvSys;
    public TrailRenderer trail;
    public WeaponsClass weapon;

    private void OnEnable() // allows for bullets to destroy after they are off map
    {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() // destory bullet after its gone for so long
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
    
    void Start()
    {
        player = GameObject.Find("InventorySystem");
        InvSys = player.GetComponent<InventorySystem>();
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        pStats = temp.GetComponent<PlayerStats>();
    }

    void Update() 
    {
        //weapon stats for current weapon being used
        weapon = InvSys.weaponInv[InvSys.weaponSel];
        damage = weapon.damage; // change damage # to current weapon

        transform.Translate(Vector3.up * speed * Time.deltaTime); // shoots projectile
    }

    private void OnTriggerEnter2D(Collider2D other) // triggers when runs into another collider
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyStats enemy = other.gameObject.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.SubtractHealth(damage + InvSys.damageAdd);
            }

            if (weapon.piercing == false) //if piercing false, then bullet stays until coroutine ends
                Destroy(gameObject);
        }

        if (other.gameObject.tag == "Projectile") {
            ParticleSystem explodeFX = Instantiate(explode, transform.position, Quaternion.identity);
            pStats.explosions += 1;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Protection") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>(), false);
        }
    }

     
      void OnDestroy() //when bullet is destroyed, unparent trail so it lingers
    {
        if (trail != null)
        {
            trail.transform.parent = null;
            trail.autodestruct = true;
            trail = null;
        }
    }
}
