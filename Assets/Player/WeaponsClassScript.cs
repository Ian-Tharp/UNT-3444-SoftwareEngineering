using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WC;

public class WeaponsClassScript : MonoBehaviour
{
    //the class used for all weapon references, create more below
    //increment weaponNum for each one you add
    public static int weaponNum = 3;
    public WeaponsClass[] weapon = new WeaponsClass[weaponNum];
    

    void Start()
    {
        //created weapons for the game here, includes all weapons
        weapon[0] = new WeaponsClass(false, false, 2, 17, .7f, 250, 50, 3, "Glock 17"); //pistol
        weapon[1] = new WeaponsClass(true, false, 1, 30, 1.1f, 600, 50, 4, "AK-47"); //AR
        weapon[2] = new WeaponsClass(false, true, 5, 1, 1.2f, 250, 80, 15, "Mosin Nagant"); //bolt-action
    }

}

namespace WC
{
    public class WeaponsClass
    {
        public bool automatic;
        public bool piercing;
        public int damage;
        public int ammo;
        public float reloadTime;
        public float rpm;
        public float speed;
        public float recoil;
        public string weaponName;
        

        public WeaponsClass(bool auto, bool tpierce, int tdam, int tammo, float rTime, float trpm, float tspeed, float trecoil, string tname)
        {
            automatic = auto;
            piercing = tpierce;
            damage = tdam;
            ammo = tammo;
            reloadTime = rTime;
            rpm = trpm;
            speed = tspeed;
            recoil = trecoil;
            weaponName = tname;
        
        }


        public bool getAutomatic()
        {
            return automatic;
        }

        public int getDamage ()
        {
            return damage;
        }

        public int getAmmo ()
        {
            return ammo;
        }

        public float getReloadTime ()
        {
            return reloadTime;
        }

        public float getRPM()
        {
            return rpm;
        }

        public float getSpeed()
        {
            return speed;
        }

        public float getRecoil ()
        {
            return recoil;
        }

        public string getWeaponName ()
        {
            return weaponName;
        }
    }
}