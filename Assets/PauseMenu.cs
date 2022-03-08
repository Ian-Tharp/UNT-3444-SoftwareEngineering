using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1,1,1,0);

        transform.position = new Vector3(0,0,30);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            sprite.color = new Color(0,0,0,.5f);
        } else {
            sprite.color = new Color(0,0,0,0);
        }
    }
}
