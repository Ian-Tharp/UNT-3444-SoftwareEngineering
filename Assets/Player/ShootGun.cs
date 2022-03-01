using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;



public class ShootGun : MonoBehaviour
{
    //Check if ammo is depleted
    //Check if player is reloading
    //Check if shot hits an enemy
    //    if so, deal damage and update enemy health
    
    [SerializeField]
    private GameObject bullet;
    
    [SerializeField]
    private Transform bulletDirection;

    Vector2 mousePosition;
    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction fire; // declaring an inputaction for fire function

    public bool automatic = true; // false - single shot, true - automatic
    public float fireRate = 600f; // rounds per minute
    private float ROF = 0f; // variable converting fire rate to wait time
    public float recoil = 30; // recoil rate of the gun (1-low 30-high)
    private float recoilBuildup = 0;  //variable of recoil building up over time and dropping
    public int ammo; // amount of bullets currently in a mag
    public int magSize = 30; // max size of a magazine
    public float reloadTime = 1.0f; // time it takes to reload
    private bool reloading; 
    private bool firing; // while shooting
    
    // next three are necessary calls for new input system
    private void Awake()
    {
        playerControls = new PlayerInput();
        
    }
    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        
        fire.performed += context => Fire();
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    
    void Start()
    {
        ammo = magSize;
        automatic = true;
        reloading = false;
        firing = false;
        recoil = 10f;
        recoilBuildup = 0f;
    }
    
    //Fixed update is update but the same rate for every system
    void FixedUpdate() {
        
        ROF = 60/fireRate; //calculating rpm to actual wait time in between rounds fired

        //if weapon is automatic and left click held down, fire
        if(automatic && fire.IsPressed() && ammo > 0)
        {   
            Fire();
        }

        if(recoilBuildup > 0 && !firing)
        {
            recoilBuildup -= .9f;
        }else if(recoilBuildup <= 0){
            recoilBuildup = 0f;
        }

        mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        bulletDirection.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90 + (Random.Range(-recoilBuildup, recoilBuildup))));
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

    IEnumerator FiringWait()
    {
        firing = true;
        yield return new WaitForSeconds(ROF);
        firing = false;
    }

    public void Fire() // called anytime player fires
    {  

            if(reloading)
            {
                Debug.Log("Still reloading..."); //trying to fire while reloading
            }

            if (!reloading && !firing)
            {
                if (ammo <= 0) //click to reload at 0
                {
                    StartCoroutine(Reload());
                    return;
                }

                recoilBuildup += recoil * .09f; //
                //fireBullet(recoildBuildup)
                ammo = ammo - 1;
                GameObject b = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
                b.SetActive(true);
                Debug.Log(recoilBuildup);
                Debug.Log("Fire! Ammo: " + ammo + "/" + magSize);
                //Debug.Log("Input Mode:" + Input.GetMouseButtonDown(0)  + "Vector 2 location of Mouse: " + mousePosition);

                


                // if mousePosition is by enemy position, then hit
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
                if (hit)
                {
                    Debug.Log("Raycast Hit");
                }   
                StartCoroutine(FiringWait());
            } 


    }
    

}

