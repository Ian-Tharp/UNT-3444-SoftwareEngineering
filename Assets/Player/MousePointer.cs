using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;

public class MousePointer : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    private Vector2 Movement;
    public float moveSpeed = 15f;

    // code somewhere to include recoil everytime a bullet is shot
    // push cursor in a random direction based on weapon stats

    //for mouse and keyboard, should have cursor follow mouse based on speed * distance/ some number


    void Start()
    {
        Cursor.visible = false;
        //code to set sprite to chosen reticle from settings
    }

    void Update()
    {
        //if mouse and keyboard input
        mousePosition = Mouse.current.position.ReadValue();
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0f, 0f, 10f));
    }

}
