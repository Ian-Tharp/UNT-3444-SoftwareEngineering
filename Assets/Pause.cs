using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject cardMenu;

    public PlayerInput playerControls;
    private InputAction pauseBut;
    private GameObject waveManager;
    private WaveManager wm;
    private CardMenu menu;
    
    public bool paused;
    public bool endWave;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        wm = waveManager.GetComponent<WaveManager>();
        menu = cardMenu.GetComponent<CardMenu>();
        endWave = wm.CanSpawnWave;
        if(paused || endWave)
        {
            paused = true;
            Time.timeScale = 0;
            if (endWave)
            {
                menu.openCards();
                endWave = false;    
                wm.StartWave();
            }
        } 
        else  
        {
            //Cursor.visible = false;
            Time.timeScale = 1;
        }
    
    
    }
    
    public void PauseGame()
    {
        if(paused )
        {
            paused = false;
        } else {
            paused = true;
        }
    }
}
