using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHead : MonoBehaviour
{
    public GameObject DC, facePhaser ,constRing, thisFlash;
    Vector2 DCPos, facePhaserPos;
    float nextDC = 0.0f;
    float DCRate = 1f;
    public static bool headAttack1, headAttack2, headAttack3, headAttack4;
    public static int bHealth;
    public AudioSource wHeadMove;

    // Start is called before the first frame update
    void Start()
    {
        constRing.SetActive(false);
        headAttack1 = false;
        headAttack2 = false;
        headAttack3 = false;
        headAttack4 = false;
        thisFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        headAttack1 = WormHeart.HeadMove1;
        headAttack2 = WormHeart.HeadMove2;
        headAttack3 = WormHeart.HeadMove3;
        headAttack4 = WormHeart.HeadMove4;
        bHealth = WormHeart.WormHealth;

        if (headAttack1 == true)
        {
            if(Time.time > nextDC)
            {
                nextDC = Time.time + DCRate;
                Invoke("HeadMoveUpNocircs", .5f);
                Invoke("stopHeadMoveUpNocircs", 1f);
            }
            headAttack2 = false;
            headAttack3 = false;
            headAttack4 = false;
        }

        if (headAttack2 == true)
        {
            if(Time.time > nextDC)
            {
                nextDC = Time.time + DCRate;
                Invoke("HeadMoveUpNocircsConst", .5f);
                thePhase();
                //Invoke("stopHeadMoveUpNocircsConst", 1f);
            }
            headAttack1 = false;
            headAttack3 = false;
            headAttack4 = false;
        }

        if (headAttack3 == true)
        {
            if(Time.time > nextDC)
            {
                nextDC = Time.time + DCRate;
                Invoke("MoveUp", .5f);
                Invoke("stopMoveUp", 1f);
                constRing.SetActive(true);
                DeathCirc();
                thePhase();
            }
            headAttack2 = false;
            headAttack1 = false;
            headAttack4 = false;
        }

        if (headAttack4 == true)
        {
            if(Time.time > nextDC)
            {
                nextDC = Time.time + DCRate;
                Invoke("constMoveUp", .5f);
                DeathCirc();
                thePhase();
                //Invoke("stopconstMoveUp", 1f);
            }
            headAttack2 = false;
            headAttack3 = false;
            headAttack1 = false;
        }

        if(bHealth <= 0)
        {
            thisFlash.SetActive(true);
            Destroy(gameObject, 16f);
            GetComponent<Animator>().SetBool("constMoveUp", false);
            GetComponent<Animator>().SetBool("MoveUp", false);
        }
    }

    void DeathCirc()
    {
        DCPos = transform.position;
        DCPos += new Vector2(0f, 0f);
        Instantiate(DC, DCPos, Quaternion.identity);
    }

    void thePhase()
    {
        facePhaserPos = transform.position;
        facePhaserPos += new Vector2(0f, 0f);
        Instantiate(facePhaser, facePhaserPos, Quaternion.identity);
        wHeadMove.Play();
    }

    public void constMoveUp()
    {
        GetComponent<Animator>().SetBool("constMoveUp", true);
        if (Time.time > nextDC)
        {
            nextDC = Time.time + DCRate;
            DeathCirc();
            thePhase();
        }
    }

    public void MoveUp()
    {
        GetComponent<Animator>().SetBool("MoveUp", true);
        /*if (Time.time > nextDC)
        {
            nextDC = Time.time + DCRate;
            DeathCirc();
            thePhase();
            constRing.SetActive(true);
        }*/
    }

    public void stopconstMoveUp()
    {
        GetComponent<Animator>().SetBool("constMoveUp", false);
    }

    public void stopMoveUp()
    {
        GetComponent<Animator>().SetBool("MoveUp", false);
        constRing.SetActive(false);
    }

    public void HeadMoveUpNocircs()
    {
        GetComponent<Animator>().SetBool("MoveUp", true);
        constRing.SetActive(false);
    }

    public void HeadMoveUpNocircsConst()
    {
        GetComponent<Animator>().SetBool("constMoveUp", true);
        constRing.SetActive(false);
    }

    public void stopHeadMoveUpNocircs()
    {
        GetComponent<Animator>().SetBool("MoveUp", false);
        constRing.SetActive(false);
    }

    public void stopHeadMoveUpNocircsConst()
    {
        GetComponent<Animator>().SetBool("constMoveUp", false);
        constRing.SetActive(false);
    }
}
