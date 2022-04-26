using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenuButtons : MonoBehaviour
{

    [SerializeField] GameObject howBut;
    [SerializeField] GameObject setBut;
    [SerializeField] GameObject credBut;
    bool toggle1 = false;
    bool toggle2 = false;
    bool toggle3 = false;

    public void toggleHow()
    {
        if(toggle1)
        {
            toggle1 = false;
            howBut.SetActive(false);
        } else{
            toggle1 = true;
            toggle2 = false;
            toggle3 = false;
            howBut.SetActive(true);
            setBut.SetActive(false);
            credBut.SetActive(false);
        }
    }
    public void toggleSet()
    {
        if(toggle2)
        {
            toggle2 = false;
            setBut.SetActive(false);
        } else{
            toggle1 = false;
            toggle2 = true;
            toggle3 = false;
            howBut.SetActive(false);
            setBut.SetActive(true);
            credBut.SetActive(false);
        }
    }
    public void toggleCred()
    {
        if(toggle3)
        {
            toggle3 = false;
            credBut.SetActive(false);
        } else{
            toggle1 = false;
            toggle2 = false;
            toggle3 = true;
            howBut.SetActive(false);
            setBut.SetActive(false);
            credBut.SetActive(true);
        }
    }
}
