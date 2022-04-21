using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    bool toggle = false;
    
    // Start is called before the first frame update
    public void showScreen()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    public void hideScreen()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    public void toggleClick()
    {
        if(toggle)
        {
            toggle = false;
            hideScreen();
        } else{
            toggle = true;
            showScreen();
        }
    }
}
