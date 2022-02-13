using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    // Update is called once per frame
    void Update() {

    }
    public void Fire() // called anytime player fires
    {
        Debug.Log("Fire!");

       // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
       // RaycastHit hit;

      // if (Physics.Raycast(ray, out hit))
       // {
            //the object has been detected and hit, do what you want
       //     Debug.Log("Hit someone");

       // }
    }
    

}

