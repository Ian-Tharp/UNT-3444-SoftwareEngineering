using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        if( GameObject.FindGameObjectWithTag("SETTINGS") == null)
            SceneManager.LoadScene("Loader");
    }

}
