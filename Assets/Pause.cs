using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;

public class Pause : MonoBehaviour
{
    public PlayerInput playerControls;
    private InputAction pauseBut;
    
    public bool paused;

    private void Awake()
    {
        playerControls = new PlayerInput();
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
        if(paused)
        {
            //Cursor.visible = true;
            Time.timeScale = 0;
        } else {
            //Cursor.visible = false;
            Time.timeScale = 1;
        }
    
    
    }
    
    public void PauseGame()
    {
        if(paused)
        {
            paused = false;
        } else {
            paused = true;
        }
    }
}
