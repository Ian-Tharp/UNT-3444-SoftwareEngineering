using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class menuCursor : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] GameObject cursorPos;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
