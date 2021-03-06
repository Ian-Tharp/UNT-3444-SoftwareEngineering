using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WC;

public class CardMenu : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;
    [SerializeField] WaveManager wm;
    [SerializeField] UISystem uiSys;
    [SerializeField] InventorySystem es;

    public GameObject player;
    private GameObject stop;
    //private GameObject inventory;

    public bool wavePause = false;

    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tower4;
    bool towerFull = false;

    public Text card1Txt;
    public Text card1desc;
    public Text card1stat1;
    public Text card1stat2;
    
    public Text card2Txt;
    public Text card2desc;

    //weapon compare stats
    public Text oldwTxt;
    public Text newwTxt;

    public Text os1Txt;
    public Text os2Txt;
    public Text os3Txt;
    public Text os4Txt;
    public Text os5Txt;
    public Text os6Txt;

    public Text ns1Txt;
    public Text ns2Txt;
    public Text ns3Txt;
    public Text ns4Txt;
    public Text ns5Txt;
    public Text ns6Txt;

    //upgrade stats
    public Text upgradeTxt;

    //wave complete
    public Text waveTxt;
    public Text scoreTxt;
    public Text timerTxt;
    
    PlayerStats ps;
    ShootGun sg;
    Pause p;
    //InventorySystem es;

    int card1ID;
    int card2ID;

    public int[] upgradeStats = new int[6] {0,0,0,0,0,0};  

    static int deckSize = 5;
    static int wdeckSize = 11;//change to weapon num invsystem
    string[] weaponDeck = new string[wdeckSize];

    int sel;
    float ammoMult;

    string[] deck = new string[] {"Health", "Damage", "Regen", "Ammo Drum", "Turret", "Barrier"};
   

    void Start()
    {

        player = GameObject.Find("HQ - Player");
        stop = GameObject.Find("InputManager");
        //inventory = GameObject.Find("InventorySystem");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        p = stop.GetComponent<Pause>();
        //es = inventory.GetComponent<InventorySystem>();

        
    }


    public void openCards()
    {

        Debug.Log(ammoMult);

        Time.timeScale = 0;

        cardMenu.SetActive(true);

        wavePause = true;
        
        cardSelection();

    }

    public void cardSelection()
    {
        card1ID = Random.Range(0, wdeckSize);
        while (es.weaponInv[0].weaponName == es.weapon[card1ID].weaponName || es.weaponInv[1].weaponName == es.weapon[card1ID].weaponName || es.weaponInv[2].weaponName == es.weapon[card1ID].weaponName)
        {
            card1ID = Random.Range(0, wdeckSize);
        }

        card2ID = Random.Range(0, deckSize);
        while (towerFull && card2ID == 4)
        {
            card2ID = Random.Range(0, deckSize);
        }

        /*Change to rarity reroll later
        while (card2ID == card1ID)
        {
            card2ID = Random.Range(0, deckSize);
        }*/
        card1Txt.text = es.weapon[card1ID].weaponName;
        C1body();

        card2Txt.text = deck[card2ID];
        C2body();

    }
    
    
    public void cardChosen1(Text name)
    {
        if (es.startWeap == 1)
        {
            es.weaponSel = 2;
            es.startWeap -=1;
        }
        if (es.startWeap == 2)
        {
            es.weaponSel = 1;
            es.startWeap -=1;
        }
        es.weaponInv[es.weaponSel] = es.weapon[card1ID];
        
        
        for (int i = 0; i < upgradeStats[4]; i++)
        {
             es.weaponInv[es.weaponSel].ammo = (int)(1.5 * es.weaponInv[es.weaponSel].ammo);
        }
        sg.ammo = es.weaponInv[es.weaponSel].ammo;

        cardMenu.SetActive(false);
        wavePause = false;
        p.paused = false;
        upgradeStats[0] +=1;
        uiSys.ShowWave();
    }

    public void cardChosen2(Text name)
    {
        if (name.text == "Health")
        {
            healthUp();
        }
        else if (name.text == "Damage")
        {
            damageUp();
        }
        else if (name.text == "Regen")
        {
            regen();
        }
        else if (name.text == "Ammo Drum")
        {
            ammoUp();
        }
        else if (name.text == "Turret")
        {
            defense();
        }/*
        else if (name.text == "Protection Barrier")
        {
            barrier();
        }*/
        cardMenu.SetActive(false);
        wavePause = false;
        uiSys.ShowWave();
    }
   

