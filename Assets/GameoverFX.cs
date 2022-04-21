using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameoverFX : MonoBehaviour
{
    ParticleSystem[] explosionArr;
    
    [SerializeField]
    ParticleSystem explosion;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameoverButtons goUI;

    public float esize;
    private float waitTime;
    PlayerStats ps;

    bool activated;
    public bool ending;



    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        ending = false;
        ps = player.GetComponent<PlayerStats>();
        explosionArr = new ParticleSystem[10];
        //StartCoroutine(gameoverEffect());
    }

    void Update()
    {
        if (ps.isDead && !activated)
        {
            activated = true;
            StartCoroutine(gameoverEffect());
        }
        if (ending)
        {
            if (Time.timeScale > 0.007f)
            {
                Time.timeScale -= .006f;
            } else
            {
                Time.timeScale = 0;
            }
        }

    }


    //scripted explosion sequence
    IEnumerator gameoverEffect()
    {
        for (int i = 0; i < 10; i++)
        {
            explosionArr[i] = Instantiate(explosion);

            if (i == 0)
            {
                explosionArr[i].transform.position = new Vector3(Random.Range(-3.7f, 3.7f), Random.Range(-3.7f, 3.7f), 0);
                esize = Random.Range(1.0f, 2.5f);
                waitTime = Random.Range(.6f, 1.0f);
            }
            if (i > 0 && i < 6)
            {
                explosionArr[i].transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
                esize = Random.Range(1.5f, 3.0f);
                waitTime = Random.Range(.1f, .3f);
            }
            if (i >= 6)
            {
                explosionArr[i].transform.position = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
                esize = Random.Range(3.5f, 5.0f);
                waitTime = Random.Range(.5f, .7f);
            }
              
            explosionArr[i].transform.localScale = new Vector3(esize, esize, esize);
            yield return new WaitForSeconds(waitTime);
        }
        ps.explosions +=10;
        yield return new WaitForSeconds(0.3f);
        ending = true;
        goUI.GameOver();
        //for (int i = 0; i < 100; i++)
           // Time.timeScale -= .01f;
        //Time.timeScale = 0;
    }
}
