using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WC;

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

    }
}