using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public GameObject player;
    ShootGun sg;
    PlayerStats ps;
    public Text ammoTxt;
    public Text healthTxt;

    void Start()
    {
        player = GameObject.Find("HQ - Player");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = ps.CurrentHealth.ToString();
        healthTxt.color = new Color(-ps.CurrentHealth * .01f +1, ps.CurrentHealth * .01f, 0, .8f);

        if(sg.reloading)
        {
            ammoTxt.text = "Reloading...";
        }else{
            ammoTxt.text = sg.ammo.ToString() + "/" + sg.magSize.ToString();
        }

    }

}
