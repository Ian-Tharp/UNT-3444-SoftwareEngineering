using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WC;

public class AmmoUI : MonoBehaviour
{
    public GameObject player;
    ShootGun sg;
    PlayerStats ps;
    public Text ammoTxt;
    public Text healthTxt;
    public Text weaponTxt;

    public float weaponAlpha;
    private string tweap;
    private float timer;

    void Start()
    {
        player = GameObject.Find("HQ - Player");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        weaponAlpha = .8f;
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthTxt.text = ps.CurrentHealth.ToString();
        healthTxt.color = new Color(-ps.CurrentHealth * .01f +1, ps.CurrentHealth * .01f, 0, .8f);

        if(sg.reloading)
        {
            ammoTxt.text = "Reloading...";
        }else{
            ammoTxt.text = sg.ammo.ToString() + "/" + sg.magSize.ToString();
        }

        if (tweap != sg.weapon.weaponName)
        {
            weaponAlpha = .8f;
            timer = 0;
        }
        weaponTxt.text = sg.weapon.weaponName;
        weaponTxt.color = new Color(.83f, .62f, .26f, weaponAlpha);
        timer += .1f;
        if (timer > 3)
            weaponAlpha -= .09f;
        tweap = sg.weapon.weaponName;
    }
    
}
