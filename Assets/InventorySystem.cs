using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;
using WC;

public class InventorySystem : MonoBehaviour
{
    
    [SerializeField] CardMenu cm;
    
    //increment weaponNum for each one you add
    public static int weaponNum = 11;
    public WeaponsClass[] weapon = new WeaponsClass[weaponNum];

    //Max inventory size
    public static int MAX = 3;
    public int startWeap;

    public WeaponsClass[] weaponInv = new WeaponsClass[MAX];
    public GameObject HQ;
    public ShootGun player;
    
    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction next;
    private InputAction prev;

    public int weaponSel; //weapon in inventory that is selected
    public int[] savedAmmo = new int[MAX]; //ammo count saved in each weapon

    // next three are necessary calls for new input system
    private void Awake()
    {
        playerControls = new PlayerInput();
        
    }
    private void OnEnable()
    {
        next = playerControls.Player.Next;
        next.Enable();

        prev = playerControls.Player.Prev;
        prev.Enable();
        
        next.performed += context => NextWeapon();
        prev.performed += context2 => PreviousWeapon();
    }

    private void OnDisable()
    {
        next.Disable();
        prev.Disable();
    }


    void Start()
    {
        HQ = GameObject.Find("HQ - Player");
        player = HQ.GetComponent<ShootGun>();
        startWeap = 2;
        weaponSel = 0; // weapon selected
        
        //created weapons for the game here, includes all weapons
        //create more here

        //pistols
        weapon[0] = new WeaponsClass(false, false, 2, 17, .7f, 390, 65, 3, "Glock 17"); //pistol
        weapon[1] = new WeaponsClass(false, false, 3, 7, .9f, 390, 65, 4, "M1911A1"); 
        weapon[2] = new WeaponsClass(false, true, 5, 7, 1.1f, 140, 80, 30, "Desert Eagle");
        weapon[3] = new WeaponsClass(true, false, 1, 10, .7f, 800, 65, 5, "C96");

        //auto
        weapon[4] = new WeaponsClass(true, false, 1, 30, 1.1f, 600, 75, 4, "AK-47"); //AR
        weapon[5] = new WeaponsClass(true, false, 1, 30, 1.0f, 900, 80, 6, "MP5K");
        weapon[6] = new WeaponsClass(true, false, 1, 30, 1.2f, 800, 75, 3, "M4A1");
        weapon[7] = new WeaponsClass(true, false, 2, 40, 3.5f, 600, 80, 6, "RPK");

        //sniper/marksman
        weapon[8] = new WeaponsClass(false, true, 5, 1, 1.2f, 250, 80, 15, "Mosin Nagant"); //bolt-action
        weapon[9] = new WeaponsClass(false, false, 2, 10, 1.1f, 400, 75, 5, "SKS"); 
        weapon[10] = new WeaponsClass(false, true, 4, 10, 1.1f, 240, 80, 8, "SVD");

        //shotgun wip
        //weapon[2] = new WeaponsClass(false, false, 2, 8, 1.1f, 30, 80, 15, "590A1");
        

        //we are setting each item in inventory to a coded weapon in the weapons class script
        //when a gun is chosen during rewards section, replace one of the inv items with the reference to the chosen item
        //weapons in inventory, set as default/debug
        weaponInv[0] = weapon[1];
        weaponInv[1] = weapon[1];
        weaponInv[2] = weapon[1];

        //set saved ammo to max mag size;
        savedAmmo[0] = weaponInv[0].ammo;
        savedAmmo[1] = weaponInv[1].ammo;
        savedAmmo[2] = weaponInv[2].ammo;
    }

    void Update()
    {
        if(Time.timeScale == 0 && !cm.wavePause)
        {
            next.Disable();
            prev.Disable();
        }  else {
            next.Enable();
            prev.Enable();
        }
    }

    //when a weapon switch is triggered
    //change the weapon selected, and save the ammo count for when you switch back
    void NextWeapon()
    {
        savedAmmo[weaponSel] = player.ammo;
        if (startWeap < 2)
        {
            weaponSel +=1;
            
        
            if (weaponSel > MAX-1 || startWeap == 1 && weaponSel > 1)
                weaponSel = 0;

            player.WeapSwitch(savedAmmo[weaponSel]);
        }

    }

    void PreviousWeapon()
    {
        savedAmmo[weaponSel] = weaponInv[weaponSel].ammo;
        
        if (startWeap < 2)
        {
            weaponSel -=1;
            
        
            if (weaponSel < 0)
                if (startWeap != 1)
                {
                    weaponSel = MAX-1;
                } else {
                    weaponSel = 1;
                }

            player.WeapSwitch(savedAmmo[weaponSel]);
        }
    }
}
