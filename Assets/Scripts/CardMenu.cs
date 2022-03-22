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
    int deckSize = 2;


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

        card1Txt.text = "Health";

        card2Txt.text = "Damage";

    
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
}
