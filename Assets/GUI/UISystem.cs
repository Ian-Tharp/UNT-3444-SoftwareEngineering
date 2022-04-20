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

    void Start()
    {
        player = GameObject.Find("HQ - Player");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        weaponAlpha = .8f;
        timer = 0;
        tempScore = 0;
        scoreRate = 1;
        goAlpha = 0;
    }

    void FixedUpdate()
    {
        healthTxt.text = ps.CurrentHealth.ToString();
        healthTxt.color = new Color(-ps.CurrentHealth * .01f +1, ps.CurrentHealth * .01f, 0, .8f);


        //ammo
        ammoPos.transform.position = mousePos.transform.position;
        
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

        //weapon
        weaponTxt.text = sg.weapon.weaponName;
        weaponTxt.color = new Color(.83f, .62f, .26f, weaponAlpha);
        timer += .1f;
        if (timer > 3)
            if (weaponAlpha > 0)
                weaponAlpha -= .09f;
        tweap = sg.weapon.weaponName;

        //score UI scripts
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
    }
}
