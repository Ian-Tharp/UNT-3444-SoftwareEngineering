using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsClass : MonoBehaviour
{

    public bool automatic;
    public int damage;
    public int ammo;
    public float reloadTime;
    public float rpm;
    public float speed;
    public float recoil;
    public string weaponName;
    

    public WeaponsClass(bool auto, int tdam, int tammo, float rTime, float trpm, float tspeed, float trecoil, string tname)
    {
        this.automatic = auto;
        this.damage = tdam;
        this.ammo = tammo;
        this.reloadTime = rTime;
        this.rpm = trpm;
        this.speed = tspeed;
        this.recoil = trecoil;
        this.weaponName = tname;
    
    }

    void Start()
    {
        WeaponsClass[] weapon = new WeaponsClass[3];
        weapon[0] = new WeaponsClass(false, 2, 17, 1.0f, 80, 50, 3, "Glock 17"); //pistol
        weapon[1] = new WeaponsClass(true, 1, 30, 1.4f, 600, 50, 4, "AK-47"); //AR
        weapon[2] = new WeaponsClass(false, 5, 1, 1.8f, 40, 80, 15, "Mosin Nagant"); //bolt-action
    }


}
