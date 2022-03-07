using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;
using WC;

public class InventorySystem : MonoBehaviour
{
    public WeaponsClassScript weaponRef;
    
    //Max inventory size
    public static int MAX = 3;

    public WeaponsClass[] weaponInv = new WeaponsClass[MAX];
    public ShootGun player;
    
    public PlayerInput playerControls; // using the created PlayerInput class 
    private InputAction next;
    private InputAction prev;

    public int weaponSel;
    public int[] savedAmmo = new int[MAX];

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
        weaponSel = 0; // weapon selected
        
        //
        WeaponsClass[] weaponList = new WeaponsClass[WeaponsClassScript.weaponNum];
        

        //weapons in inventory, set as default/debug
        weaponInv[0] = weaponRef.weapon[0];
        weaponInv[1] = weaponRef.weapon[1];
        weaponInv[2] = weaponRef.weapon[2];
    }


    void NextWeapon()
    {
        savedAmmo[weaponSel] = weaponInv[weaponSel].ammo;
        Debug.Log(savedAmmo[weaponSel]);
        //player.WeapSwitch(savedAmmo[weaponSel]);
        weaponSel +=1;
        if (weaponSel > MAX-1)
            weaponSel = 0;
    }

    void PreviousWeapon()
    {
        savedAmmo[weaponSel] = weaponInv[weaponSel].ammo;
        //player.WeapSwitch(savedAmmo[weaponSel]);
        weaponSel -=1;
        if (weaponSel < 0)
            weaponSel = MAX-1;
    }
}
