using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    public AudioSource source;
    private AudioClip dieSfx;
    private AudioClip hurt1;
    private AudioClip hurt2;
    private AudioClip hurt3;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        source = sources[0];
        dieSfx = sources[0].clip;
        hurt1 = sources[1].clip;
        hurt2 = sources[2].clip;
        hurt3 = sources[3].clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