/// START CARD 2 ITEMS ////////////////////////
   public void healthUp()
   {
       ps.AddTotalHealth(25);
       upgradeStats[1] +=1;
       p.paused = false; 
   }

   public void damageUp()
   {
       upgradeStats[2] +=1;
       p.paused = false;
   }

   public void regen()
   {
       ps.regenAmount = ps.regenAmount + 10;
       ps.AddCurrentHealth(10);
       upgradeStats[3] +=1;
       p.paused = false;
   }
   
   public void ammoUp()
   {
       for (int i = 0; i < 3 - es.startWeap; i++)
       {
            es.weaponInv[i].ammo = (int)(1.5 * es.weaponInv[i].ammo);
            //reload all later
       }
       upgradeStats[4] +=1;
       p.paused = false;
   }

   public void defense()
   {
       if (!tower1.activeSelf)
       {
           tower1.SetActive(true);
       }
       else if (!tower2.activeSelf)
       {
           tower2.SetActive(true);
       }
        else if (!tower3.activeSelf)
       {
           tower3.SetActive(true);
       }
        else if (!tower4.activeSelf)
       {
           tower4.SetActive(true);
            towerFull = true;
       }
       upgradeStats[5] +=1;
        p.paused = false;
   }
/*
    public void barrier()
    {
        // set item to barrier or whatever script

        upgradeStats[6] += 1;
        p.paused = false;
    }*/

