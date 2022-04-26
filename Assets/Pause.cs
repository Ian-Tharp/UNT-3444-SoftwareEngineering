using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;
    [SerializeField] GameObject player;

    
    public PlayerInput playerControls;
    private InputAction pauseBut;
    private GameObject waveManager;
    private WaveManager wm;
    private CardMenu menu;
    private PlayerStats pStats;

    public bool paused;
    public bool endWave;
    //bool delay = false;

    float pauseTimer;

    private void Awake()
    {
        playerControls = new PlayerInput();
        
        waveManager = GameObject.Find("WaveManagerSystem");
    }

    private void OnEnable()
    {
        pauseBut = playerControls.Player.Pause;
        pauseBut.Enable();
        pauseBut.performed += context => PauseGame();

    }

    private void OnDisable()
    {
        pauseBut.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseTimer = 0f;
    }
/*
    IEnumerator menuDelay()
    {
        yield return new WaitForSeconds(2); 
        delay = false;
        
    }
*/
    // Update is called once per frame
    void Update()
    {
        
        wm = waveManager.GetComponent<WaveManager>();
        menu = cardMenu.GetComponent<CardMenu>();
        pStats = player.GetComponent<PlayerStats>();

        if (menu.wavePause || pStats.isDead)
        {
            pauseBut.Disable();
        } else {
            pauseBut.Enable();
        }


        endWave = wm.CanSpawnWave;
        if(paused || endWave || pStats.isDead)
        {
            if (endWave && !pStats.isDead)
            {
                menu.wavePause = true;
                //if (delay == false)
                pauseTimer -= .06f;
                if (pauseTimer <= 1)
                    if (Time.timeScale > 0.07f)
                        Time.timeScale -= .06f;

                if (pauseTimer <= .01f)
                {
                    paused = true;
                    Time.timeScale = 0;
                    //delay = true;
                    //StartCoroutine(menuDelay());
                
                    menu.openCards();
                    endWave = false;    
                    wm.StartWave();
                }
            }

            if(paused)
            {
                paused = true;
                Time.timeScale = 0;
            }
        } 
        else  
        {
            //Cursor.visible = false;
            Time.timeScale = 1;
            pauseTimer = 1.5f;
        }
    
    }
    
    public void PauseGame()
    {
        Debug.Log(menu.wavePause);
        if(paused && !pStats.isDead && !menu.wavePause)
        {
            paused = false;
        } else {
            paused = true;
        }
    }
}
