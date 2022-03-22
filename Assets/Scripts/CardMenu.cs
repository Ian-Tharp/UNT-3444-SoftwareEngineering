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

    public Text card1Txt;
    public Text card2Txt;
    
    PlayerStats ps;
    ShootGun sg;
    Pause p;
    InventorySystem es;

    int card1ID;
    int card2ID;
    int deckSize = 4;
    string[] deck = new string[] {"Health", "Damage", "Regen", "Ammo Drum"};
   

    public void openCards()
    {
        
        Time.timeScale = 0;


        player = GameObject.Find("HQ - Player");
        stop = GameObject.Find("InputManager");
        inventory = GameObject.Find("InventorySystem");
        sg = player.GetComponent<ShootGun>();
        ps = player.GetComponent<PlayerStats>();
        p = stop.GetComponent<Pause>();
        es = inventory.GetComponent<InventorySystem>();

        cardMenu.SetActive(true);

        card1ID = Random.Range(0, deckSize);

        card2ID = Random.Range(0, deckSize);

        while (card2ID == card1ID)
        {
            card2ID = Random.Range(0, deckSize);
        }
        card1Txt.text = deck[card1ID];

       card2Txt.text = deck[card2ID];
    }
    
    public void cardChosen(Text name)
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
        cardMenu.SetActive(false);
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
}
