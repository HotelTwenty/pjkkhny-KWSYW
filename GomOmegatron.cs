using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GomOmegatron : MonoBehaviour
{
    public GameObject GOHit;
    public static int GommiHealth, PHealth;
    public int curHealth;
    public bool isHeartOn, isHeartOff;

    public GameObject GomGun1, GomGun2, GomBomb, OmniDirTur, FWODDown, FWODUp, FWODLeft, FWODRight, GomFlash, GomHeartShield;
    public GameObject  GDC1, GDC2, GDC3, GDC4, FinDC;
    Vector2 GDC1Pos, GDC2Pos, GDC3Pos, GDC4Pos, FinDCPos;
    public AudioSource GHit, GMinExp, GBigExp;

    // Start is called before the first frame update
    void Start()
    {
        GommiHealth = 150;
        GomHeartShield.SetActive(true);
        GOHit.SetActive(false);
        GomGun1.SetActive(false);
        GomGun2.SetActive(false);
        GomBomb.SetActive(false);
        OmniDirTur.SetActive(false);
        FWODDown.SetActive(false);
        FWODUp.SetActive(false);
        FWODLeft.SetActive(false);
        FWODRight.SetActive(false);
        isHeartOn = true;
        isHeartOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        PHealth = KellyChildMove.kHealth;
        if(PHealth <= 0)
        {
            GommiHealth = 150;
            GomHeartShield.SetActive(true);
            GOHit.SetActive(false);
            GomGun1.SetActive(false);
            GomGun2.SetActive(false);
            GomBomb.SetActive(false);
            OmniDirTur.SetActive(false);
            FWODDown.SetActive(false);
            FWODUp.SetActive(false);
            FWODLeft.SetActive(false);
            FWODRight.SetActive(false);
            isHeartOn = true;
            isHeartOff = false;
        }
        
        curHealth = GommiHealth;
        if(GommiHealth > 0)
        {
            if(isHeartOn == true)
            {
                isHeartOff = false;
                onSheild();
            }

            else if(isHeartOff == true)
            {
                isHeartOn = false;
                offSheild();
            }

            if(GommiHealth <= 150 && GommiHealth > 125)
            {
                GomGun1.SetActive(true);
                GomGun2.SetActive(true);
            }

            else if(GommiHealth <= 125 && GommiHealth > 100)
            {
                GomGun1.SetActive(false);
                GomGun2.SetActive(false);
                OmniDirTur.SetActive(true);
            }

            else if(GommiHealth <= 100 && GommiHealth > 75)
            {
                GomBomb.SetActive(true);
                OmniDirTur.SetActive(false);
            }

            else if(GommiHealth <= 75 && GommiHealth > 50)
            {
                GomBomb.SetActive(false);
                OmniDirTur.SetActive(false);
                FWODDown.SetActive(true);
            }

            else if(GommiHealth <= 50 && GommiHealth > 25)
            {
                FWODDown.SetActive(true);
                OmniDirTur.SetActive(true);
            }

            else if(GommiHealth <= 25 && GommiHealth > 0)
            {
                OmniDirTur.SetActive(false);
                FWODDown.SetActive(true);
                actLeftWall();
            }
        }

        else
        {
            GOHit.SetActive(true);
            GomGun1.SetActive(false);
            GomGun2.SetActive(false);
            GomBomb.SetActive(false);
            OmniDirTur.SetActive(false);
            FWODDown.SetActive(false);
            FWODUp.SetActive(false);
            FWODLeft.SetActive(false);
            FWODRight.SetActive(false);

            Invoke("offGDC1", 2f);
            Invoke("offGDC2", 3f);
            Invoke("offGDC3", 4f);
            Invoke("offGDC4", 5f);
            Invoke("offFinDC", 6f);
            Invoke("toNxtLvl", 8f);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "PalyerLaser") 
	    {
            GommiHealth -= 1;
            GOHit.SetActive(true);
            GHit.Play();
            Invoke("offHit", .5f);
        }

        if (col.gameObject.tag == "PlayerBull") 
	    {
            GommiHealth -= 3;
            GOHit.SetActive(true);
            GHit.Play();
            Invoke("offHit", .5f);
        }
    }

    void offHit()
    {
        GOHit.SetActive(false);
    }

    void actHeartSheild()
    {
        GomHeartShield.SetActive(true);
    }

    void deactHeartShield()
    {
        GomHeartShield.SetActive(false);
    }

    //Guns===============================
    void actGun1()
    {
        GomGun1.SetActive(true);
    }

    void deactGun1()
    {
        GomGun1.SetActive(false);
    }

    void actGun2()
    {
        GomGun2.SetActive(true);
    }

    void deactGun2()
    {
        GomGun2.SetActive(false);
    }

    void actBomb()
    {
        GomBomb.SetActive(true);
    }

    void deactBomb()
    {
        GomBomb.SetActive(false);
    }

    //Wall Spawns=============================
    void actUpWall()
    {
        FWODUp.SetActive(true);
        Invoke("actDownWall", 4f);
        Invoke("deactDownWall", 4.1f);
    }

    void deactUpWall()
    {
        FWODUp.SetActive(false);
    }

    void actDownWall()
    {
        FWODDown.SetActive(true);
        Invoke("actUpWall", 4f);
        Invoke("deactUpWall", 4.1f);
    }

    void deactDownWall()
    {
        FWODDown.SetActive(false);
    }

    void actLeftWall()
    {
        FWODLeft.SetActive(true);
        Invoke("actRightWall", 4f);
        Invoke("deactRightWall", 4.1f);
    }

    void deactLeftWall()
    {
        FWODLeft.SetActive(false);
    }

    void actRightWall()
    {
        FWODRight.SetActive(true);
        Invoke("actLeftWall", 4f);
        Invoke("deactLeftWall", 4.1f);
    }

    void deactRightWall()
    {
        FWODRight.SetActive(false);
    }

    void actOT()
    {
        OmniDirTur.SetActive(true);
    }

    void deactOT()
    {
        OmniDirTur.SetActive(false);
    }

    void actGomFlash()
    {
        GomFlash.SetActive(true);
    }

    void deactGomFlash()
    {
        GomFlash.SetActive(false);
    }

    void onSheild()
    {
        GomHeartShield.SetActive(true);
        Invoke("offHeart", 10f);
    }

    void offSheild()
    {
        GomHeartShield.SetActive(false);
        Invoke("onHeart", 4f);
    }

    void onHeart()
    {
        isHeartOn = true;
        isHeartOff = false;
    }


    void offHeart()
    {
        isHeartOff = true;
        isHeartOn = false;
    }

    void offGDC1()
    {
        GDC1Pos = transform.position;
        GDC1Pos += new Vector2(0f, 0f);
        Instantiate(GDC1, GDC1Pos, Quaternion.identity);
        GMinExp.Play();
    }

    void offGDC2()
    {
        GDC2Pos = transform.position;
        GDC2Pos += new Vector2(0f, 0f);
        Instantiate(GDC2, GDC2Pos, Quaternion.identity);
        GMinExp.Play();
    }

    void offGDC3()
    {
        GDC3Pos = transform.position;
        GDC3Pos += new Vector2(0f, 0f);
        Instantiate(GDC3, GDC3Pos, Quaternion.identity);
        GMinExp.Play();
    }

    void offGDC4()
    {
        GDC4Pos = transform.position;
        GDC4Pos += new Vector2(0f, 0f);
        Instantiate(GDC4, GDC4Pos, Quaternion.identity);
        GMinExp.Play();
    }

    void offFinDC()
    {
        FinDCPos = transform.position;
        FinDCPos += new Vector2(0f, 0f);
        Instantiate(FinDC, FinDCPos, Quaternion.identity);
        GBigExp.Play();
    }

    void toNxtLvl()
    {
        SceneManager.LoadScene("WorldEnderFail");
    }
}
