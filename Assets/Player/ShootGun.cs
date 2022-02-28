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
    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction fire; // declaring an inputaction for fire function

    public int ammo; // ammount of bullets currently in a mag
    public int magSize = 10; // max size of a magazine
    public float reloadTime = 1.0f; // time it takes to reload
    private bool reloading; 
    // Update is called once per frame
    
    // next three are necessary calls for new input system
    private void Awake()
    {
        playerControls = new PlayerInput();
    }
    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    
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
        

        mousePosition = Mouse.current.position.ReadValue();
    }

    IEnumerator Reload ()
    {
        reloading = true;
        Debug.Log("Reloading...");
        
        yield return new WaitForSeconds(reloadTime);

        ammo = magSize;
        reloading = false;
        Debug.Log("Loaded. Ammo: " + ammo + "/" + magSize);

    }

    public void Fire(InputAction.CallbackContext context) // called anytime player fires
    {  

            if(reloading)
            {
                Debug.Log("Still reloading..."); //trying to fire while reloading
            }

            if (!reloading)
            {
                if (ammo <= 0) //click to reload at 0
                {
                    StartCoroutine(Reload());
                    return;
                }

                ammo = ammo - 1;
                Debug.Log("Fire! Ammo: " + ammo + "/" + magSize);
                //Debug.Log("Input Mode:" + Input.GetMouseButtonDown(0)  + "Vector 2 location of Mouse: " + mousePosition);

                


                // if mousePosition is by enemy position, then hit
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
                if (hit)
                {
                    Debug.Log("Raycast Hit");
                }   
            }      
    }
    

}