/// END CARD 2 ITEMS ///////////////////

   void C1body()
    {
        ammoMult = Mathf.Pow(1.5f, (float)upgradeStats[4]);
        
        if(card1ID == 0)//g17
                card1desc.text = "Pistol";
        if(card1ID == 1)//m1911
                card1desc.text = "Pistol";
        if(card1ID == 2)//DE
                card1desc.text = "Hand Cannon";
        if(card1ID == 3)//c96
                card1desc.text = "Machine Pistol";
        if(card1ID == 4)//ak
                card1desc.text = "Assault Rifle";
        if(card1ID == 5)//mp5
                card1desc.text = "Submachine Gun";
        if(card1ID == 6)//m4
                card1desc.text = "Assault Rifle";
        if(card1ID == 7)//rpk
                card1desc.text = "Light Machine Gun";
        if(card1ID == 8)//mos
                card1desc.text = "Bolt-Action Sniper Rifle";
        if(card1ID == 9)//sks
                card1desc.text = "Semi-Automatic Rifle";
        if(card1ID == 10)//svd
                card1desc.text = "Semi-Automatic Sniper Rifle";

        card1stat1.text = es.weapon[card1ID].damage+ "\n";
        card1stat1.text += es.weapon[card1ID].ammo + "\n" + es.weapon[card1ID].reloadTime;
        card1stat2.text = es.weapon[card1ID].rpm + "\n" + es.weapon[card1ID].speed + "\n" + es.weapon[card1ID].recoil;

        oldwTxt.text = es.weapon[card1ID].weaponName;
        if( upgradeStats[2] > 0)
        {
            os1Txt.text = es.weapon[card1ID].damage + " (+" + upgradeStats[2] + ")";
        }else{
            os1Txt.text = es.weapon[card1ID].damage.ToString();
        }

        if(upgradeStats[4] > 0)
        {
            os2Txt.text = es.weapon[card1ID].ammo + " (+" + (int)((es.weapon[card1ID].ammo* ammoMult)-es.weapon[card1ID].ammo) + ")";
        }else{
            os2Txt.text = es.weapon[card1ID].ammo.ToString();
        }

        os3Txt.text = es.weapon[card1ID].reloadTime.ToString();
        os4Txt.text = es.weapon[card1ID].rpm.ToString();
        os5Txt.text = es.weapon[card1ID].speed.ToString();
        os6Txt.text = es.weapon[card1ID].recoil.ToString();

   }

   void C2body()
   {
        if(card2ID == 0)//health
            card2desc.text = "Permanently increase your base health";
        if(card2ID == 1)//damage
            card2desc.text = "Permanently increase the damage for all your weapons";
        if(card2ID == 2)//regen
            card2desc.text = "Repair your base by a small amount at the end of each wave";
        if(card2ID == 3)//ammo
            card2desc.text = "Increase the magazine size for all of your weapons by x 1.5";
        if(card2ID == 4)//turret
            card2desc.text = "Add an automated turret to destroy enemies within range";
        /*
        if(card2ID == 5)//barrier
            card2desc.text = "Add an automated turret to destroy enemies within range";
        */
   }

    void Update()
    {

        
        //endwave stats
        waveTxt.text = "Wave " + (wm.WaveNumber -1) + " Complete";
        scoreTxt.text = "Score: " + ps.Score;
        timerTxt.text = "Time: " + Time.fixedTime + " sec";

        //weapon compare side
        sel = es.weaponSel;
        newwTxt.text = es.weaponInv[sel].weaponName;
        if (upgradeStats[2] > 0)
        {
            ns1Txt.text = es.weaponInv[sel].damage.ToString() + " (+" + upgradeStats[2].ToString() + ")";
        } else {
            ns1Txt.text = es.weaponInv[sel].damage.ToString();
        }
        if (upgradeStats[4] > 0)
        {
            ns2Txt.text = (int)(es.weaponInv[sel].ammo/ammoMult) + " (+" + (int)(es.weaponInv[sel].ammo - (es.weaponInv[sel].ammo/ammoMult)) + ")" ;
        } else {
            ns2Txt.text = es.weaponInv[sel].ammo.ToString();
        }
        ns3Txt.text = es.weaponInv[sel].reloadTime.ToString();
        ns4Txt.text = es.weaponInv[sel].rpm.ToString();
        ns5Txt.text = es.weaponInv[sel].speed.ToString();
        ns6Txt.text = es.weaponInv[sel].recoil.ToString();

        if(es.weaponInv[sel].damage < es.weapon[card1ID].damage)
        {
            os1Txt.color = new Color(0,.6f,0,1);
            ns1Txt.color = new Color(.6f,0,0,1);
        } else {
            os1Txt.color = new Color(.6f,0,0,1);
            ns1Txt.color = new Color(0,.6f,0,1);
        }
        if(es.weaponInv[sel].ammo < es.weapon[card1ID].ammo)
        {
            os2Txt.color = new Color(0,.6f,0,1);
            ns2Txt.color = new Color(.6f,0,0,1);
        } else {
            os2Txt.color = new Color(.6f,0,0,1);
            ns2Txt.color = new Color(0,.6f,0,1);
        }
        if(es.weaponInv[sel].reloadTime > es.weapon[card1ID].reloadTime)
        {
            os3Txt.color = new Color(0,.6f,0,1);
            ns3Txt.color = new Color(.6f,0,0,1);
        } else {
            os3Txt.color = new Color(.6f,0,0,1);
            ns3Txt.color = new Color(0,.6f,0,1);
        }
        if(es.weaponInv[sel].rpm < es.weapon[card1ID].rpm)
        {
            os4Txt.color = new Color(0,.6f,0,1);
            ns4Txt.color = new Color(.6f,0,0,1);
        } else {
            os4Txt.color = new Color(.6f,0,0,1);
            ns4Txt.color = new Color(0,.6f,0,1);
        }
        if(es.weaponInv[sel].speed < es.weapon[card1ID].speed)
        {
            os5Txt.color = new Color(0,.6f,0,1);
            ns5Txt.color = new Color(.6f,0,0,1);
        } else {
            os5Txt.color = new Color(.6f,0,0,1);
            ns5Txt.color = new Color(0,.6f,0,1);
        }
        if(es.weaponInv[sel].recoil > es.weapon[card1ID].recoil)
        {
            os6Txt.color = new Color(0,.6f,0,1);
            ns6Txt.color = new Color(.6f,0,0,1);
        } else {
            os6Txt.color = new Color(.6f,0,0,1);
            ns6Txt.color = new Color(0,.6f,0,1);
        }

        //upgrade stats side
        upgradeTxt.text = upgradeStats[0].ToString() + "\n" + upgradeStats[1].ToString() + "\n" + upgradeStats[2].ToString() + "\n";
        if (upgradeStats[5] > 3)
        {
            upgradeTxt.text += upgradeStats[3].ToString() + "\n" + upgradeStats[4].ToString() + "\nMAX\n";
        }else{
            upgradeTxt.text += upgradeStats[3].ToString() + "\n" + upgradeStats[4].ToString() + "\n" + upgradeStats[5].ToString() + "\n";
        }


        if (es.startWeap > 0)
        {
            newwTxt.text = "EMPTY SLOT";
            os1Txt.color = new Color(0,.6f,0,1);
            os2Txt.color = new Color(0,.6f,0,1);
            os3Txt.color = new Color(0,.6f,0,1);
            os4Txt.color = new Color(0,.6f,0,1);
            os5Txt.color = new Color(0,.6f,0,1);
            os6Txt.color = new Color(0,.6f,0,1);
            ns1Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns2Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns3Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns4Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns5Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns6Txt.color = new Color(0.8f,0.8f,0.8f,1);
            ns1Txt.text = "0";
            ns2Txt.text = "0";
            ns3Txt.text = "0";
            ns4Txt.text = "0";
            ns5Txt.text = "0";
            ns6Txt.text = "0";
            
        }
    }

}
