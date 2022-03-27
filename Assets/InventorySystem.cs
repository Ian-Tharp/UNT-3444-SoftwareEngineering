using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;
using WC;

public class InventorySystem : MonoBehaviour
{
    
    
    //increment weaponNum for each one you add
    public static int weaponNum = 3;
    public WeaponsClass[] weapon = new WeaponsClass[weaponNum];

    //Max inventory size
    public static int MAX = 3;

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
        weaponSel = 0; // weapon selected
        
        //created weapons for the game here, includes all weapons
        //create more here
        weapon[0] = new WeaponsClass(false, false, 2, 17, .7f, 390, 65, 3, "Glock 17"); //pistol
        weapon[1] = new WeaponsClass(true, false, 1, 30, 1.1f, 600, 75, 4, "AK-47"); //AR
        weapon[2] = new WeaponsClass(false, true, 5, 1, 1.2f, 250, 80, 15, "Mosin Nagant"); //bolt-action

        //we are setting each item in inventory to a coded weapon in the weapons class script
        //when a gun is chosen during rewards section, replace one of the inv items with the reference to the chosen item
        //weapons in inventory, set as default/debug
        weaponInv[0] = weapon[0];
        weaponInv[1] = weapon[1];
        weaponInv[2] = weapon[2];

        //set saved ammo to max mag size;
        savedAmmo[0] = weaponInv[0].ammo;
        savedAmmo[1] = weaponInv[1].ammo;
        savedAmmo[2] = weaponInv[2].ammo;
    }

    void Update()
    {
        if(Time.timeScale == 0)
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
        
        weaponSel +=1;
        if (weaponSel > MAX-1)
            weaponSel = 0;
        player.WeapSwitch(savedAmmo[weaponSel]);
    }

    void PreviousWeapon()
    {
        savedAmmo[weaponSel] = weaponInv[weaponSel].ammo;
        
        weaponSel -=1;
        if (weaponSel < 0)
            weaponSel = MAX-1;
        player.WeapSwitch(savedAmmo[weaponSel]);
    }
}
