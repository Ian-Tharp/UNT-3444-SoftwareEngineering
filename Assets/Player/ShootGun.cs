using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class ShootGun : MonoBehaviour
{
    //Check if ammo is depleted
    //Check if player is reloading
    //Check if shot hits an enemy
    //    if so, deal damage and update enemy health
    
    Vector2 mousePosition;
    public int ammo; // ammount of bullets currently in a mag
    public int magSize = 10; // max size of a magazine
    public float reloadTime = 1.0f; // time it takes to reload
    private bool reloading; 
    // Update is called once per frame
    
    void Start()
    {
        ammo = magSize;
        reloading = false;
    }
    
    void Update() {
        if (reloading)
        {
            return;
        }

        if (ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        mousePosition = Mouse.current.position.ReadValue();
    }

    IEnumerator Reload ()
    {
        reloading = true;
        Debug.Log("Reloading...");
        
        yield return new WaitForSeconds(reloadTime);

        ammo = magSize;
        reloading = false;

    }

    public void Fire() // called anytime player fires
    {  
          
            if (!reloading)
            {
                Debug.Log("Fire!");
                Debug.Log("Vector 2 location of Mouse: " + mousePosition);

                ammo = ammo - 1;


                // if mousePosition is by enemy position, then hit
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
                if (hit)
                {
                    Debug.Log("Raycast Hit");
                }   
            }           
    }
    

}

