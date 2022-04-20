using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;
using WC;


public class shootTurret : MonoBehaviour
{
    
    [SerializeField]
    private GameObject bullet; // bullet object

    public GameObject currTurret;
    turret t;
    
    
    public Transform missileLocationLeft;
    public Transform missileLocationRight;

    public AudioSource source;
    private AudioClip gunshotSFX;
    private AudioClip emptySFX;
    private AudioClip rstartSFX;
    private AudioClip rfinSFX;

    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction fire; // declaring an inputaction for fire function
    private InputAction reload;

    public bool automatic = true; // false - single shot, true - automatic
    public float fireRate = 30f; // rounds per minute
    private float ROF = 0f; // variable converting fire rate to wait time
    public float recoil = 0; // recoil rate of the gun (1-low 10-high)
    public float recoilBuildup = 0;  //variable of recoil building up over time and dropping
    public int ammo; // amount of bullets currently in a mag
    public int magSize = 60; // max size of a magazine
    public float reloadTime = 5.0f; // time it takes to reload
    public bool reloading; 
    public bool firing; // while shooting

    
    void Start()
    {     
        fireRate = 30f;
        ammo = 200000;
        magSize = 200000;
        reloadTime = 5.0f;

        reloading = false;
        firing = false;

        t = currTurret.GetComponent<turret>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        gunshotSFX = audioSources[0].clip;
        emptySFX = audioSources[1].clip;
        rstartSFX = audioSources[2].clip;
        rfinSFX = audioSources[3].clip;
    }


    //Fixed update is update but the same rate for every system
    void FixedUpdate() {

        ROF = 60/fireRate; //calculating rpm to actual wait time in between rounds fired

        //if weapon is automatic and left click held down, fire
        if(t.target != null && ammo > 0)
        {   
            tFire();
        }
        
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }

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

    public void tFire() // called anytime player fires
    {  


            if (!reloading && !firing)
            {
                if (ammo <= 0) //click to reload at 0
                {
                    StartCoroutine(Reload());
                    return;
                }

                ammo = ammo - 1;
                source.time= .1f; //weird sound effect in beginning, skips to 1/10th of sec
                source.PlayOneShot(gunshotSFX, 0.7f);
                GameObject b = Instantiate(bullet, missileLocationLeft.position, missileLocationLeft.rotation); //create bullet from turret
                b.SetActive(true);
                GameObject b2 = Instantiate(bullet, missileLocationRight.position, missileLocationRight.rotation); //create bullet from turret
                b2.SetActive(true);

                StartCoroutine(FiringWait());
            } 


    }   

}


