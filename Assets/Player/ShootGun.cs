using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;
using WC;


public class ShootGun : MonoBehaviour
{
    //Check if ammo is depleted
    //Check if player is reloading
    //Check if shot hits an enemy
    //    if so, deal damage and update enemy health
    
    [SerializeField]
    private GameObject bullet; // bullet object
    
    [SerializeField]
    private Transform bulletDirection; // bullet transform where bullet is spawned and shot from

    public AudioSource source;
    private AudioClip gunshotSFX;
    private AudioClip emptySFX;
    private AudioClip rstartSFX;
    private AudioClip rfinSFX;

    Vector2 mousePosition;
    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction fire; // declaring an inputaction for fire function
    private InputAction reload;

    public GameObject IS;
    public InventorySystem InvSys;
    public WeaponsClass weapon;

    public bool automatic = true; // false - single shot, true - automatic
    public float fireRate = 600f; // rounds per minute
    private float ROF = 0f; // variable converting fire rate to wait time
    public float recoil = 3; // recoil rate of the gun (1-low 10-high)
    public float recoilBuildup = 0;  //variable of recoil building up over time and dropping
    public int ammo; // amount of bullets currently in a mag
    public int magSize = 30; // max size of a magazine
    public float reloadTime = 1.0f; // time it takes to reload
    public bool reloading; 
    public bool firing; // while shooting
    
    // next three are necessary calls for new input system
    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();

        reload = playerControls.Player.Reload;
        reload.Enable();
        
        fire.performed += context => Fire();
        reload.performed += context2 => StartCoroutine(Reload());
    }

    private void OnDisable()
    {
        fire.Disable();
        reload.Disable();
    }

    
    void Start()
    {
        IS = GameObject.Find("InventorySystem");
        InvSys = IS.GetComponent<InventorySystem>();
        

        reloading = false;
        firing = false;
        recoilBuildup = 0f;

        weapon = InvSys.weaponInv[InvSys.weaponSel];
        ammo = 17;


        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        gunshotSFX = audioSources[0].clip;
        emptySFX = audioSources[1].clip;
        rstartSFX = audioSources[2].clip;
        rfinSFX = audioSources[3].clip;
    }
    
    void Update()
    {
        if(Time.timeScale == 0)
        {
            fire.Disable();
            reload.Disable();
        }  else {
            fire.Enable();
            reload.Enable();
        }
    }


    //Fixed update is update but the same rate for every system
    void FixedUpdate() {

        weapon = InvSys.weaponInv[InvSys.weaponSel];
        recoil = weapon.recoil;
        fireRate = weapon.rpm;
        magSize = weapon.ammo;
        automatic = weapon.automatic;
        reloadTime = weapon.reloadTime;

        ROF = 60/fireRate; //calculating rpm to actual wait time in between rounds fired

        //if weapon is automatic and left click held down, fire
        if(automatic && fire.IsPressed() && ammo > 0)
        {   
            Fire();
        }

        if(recoilBuildup > 0 && !firing) // if not shooting and recoil > 0, reduce buildup
        {
            recoilBuildup -= .9f;
        }else if(recoilBuildup <= 0){ // if buildup reduces below 0, set to 0
            recoilBuildup = 0f;
        }

        mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // calcuates position from world for mouse rather than from camera
        Vector3 targetDirection = mouseWorldPosition - transform.position; // point from current location
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // angle to shot
        bulletDirection.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90 + (Random.Range(-recoilBuildup, recoilBuildup)))); //point to mouse + recoil buildup
    }

    IEnumerator Reload ()
    {
        reloading = true;

        source.PlayOneShot(rstartSFX, 1.0f); //start reload sfx
        yield return new WaitForSeconds(reloadTime); //wait for reload
        source.PlayOneShot(rfinSFX, 1.0f); //reload finish sfx

        ammo = magSize;
        reloading = false;
        firing = false;
    }

    IEnumerator FiringWait()
    {
        firing = true;
        yield return new WaitForSeconds(ROF); //rate of fire wait
        firing = false;
    }

    public void Fire() // called anytime player fires
    {  

            if(reloading)
            {
                source.PlayOneShot(emptySFX, 1.0f);//trying to fire while reloading
            }

            if (!reloading && !firing)
            {
                if (ammo <= 0) //click to reload at 0
                {
                    StartCoroutine(Reload());
                    return;
                }

                recoilBuildup += recoil * .09f; //each shot adds recoil buildup over time
                ammo = ammo - 1;
                source.time= .1f; //weird sound effect in beginning, skips to 1/10th of sec
                source.PlayOneShot(gunshotSFX, 0.7f);
                GameObject b = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation); //create bullet from hq
                b.SetActive(true);


                // if mousePosition is by enemy position, then hit
                /* commented out since we changed the method of shooting
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);
                if (hit)
                {
                    Debug.Log("Raycast Hit");
                }   */
                StartCoroutine(FiringWait());
            } 


    }


    //when weapon switch called from inventory system, change ammo count to saved ammo
    //then play loading sfx
    public void WeapSwitch(int tammo)
    {
        ammo = tammo;
        source.PlayOneShot(rfinSFX, 1.0f); //reload finish sfx
    }
    

}

