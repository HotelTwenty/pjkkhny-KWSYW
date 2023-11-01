using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossOne : MonoBehaviour
{
    private Transform target;

    bool isFacingRight = true;
    bool isMovingRight;

    public GameObject S1, S2;
    Vector2 S1Pos, S2Pos;
    float nextSteam = 0.0f;
    float SteamRate = .005f;

    public GameObject RightBull, LeftBull, UpBull;
    Vector2 RightBullPos, LeftBullPos, UpBullPos;
    float nextBull = 0.0f;
    float bullRate = .2f;
    
    public GameObject ECirc, PhaseLeft, PhaseRight;
    Vector2 ECircPos, PhaseLeftPos, PhaseRightPos;
    float nextET = 0.0f;
    float ETRate = .5f;
    float nextPhase = 0.0f;
    float phaseRate = .1f;

    public GameObject smExp1, smExp2, smExp3, BExpWhite, BExpPurp, BExpOrange, BExpRed, BossDeathFlash;
    Vector2 smExp1Pos, smExp2Pos, smExp3Pos, BExpWhitePos, BExpPurpPos, BExpOrangePos, BExpRedPos;

    public static int MBOneHealth;
    public int curHealth;
    public static int pHealth;
    Vector3 SpawnPos1;

    public AudioSource MBHit, MBShot, MBMinExp, MBBigExp, MBTurn;

    // Start is called before the first frame update
    void Start()
    {
        isMovingRight = true;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        MBOneHealth = 300;
        SpawnPos1 = new Vector3(5235.6f, -84.7f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        pHealth = KellyChildMove.kHealth;
        if(pHealth <= 0)
        {
            transform.position = SpawnPos1;
            MBOneHealth = 300;
        }
        
        curHealth = MBOneHealth;
        if(MBOneHealth > 0)
        {
            if(Time.time > nextSteam)
            {
                nextSteam = Time.time + SteamRate;
                if(isMovingRight == true)
                {
                    purpSteam();
                    whiteSteam();
                }
                
                else
                {
                    negpurpSteam();
                    negwhiteSteam();
                }
            }

            if(isMovingRight == true)
            {
                MoveRight();
                if(isFacingRight == false)
                {
                    FlipPlayer();
                }

                if(target.position.y > transform.position.y -10f && target.position.y < transform.position.y + 10f)
                {
                    if(Time.time > nextBull)
                    {
                        nextBull = Time.time + bullRate;
                        fireRightUp();
                        fireRightDown();
                    }
                }

                if(MBOneHealth < 100 && MBOneHealth > 0)
                {
                    if(target.position.y > transform.position.y + 20f)
                    {
                        if(Time.time > nextBull)
                        {
                            nextBull = Time.time + bullRate;
                            fireUpRight();
                        }
                    }

                    if(MBOneHealth < 50 && Time.time > nextET)
                    {
                        nextET = Time.time + ETRate;
                        expTrig();
                    }

                    if(Time.time > nextPhase)
                    {
                        nextPhase = Time.time + phaseRate;
                        rPhase();
                    }
                }
            }

            else
            {
                MoveLeft();
                if(isFacingRight == true)
                {
                    FlipPlayer();
                }

                if(target.position.y > transform.position.y -10f && target.position.y < transform.position.y + 10f)
                {
                    if(Time.time > nextBull)
                    {
                        nextBull = Time.time + bullRate;
                        fireLeftUp();
                        fireLeftDown();
                    }
                }

                if(MBOneHealth < 100 && MBOneHealth > 0)
                {
                    if(target.position.y > transform.position.y + 20f)
                    {
                        if(Time.time > nextBull)
                        {
                            nextBull = Time.time + bullRate;
                            fireUpLeft();
                        }
                    }

                    if(MBOneHealth < 50 && Time.time > nextET)
                    {
                        nextET = Time.time + ETRate;
                        expTrig();
                    }

                    if(Time.time > nextPhase)
                    {
                        nextPhase = Time.time + phaseRate;
                        lPhase();
                    }
                }
            }
        }

        else
        {
            GetComponent<Animator> ().SetBool ("isHit", true);
            Invoke("whiteExp1", 1f);
            Invoke("whiteExp2", 1.5f);
            Invoke("whiteExp3", 2f);
            Invoke("whiteExp4", 2.5f);
            Invoke("whiteExp5", 3f);
            Invoke("whiteExp6", 3.5f);

            Invoke("orangeExp1", 4f);
            Invoke("orangeExp2", 4.25f);
            Invoke("orangeExp3", 4.5f);
            Invoke("orangeExp4", 4.75f);
            Invoke("orangeExp5", 5f);
            Invoke("orangeExp6", 5.25f);

            Invoke("purpExp1", 5.5f);
            Invoke("purpExp2", 5.75f);
            Invoke("purpExp3", 6f);
            Invoke("purpExp4", 6.25f);
            Invoke("purpxp5", 6.5f);
            Invoke("purpExp6", 6.75f);
            Invoke("BigExpOrange", 7f);
            Invoke("BigExpPurp", 7.9f);
            Invoke("BigExpWhite", 7.6f);
            Invoke("BigExpRed", 7.9f);
        }

    }


    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        MBTurn.Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            expTrig();
            if(isMovingRight == true)
            {
                isMovingRight = false;
                isFacingRight = false;
                FlipPlayer();
            }

            else
            {
                isMovingRight = true;
                isFacingRight = true;
                FlipPlayer();
            }
        }

        if(col.gameObject.tag == "PalyerLaser")
        {
            MBOneHealth -= 1;
            GetComponent<Animator> ().SetBool ("isHit", true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }

        if(col.gameObject.tag == "PlayerBull")
        {
            MBOneHealth -= 3;
            GetComponent<Animator> ().SetBool ("isHit", true);
            Invoke("offHit", .7f);
            MBHit.Play();
        }
    }

    void onHit()
    {
        GetComponent<Animator> ().SetBool ("isHit", true);
    }

    void offHit()
    {
        GetComponent<Animator> ().SetBool ("isHit", false);
    }

    //Movement===================================================================
    void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x - 40 * Time.deltaTime, transform.position.y); 
        isFacingRight = false;
    }

    void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + 40 * Time.deltaTime, transform.position.y); 
        isFacingRight = true;
    }

    //Steam====================================================================
    void purpSteam()
    {
        S1Pos = transform.position;
        S1Pos += new Vector2(-13f, 17f);
        Instantiate(S1, S1Pos, Quaternion.identity);
    }

    void whiteSteam()
    {
        S2Pos = transform.position;
        S2Pos += new Vector2(-11f, 17f);
        Instantiate(S2, S2Pos, Quaternion.identity);
    }

    void negpurpSteam()
    {
        S1Pos = transform.position;
        S1Pos += new Vector2(13f, 17f);
        Instantiate(S1, S1Pos, Quaternion.identity);
    }

    void negwhiteSteam()
    {
        S2Pos = transform.position;
        S2Pos += new Vector2(11f, 17f);
        Instantiate(S2, S2Pos, Quaternion.identity);
    }

    //shots ==============================
    void fireRightUp()
    {
        RightBullPos = transform.position;
        RightBullPos += new Vector2(18f, 11f);
        Instantiate(RightBull, RightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void fireRightDown()
    {
        RightBullPos = transform.position;
        RightBullPos += new Vector2(18f, 9f);
        Instantiate(RightBull, RightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void fireLeftUp()
    {
        LeftBullPos = transform.position;
        LeftBullPos += new Vector2(-18f, 11f);
        Instantiate(LeftBull, LeftBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void fireLeftDown()
    {
        LeftBullPos = transform.position;
        LeftBullPos += new Vector2(-18f, 9f);
        Instantiate(LeftBull, LeftBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void fireUpLeft()
    {
        UpBullPos = transform.position;
        UpBullPos += new Vector2(-6f, 14f);
        Instantiate(UpBull, UpBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void fireUpRight()
    {
        UpBullPos = transform.position;
        UpBullPos += new Vector2(6f, 14f);
        Instantiate(UpBull, UpBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void expTrig()
    {
        ECircPos = transform.position;
        ECircPos += new Vector2(0f, 0f);
        Instantiate(ECirc, ECircPos, Quaternion.identity);
    }

    void rPhase()
    {
        PhaseRightPos = transform.position;
        PhaseRightPos += new Vector2(0f, 0f);
        Instantiate(PhaseRight, PhaseRightPos, Quaternion.identity);
    }

    void lPhase()
    {
        PhaseLeftPos = transform.position;
        PhaseLeftPos += new Vector2(0f, 0f);
        Instantiate(PhaseLeft, PhaseLeftPos, Quaternion.identity);
    }

    //SmallExps======================================================
    //WhiteExps
    void whiteExp1()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(0f, 0f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp2()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-4f, -15f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp3()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-14f, -8f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp4()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-3f, -2f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp5()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(10f, 10f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp6()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-14f, 12f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    //OrangeExps
    void orangeExp1()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(0f, 0f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp2()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-4f, -15f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp3()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-14f, -8f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp4()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-3f, -2f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp5()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(10f, 10f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp6()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-14f, 12f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    //purpExps
    void purpExp1()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(0f, 0f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp2()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-4f, -15f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp3()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-14f, -8f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp4()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-3f, -2f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp5()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(10f, 10f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp6()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-14f, 12f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    //BigExps
    void BigExpWhite()
    {
        BExpWhitePos = transform.position;
        BExpWhitePos += new Vector2(0f, 0f);
        Instantiate(BExpWhite, BExpWhitePos, Quaternion.identity);
        BossDeathFlash.SetActive(true);
        MBBigExp.Play();
    }

    void BigExpPurp()
    {
        BExpPurpPos = transform.position;
        BExpPurpPos += new Vector2(0f, 0f);
        Instantiate(BExpPurp, BExpPurpPos, Quaternion.identity);
        MBBigExp.Play();
    }

    void BigExpOrange()
    {
        BExpOrangePos = transform.position;
        BExpOrangePos += new Vector2(0f, 0f);
        Instantiate(BExpOrange, BExpOrangePos, Quaternion.identity);
        MBBigExp.Play();
    }

    void BigExpRed()
    {
        BExpRedPos = transform.position;
        BExpRedPos += new Vector2(0f, 0f);
        Instantiate(BExpRed, BExpRedPos, Quaternion.identity);
        BossDeathFlash.SetActive(false);
        Destroy(gameObject);
        MBBigExp.Play();
    }
}
