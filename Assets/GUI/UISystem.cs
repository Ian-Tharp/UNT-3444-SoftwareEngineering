using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WC;

public class UISystem : MonoBehaviour
{
    public GameObject player;
    ShootGun sg;
    PlayerStats ps;

    [SerializeField] GameObject mousePos;
    [SerializeField] GameObject ammoPos;
    [SerializeField] RectTransform rt;

    [SerializeField] CardMenu cm;

    public Text ammoTxt;
    public Text healthTxt;
    public Text weaponTxt;
    public Text scoreTxt;
    public Text gameoverTxt;

    public float weaponAlpha;
    private float goAlpha;
    private string tweap;
    private float timer;

    private long tempScore;
    private float scoreRate;

    private int tempHealth;
    private float healthRate;
    private float healthAlpha;
    private float htimer;

    void Start()
    {
        player = GameObject.Find("HQ - Player");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        healthAlpha = .8f;
        weaponAlpha = .8f;
        timer = 0;
        htimer = 0;
        tempScore = 0;
        tempHealth = 100;
        scoreRate = 1;
        goAlpha = 0;
    }

    void FixedUpdate()
    {
        if (tempHealth > ps.CurrentHealth)
        {
            tempHealth -= 1;
            healthAlpha = .8f;
            htimer = 0;
        }else {
            if (tempHealth != ps.CurrentHealth)
            {
                healthAlpha = .8f;
                htimer = 0;
            }
            tempHealth = ps.CurrentHealth;
        }
        healthTxt.text = tempHealth.ToString();
        healthTxt.color = new Color(-ps.CurrentHealth * .01f +1, ps.CurrentHealth * .01f, 0, healthAlpha);
        htimer += .1f;
        if (htimer > 3)
            if (healthAlpha > 0)
                healthAlpha -= .09f;

        //ammo
        ammoPos.transform.position = mousePos.transform.position;
        ammoTxt.color = new Color(.8980f, .5686f, 0, .8f);
        if(sg.reloading)
        {
            ammoTxt.text = "Reloading...";
        }else{
            ammoTxt.text = sg.ammo.ToString() + "/" + sg.magSize.ToString();
        }

        //weapon
        if (tweap != sg.weapon.weaponName)
        {
            weaponAlpha = .8f;
            timer = 0;
        }
        
        weaponTxt.text = sg.weapon.weaponName;
        weaponTxt.color = new Color(.83f, .62f, .26f, weaponAlpha);
        timer += .1f;
        if (timer > 3)
            if (weaponAlpha > 0)
                weaponAlpha -= .09f;
        tweap = sg.weapon.weaponName;

        //score UI scripts
        scoreTxt.color = new Color(.9811f, .8731f, .0503f, .8f);
        if (ps.Score >= 123)
        {
            scoreRate = (ps.Score/(123));
        } else {
            scoreRate = 1;
        }

        if (tempScore < ps.Score)
        {
            tempScore += Convert.ToInt64(scoreRate);
        }else {
            tempScore = ps.Score;
        }
        scoreTxt.text = tempScore.ToString();
        scoreTxt.fontSize = Convert.ToInt32(ps.Score/10000 + 65);
        if (scoreTxt.fontSize >= 200)
            scoreTxt.fontSize = 200;
    }
    
    void Update()
    {
        if(Time.timeScale == 0)
        {
            rt.anchoredPosition = new Vector3(-119,-415, 0);
        }

        if (ps.isDead)
        {   
            goAlpha += .01f;
            gameoverTxt.color = new Color(.98f, .87f, .05f, goAlpha);
        } else 
        {
            gameoverTxt.color = new Color(.98f, .87f, .05f, 0);
        }

        if (cm.wavePause || ps.isDead)
        {
            ammoTxt.color = new Color(.98f, .87f, .05f, 0);
            healthTxt.color = new Color(.98f, .87f, .05f, 0);
            weaponTxt.color = new Color(.98f, .87f, .05f, 0);
            scoreTxt.color = new Color(.98f, .87f, .05f, 0); 
        }
    }
}
