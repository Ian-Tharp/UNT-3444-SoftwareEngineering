using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMESETTINGSscript: MonoBehaviour
{
    public float volume = 1.0f;
    public bool showHealth = false;
    public bool showBlood = true;
    public bool showHitnum = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu");
    }

    //void if

}
