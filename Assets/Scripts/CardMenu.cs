using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;

    public void openCards()
    {
        cardMenu.SetActive(true);
        Time.timeScale = 0;
    
    }
    
    public void cardChosen()
    {
        cardMenu.SetActive(false);
    }
   
}
