using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTo : MonoBehaviour
{
    bool toggle = false;
    
    // Start is called before the first frame update
    public void showHowScreen()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    public void hideHowScreen()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    public void toggleClick()
    {
        if(toggle)
        {
            toggle = false;
            hideHowScreen();
        } else{
            toggle = true;
            showHowScreen();
        }
    }
}
