using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDestroy : MonoBehaviour
{
    private int timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= 1;
        if (timer <= 0)
            Destroy(gameObject);
    }
}
