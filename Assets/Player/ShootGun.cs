using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootGun : MonoBehaviour
{
    //Check if ammo is depleted
    //Check if player is reloading
    //Check if shot hits an enemy
    //    if so, deal damage and update enemy health

    // Update is called once per frame
    Vector2 mousePosition;
    
    void Update() {
        mousePosition = Mouse.current.position.ReadValue();
    }

    public void Fire() // called anytime player fires
    {  

       Debug.Log("Fire!");
       Debug.Log("Vector 2 location of Mouse: " + mousePosition);

       // if mousePosition is by enemy position, then hit

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
        if (hit)
        {
            Debug.Log("Raycast Hit");
        }          
    }
    

}

