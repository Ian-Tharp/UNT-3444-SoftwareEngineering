using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using System.Threading;

public class MousePointer : MonoBehaviour
{
    [SerializeField] BoxCollider2D box;

    bool touching;
    private Vector3 mousePos;
    private Vector3 targetPos;
    public float moveSpeed = 15f;
    SpriteRenderer msprite;
    [SerializeField]
    Sprite pointer;
    [SerializeField]
    Sprite reticle;

    Scene sceneC;

    //for mouse and keyboard, should have cursor follow mouse based on speed * distance/ some number


    void Start()
    {
        sceneC = SceneManager.GetActiveScene();
        Cursor.visible = false;
        touching = false;
        moveSpeed = 15f; //follow speed
        msprite = GetComponent<SpriteRenderer>();
        //code to set sprite to chosen reticle from settings
    }

    void Update()
    {
        //if mouse and keyboard input
        //follow mouse 
        mousePos = Mouse.current.position.ReadValue();
        float distance = transform.position.z + Camera.main.transform.position.z;
        targetPos = new Vector3(mousePos[0], mousePos[1], distance);
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
 
        Vector3 followXonly = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        transform.position = Vector3.Lerp (transform.position, followXonly, moveSpeed * Time.deltaTime);

        if(Time.timeScale <= 0 || sceneC.name == "Menu")
        {
            msprite.sprite = pointer;
            transform.position = new Vector3 (targetPos.x, targetPos.y, 0);
            msprite.color = new Color (1,1,1,1);
            box.enabled = false;
        }
        else{
            msprite.sprite = reticle;
            box.enabled = true;
            colorChange();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") // if touching enemy turn red
        {
            touching = true; 
            msprite.color = new Color (.7f,0,0,1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        touching = false;
        colorChange();
    }

    void colorChange()
    {
        if (!touching)
            msprite.color = new Color (0.7960785f, 0.5058824f, 0.02745098f, 1); // if not touching anything turn white
    }

}
