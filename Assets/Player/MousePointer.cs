using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Threading;

public class MousePointer : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 targetPos;
    public float moveSpeed = 15f;
    SpriteRenderer sprite;

    //for mouse and keyboard, should have cursor follow mouse based on speed * distance/ some number


    void Start()
    {
        Cursor.visible = false;
        moveSpeed = 15f;
        sprite = GetComponent<SpriteRenderer>();
        //code to set sprite to chosen reticle from settings
    }

    void Update()
    {
        //if mouse and keyboard input
        mousePos = Mouse.current.position.ReadValue();
        float distance = transform.position.z + Camera.main.transform.position.z;
        targetPos = new Vector3(mousePos[0], mousePos[1], distance);
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
 
        Vector3 followXonly = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        transform.position = Vector3.Lerp (transform.position, followXonly, moveSpeed * Time.deltaTime);
        //transform.position = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0f, 0f, 10f));
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Enemy");
        if (col.gameObject.tag == "Enemy")
            sprite.color = new Color (.7f,0,0,1);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        sprite.color = new Color (1,1,1,1);
    }
}
