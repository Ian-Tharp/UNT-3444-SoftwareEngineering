using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotate : MonoBehaviour
{
    public float Speed = 9.0f;

    void Update() {
        transform.Rotate(new Vector3(0, 0, Speed) * Time.deltaTime);
    }
}
