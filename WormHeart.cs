using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHeart : MonoBehaviour
{
    public static int WormHealth;
    public static bool gameOn, DCFire, BackCanFire1, BackCanFire2, BackCanFire3, BackCanFire4, HeadMove1, HeadMove2, HeadMove3, HeadMove4;
    public int curHealth;
    public GameObject wExp1, wExp2, wExp3, wExp4, wExp5, wExp6, wExp7, wExp8, wExp9, mainExp1, mainExp2, mainExp3, mainExp4, mainExp5, blackExp;
    Vector2 wExp1Pos, wExp2Pos, wExp3Pos, wExp4Pos, wExp5Pos, wExp6Pos, wExp7Pos, wExp8Pos, wExp9Pos, mainExp1Pos, mainExp2Pos, mainExp3Pos, mainExp4Pos, mainExp5Pos, blackExpPos;
    public AudioSource minExpSFX, finExpSFX, heartHitSFX;
    float nextExp = 0.0f;
    float expRate = 2.0f;
  
    // Start is called before the first frame update
    void Start()
    {
        WormHealth = 300;
        gameOn = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        curHealth = WormHealth;
        if(WormHealth > 200 && WormHealth <= 300)
        {
            //DCFire = true;
            BackCanFire1 = false;
            BackCanFire2 = false;
            BackCanFire3 = false;
            BackCanFire4 = false;
            HeadMove1 = false;
            HeadMove2 = true;
            HeadMove3 = false;
            HeadMove4 = false;
            if(WormHealth > 200 && WormHealth <= 250)
            {
                DCFire = true;
            }
        }

        else if(WormHealth > 100 && WormHealth <= 200)
        {
            HeadMove1 = false;
            HeadMove2 = true;
            HeadMove3 = false;
            HeadMove4 = false;
            BackCanFire2 = false;
            BackCanFire3 = false;
            if(WormHealth > 100 && WormHealth <= 150)
            {
                DCFire = true;
                if(WormHealth > 100 && WormHealth <= 120)
                {
                    BackCanFire1 = true;
                    BackCanFire4 = true;
                }
            }
        }

        else if(WormHealth > 0 && WormHealth <= 100)
        {
            DCFire = true;
            HeadMove1 = false;
            HeadMove2 = false;
            HeadMove3 = false;
            HeadMove4 = true;
            if(WormHealth > 10 && WormHealth < 50)
            {
                BackCanFire1 = true;
                BackCanFire4 = true;
                BackCanFire2 = true;
                BackCanFire3 = true;
            }
        }

        else if(WormHealth <= 0)
        {
            BackCanFire1 = false;
            BackCanFire4 = false;
            BackCanFire2 = false;
            BackCanFire3 = false;
            DCFire = false;
            HeadMove1 = false;
            HeadMove2 = false;
            HeadMove3 = false;
            HeadMove4 = false;

            Destroy(gameObject, 16.4f);
            //exps1
            if(Time.time > nextExp)
            {
                nextExp = Time.time + expRate;
                Invoke("exp1", .5f);
                Invoke("exp2", 2.5f);
                Invoke("exp3", 3.5f);
                Invoke("exp4", 4.5f);
                Invoke("exp5", 5.5f);
                Invoke("exp6", 6.5f);
                Invoke("exp7", 7.5f);
                Invoke("exp8", 8.5f);
                Invoke("exp9", 9.5f);
                //exps2
                Invoke("exp1", 10.5f);
                Invoke("exp2", 11f);
                Invoke("exp3", 11.5f);
                Invoke("exp4", 12f);
                Invoke("exp5", 12.5f);
                Invoke("exp6", 13f);
                Invoke("exp7", 13.5f);
                Invoke("exp8", 14f);
                Invoke("exp9", 14.5f);
                //bigExps
                Invoke("mainExplosion1", 15f);
                Invoke("mainExplosion2", 15.1f);
                Invoke("mainExplosion3", 15.2f);
                Invoke("mainExplosion4", 15.3f);
                Invoke("mainExplosion5", 15.4f);
                Invoke("mainExplosion1", 15.5f);
                Invoke("mainExplosion2", 15.6f);
                Invoke("mainExplosion3", 15.7f);
                Invoke("mainExplosion4", 15.8f);
                Invoke("mainExplosion5", 15.9f);
                Invoke("mainExplosion5", 16f);
                Invoke("blackoutExp", 16.2f);
            }
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PalyerLaser") 
	    {
            WormHealth -= 1;
            GetComponent<Animator>().SetBool("isHit", true);
            Invoke("offHit", .5f);
            heartHitSFX.Play();
        }

        if (col.gameObject.tag == "PlayerBull") 
	    {
            WormHealth -= 5;
            GetComponent<Animator>().SetBool("isHit", true);
            Invoke("offHit", .5f);
            heartHitSFX.Play();
        }

        if (col.gameObject.tag == "PlayerMissile") 
	    {
            WormHealth -= 10;
            GetComponent<Animator>().SetBool("isHit", true);
            Invoke("offHit", .5f);
            heartHitSFX.Play();
        }

    }

    void offHit()
    {
        GetComponent<Animator>().SetBool("isHit", false);
    }
    //Explosions
    void exp1()
    {
        wExp1Pos = transform.position;
        wExp1Pos += new Vector2(0f, 0f);
        Instantiate(wExp1, wExp1Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp2()
    {
        wExp2Pos = transform.position;
        wExp2Pos += new Vector2(30f, 20f);
        Instantiate(wExp2, wExp2Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp3()
    {
        wExp3Pos = transform.position;
        wExp3Pos += new Vector2(0f, 20f);
        Instantiate(wExp3, wExp3Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp4()
    {
        wExp4Pos = transform.position;
        wExp4Pos += new Vector2(-10f, -10f);
        Instantiate(wExp4, wExp4Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp5()
    {
        wExp5Pos = transform.position;
        wExp5Pos += new Vector2(50f, -10f);
        Instantiate(wExp5, wExp5Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp6()
    {
        wExp6Pos = transform.position;
        wExp6Pos += new Vector2(60f, 10f);
        Instantiate(wExp6, wExp6Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp7()
    {
        wExp7Pos = transform.position;
        wExp7Pos += new Vector2(-10f, 10f);
        Instantiate(wExp7, wExp7Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp8()
    {
        wExp8Pos = transform.position;
        wExp8Pos += new Vector2(10f, 0f);
        Instantiate(wExp8, wExp8Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    void exp9()
    {
        wExp9Pos = transform.position;
        wExp9Pos += new Vector2(-10f, -10f);
        Instantiate(wExp9, wExp9Pos, Quaternion.identity);
        minExpSFX.Play();
    }

    //MainExps
    void mainExplosion1()
    {
        mainExp1Pos = transform.position;
        mainExp1Pos += new Vector2(0f, 0f);
        Instantiate(mainExp1, mainExp1Pos, Quaternion.identity);
        finExpSFX.Play();
    }

    void mainExplosion2()
    {
        mainExp2Pos = transform.position;
        mainExp2Pos += new Vector2(0f, 0f);
        Instantiate(mainExp2, mainExp2Pos, Quaternion.identity);
        finExpSFX.Play();
    }

    void mainExplosion3()
    {
        mainExp3Pos = transform.position;
        mainExp3Pos += new Vector2(0f, 0f);
        Instantiate(mainExp3, mainExp3Pos, Quaternion.identity);
        finExpSFX.Play();
    }

    void mainExplosion4()
    {
        mainExp4Pos = transform.position;
        mainExp4Pos += new Vector2(0f, 0f);
        Instantiate(mainExp4, mainExp4Pos, Quaternion.identity);
        finExpSFX.Play();
    }

    void mainExplosion5()
    {
        mainExp5Pos = transform.position;
        mainExp5Pos += new Vector2(0f, 0f);
        Instantiate(mainExp5, mainExp5Pos, Quaternion.identity);
        finExpSFX.Play();
    }

    void blackoutExp()
    {
        blackExpPos = transform.position;
        blackExpPos += new Vector2(0f, 0f);
        Instantiate(blackExp, blackExpPos, Quaternion.identity);
    }

}
