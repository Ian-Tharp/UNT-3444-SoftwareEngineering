using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public GameObject player;
    ShootGun sg;
    public Text txt;

    void Start()
    {
        player = GameObject.Find("HQ - Player");
        sg = player.GetComponent<ShootGun>();
        int tammo =  sg.ammo;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = sg.ammo.ToString() + "/" + sg.magSize.ToString();
    }
}
