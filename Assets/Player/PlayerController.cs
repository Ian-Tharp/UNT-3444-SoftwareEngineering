using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player2D;
    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 WorldPosition2D = new Vector2(WorldPosition.x, WorldPosition.y);
        Instantiate(Player2D, WorldPosition2D, Quaternion.identity); 
    }
}
