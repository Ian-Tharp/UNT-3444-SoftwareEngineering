using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Camera;

public class CameraChange : MonoBehaviour
{
    [SerializeField] PlayerStats ps;
    public Transform player; //hq pos
    public Transform reticle; //reticle pos
    public Vector3 center; //center between two object
    Vector3 mouseDist;

    private float goTime;

    public ShootGun sg;

    public Camera cam; //camera object
    public float size; //size modifier
    public float offset; //offset to fix camera sizing calc

    public float MAX, MIN, MAXDIST; //max and min of camera size

    public float shakeTime = 0f;
    public float shakeMag = 0.7f;
    public float damp = 1.0f;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        size = 50;
        offset = 14;
        MAX = 11;
        MIN = 8;
        MAXDIST = 7;
        goTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calcPosition();
        calcSize();

        if(ps.isDead)
        {
            if(goTime < 1.0f)
                goTime += .01f;
            transform.position = Vector3.Lerp (transform.position, new Vector3(0,0,-10), goTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8, goTime);
        }
        else
        {
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

    void calcPosition()
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
    }

    void calcSize()
    {
        cam.orthographicSize = (-size/Vector2.Distance(reticle.position, player.position)) + offset;
        if(cam.orthographicSize > MAX)
            cam.orthographicSize = MAX;
        if(cam.orthographicSize < MIN)
            cam.orthographicSize = MIN;
    }
}
