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
    public Text card2Txt;
    
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

       card2Txt.text = deck[card2ID];
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
}
