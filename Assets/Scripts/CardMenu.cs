using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WC;

public class CardMenu : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;

    public GameObject player;
    private GameObject stop;
    private GameObject inventory;

    public bool wavePause;
    public bool startFin;

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

    
    PlayerStats ps;
    ShootGun sg;
    Pause p;
    InventorySystem es;

    int card1ID;
    int card2ID;

    static int deckSize = 5;
    static int wdeckSize = 11;//change to weapon num invsystem
    string[] weaponDeck = new string[wdeckSize];

    string[] deck = new string[] {"Health", "Damage", "Regen", "Ammo Drum", "Turret"};
   

    void Start()
    {

        player = GameObject.Find("HQ - Player");
        stop = GameObject.Find("InputManager");
        inventory = GameObject.Find("InventorySystem");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        p = stop.GetComponent<Pause>();
        es = inventory.GetComponent<InventorySystem>();
        wavePause = false;
        
    }


    public void openCards()
    {

        Time.timeScale = 0;

        cardMenu.SetActive(true);

        wavePause = true;
        
        cardSelection();

    }

    public void cardSelection()
    {
        if (towerFull)
        {
            deckSize = deckSize - 1;
        }
        card1ID = Random.Range(0, wdeckSize);

        card2ID = Random.Range(0, deckSize);


        while (card2ID == card1ID)
        {
            card2ID = Random.Range(0, deckSize);
        }
        card1Txt.text = es.weapon[card1ID].weaponName;
        C1body();

        card2Txt.text = deck[card2ID];
        C2body();
        if (towerFull)
        {
            deckSize = deckSize + 1;
        }

    }
    
    
    public void cardChosen1(Text name)
    {
        es.weaponInv[es.weaponSel] = es.weapon[card1ID];
        sg.ammo = es.weaponInv[es.weaponSel].ammo;
        cardMenu.SetActive(false);
        wavePause = false;
        p.paused = false;
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
        }
        cardMenu.SetActive(false);
        wavePause = false;
    }
   
   public void healthUp()
   {
       ps.AddTotalHealth(25);
       p.paused = false; 
   }

   public void damageUp()
   {
       for (int i = 0; i < 3; i++)
       {
            es.weaponInv[i].damage = es.weaponInv[i].damage + 1;
       }
       p.paused = false;
   }

   public void regen()
   {
       ps.regenAmount = ps.regenAmount + 10;
       ps.AddCurrentHealth(10);
       p.paused = false;
   }
   
   public void ammoUp()
   {
       for (int i = 0; i < 3; i++)
       {
            es.weaponInv[i].ammo = (int)(1.5 * es.weaponInv[i].ammo);
       }
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
        p.paused = false;
   }

   void C1body()
    {
        if(card1ID == 0)//g17
                card1desc.text = "Pistol";
        if(card1ID == 1)//m1911
                card1desc.text = "Pistol";
        if(card1ID == 2)//DE
                card1desc.text = "Pistol";
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
        card1stat1.text = es.weapon[card1ID].damage + "\n" + es.weapon[card1ID].ammo + "\n" + es.weapon[card1ID].reloadTime;
        card1stat2.text = es.weapon[card1ID].rpm + "\n" + es.weapon[card1ID].speed + "\n" + es.weapon[card1ID].recoil;

   }

   void C2body()
   {
        if(card2ID == 0)//health
            card2desc.text = "Repair your base by a large amount";
        if(card2ID == 1)//damage
            card2desc.text = "Permanently increase the damage for all your weapons";
        if(card2ID == 2)//regen
            card2desc.text = "Repair your base by a small amount at the end of each wave";
        if(card2ID == 3)//ammo
            card2desc.text = "Increase the magazine size for all of your weapons by x 1.5";
        if(card2ID == 4)//turret
            card2desc.text = "Add an automated turret to destroy enemies within range";

   }
}
