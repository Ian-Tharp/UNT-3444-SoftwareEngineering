using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Camera;

public class CameraChange : MonoBehaviour
{
    public Transform player;
    public Transform reticle;
    public Vector3 center;

    public Camera cam; 
    public float size;

    public int MAX, MIN;


    // Start is called before the first frame update
    void Start()
    {
        MAX = 20;
        MIN = 8;
    }

    // Update is called once per frame
    void Update()
    {
        center = ((reticle.position - player.position)/2.0f) + player.position;
        transform.position = new Vector3 (center.x, center.y, -10);

        //cam.orthographicSize = Vector2.Distance(reticle.position, player.position)/2 * size;
        if(cam.orthographicSize > MAX)
            cam.orthographicSize = MAX;
        if(cam.orthographicSize < MIN)
            cam.orthographicSize = MIN;
    }
}