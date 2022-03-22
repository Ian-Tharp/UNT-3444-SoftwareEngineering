using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Camera;

public class CameraChange : MonoBehaviour
{
    public Transform player; //hq pos
    public Transform reticle; //reticle pos
    public Vector3 center; //center between two object
    Vector3 mouseDist;

    public ShootGun sg;

    public Camera cam; //camera object
    public float size; //size modifier
    public float offset; //offset to fix camera sizing calc

    public int MAX, MIN, MAXDIST; //max and min of camera size

    public float shakeTime = 0f;
    public float shakeMag = 0.7f;
    public float damp = 1.0f;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        size = 25;
        offset = 12;
        MAX = 20;
        MIN = 10;
        MAXDIST = 7;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseDist = ((reticle.position - player.position)/2.0f);
        if (mouseDist.x > MAXDIST)
            mouseDist.x = MAXDIST;
        if (mouseDist.x < -MAXDIST)
            mouseDist.x = -MAXDIST;
        if (mouseDist.y > MAXDIST)
            mouseDist.y = MAXDIST;
        if (mouseDist.y < -MAXDIST)
            mouseDist.y = -MAXDIST;
        center = (mouseDist + player.position); //center calc
        Debug.Log(mouseDist);
        

        cam.orthographicSize = (-size/Vector2.Distance(reticle.position, player.position)) + offset;
        //Debug.Log("size of cam: " + cam.orthographicSize);
        //Debug.Log("distance: " + Vector2.Distance(reticle.position, player.position));
        
        if(cam.orthographicSize > MAX)
            cam.orthographicSize = MAX;
        if(cam.orthographicSize < MIN)
            cam.orthographicSize = MIN;

        initPos = center;
        if (shakeTime > 0)
        {
            transform.position = new Vector3 (center.x, center.y, -10) + Random.insideUnitSphere * shakeMag; //position camera in the center pos 

            shakeTime -= Time.deltaTime * damp;
        } else {
            shakeTime = 0f;
            transform.position = new Vector3 (center.x, center.y, -10); //position camera in the center pos
        }

        if(sg.firing == true && sg.recoilBuildup > 0)
        {
            shakeTime = 0.17f;
            shakeMag = .03f * sg.recoilBuildup + .08f;
        }
    }
}
