using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossFour : MonoBehaviour
{
    int MB4Speed;
    bool isFacingRight;
    Vector3 RightSpawn, LeftSpawn, startPos;
    public static int MBFourHealth, PHealth;
    public int curHealth;

    public GameObject smExp1, smExp2, smExp3, BExpWhite, BExpPurp, BExpOrange, BExpRed, BossDeathFlash;
    Vector2 smExp1Pos, smExp2Pos, smExp3Pos, BExpWhitePos, BExpPurpPos, BExpOrangePos, BExpRedPos;

    //NormalBulls
    public GameObject downBull, upBull;
    Vector2 downBullPos, upBullPos;
    float nextBull = 0.0f;
    float bullRate = .2f;

    //bombBulls
    public GameObject bomdownBull, bomupBull;
    Vector2 bomdownBullPos, bomupBullPos;
    float nextBombBull = 0.0f;
    float bombBullRate = .5f;

    //specBulls
    public GameObject specdownBull, specupBull;
    Vector2 specdownBullPos, specupBullPos;
    float nextSpecBull = 0.0f;
    float specBullRate = .5f;

    public GameObject MB4JetLeft, MB4JetRight, MB4PhaserLeft, MB4PhaserRight;
    Vector2 MB4JetLeftPos, MB4JetRightPos, MB4PhaserLeftPos, MB4PhaserRightPos;
    float nextPhase = 0.0f;
    float phaseRate = .1f;

    public AudioSource MBHit, MBShot, MBMinExp, MBBigExp;

    // Start is called before the first frame update
    void Start()
    {
        MB4Speed = 40;
        isFacingRight = false;
        RightSpawn = new Vector3(5563f, 313.2075f, 0f);
        LeftSpawn = new Vector3(5255.2f, 280f, 0f);
        startPos = new Vector3(5416.1f, 313.207f, 0f);
        MBFourHealth = 150;
    }

    // Update is called once per frame
    void Update()
    {
        PHealth = KellyChildMove.kHealth;
        curHealth = MBFourHealth;
        if(PHealth <= 0)
        {
            transform.position = startPos;
            MBFourHealth = 150;
            if(isFacingRight == true)
            {
                FlipPlayer();
            }
        }

        if(MBFourHealth > 0)
        {
            if(Time.time > nextPhase)
            {
                nextPhase = Time.time + phaseRate;
                if(MBFourHealth > 25 && MBFourHealth <= 150)
                {
                    if(isFacingRight == true)
                    {
                        MBFourJetRight();
                    }

                    else
                    {
                        MBFourJetLeft();
                    }
                }

                else if(MBFourHealth > 0 && MBFourHealth <= 25)
                {
                    if(isFacingRight == true)
                    {
                        MB4PhaseRight();
                    }

                    else
                    {
                        MB4PhaseLeft();
                    }                  
                }
            }
            if(isFacingRight == false)
            {
                moveLeft();
            }

            else
            {
                moveRight();
            }

            if(MBFourHealth > 100)
            {
                if(Time.time > nextBull)
                {
                    nextBull = Time.time + bullRate;
                    if(isFacingRight == true)
                    {
                        stdFireRightDown();
                        stdFireRightUp();
                    }

                    else
                    {
                        stdFireLeftDown();
                        stdFireLeftUp();
                    }
                }
            }

            else if(MBFourHealth <= 100 && MBFourHealth > 50)
            {
                if(Time.time > nextSpecBull)
                {
                    nextSpecBull = Time.time + specBullRate;
                    if(isFacingRight == true)
                    {
                        specFireRightDown();
                        specFireRightUp();
                    }

                    else
                    {
                        specFireLeftDown();
                        specFireLeftUp();
                    }
                }
            }

            else if(MBFourHealth <= 50 && MBFourHealth > 0)
            {
                if(Time.time > nextBombBull)
                {
                    nextBombBull = Time.time + bombBullRate;
                    if(isFacingRight == true)
                    {
                        bombFireRightDown();
                        bombFireRightUp();
                    }

                    else
                    {
                        bombFireLeftDown();
                        bombFireLeftUp();
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
            if(isFacingRight == true)
            {
                FlipPlayer();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "PalyerLaser")
        {
            MBFourHealth -= 1;
            GetComponent<Animator> ().SetBool ("isHit", true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }

        if(col.gameObject.tag == "PlayerBull")
        {
            MBFourHealth -= 3;
            GetComponent<Animator> ().SetBool ("isHit", true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "MB4TurnRight":
            {
                if(isFacingRight == false)
                {
                    FlipPlayer();
                    transform.position = LeftSpawn;
                }
            }
            break;

            case "MB4TurnLeft":
            {
                if(isFacingRight == true)
                {
                    FlipPlayer();
                    transform.position = RightSpawn;
                }
            }
            break;
        }
    }

    void offHit()
    {
        GetComponent<Animator> ().SetBool ("isHit", false);
    }

    //NormBulls
    void stdFireLeftUp()
    {
        upBullPos = transform.position;
        upBullPos += new Vector2(13.25f, 5.4f);
        Instantiate(upBull, upBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireLeftDown()
    {
        downBullPos = transform.position;
        downBullPos += new Vector2(13.25f, -5.4f);
        Instantiate(downBull, downBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireRightUp()
    {
        upBullPos = transform.position;
        upBullPos += new Vector2(-13.25f, 5.4f);
        Instantiate(upBull, upBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireRightDown()
    {
        downBullPos = transform.position;
        downBullPos += new Vector2(-13.25f, -5.4f);
        Instantiate(downBull, downBullPos, Quaternion.identity);
        MBShot.Play();
    }

    //bombBulls
    void bombFireLeftUp()
    {
        bomupBullPos = transform.position;
        bomupBullPos += new Vector2(13.25f, 5.4f);
        Instantiate(bomupBull, bomupBullPos, Quaternion.identity);
    }

    void bombFireLeftDown()
    {
        bomdownBullPos = transform.position;
        bomdownBullPos += new Vector2(13.25f, -5.4f);
        Instantiate(bomdownBull, bomdownBullPos, Quaternion.identity);
    }

    void bombFireRightUp()
    {
        bomupBullPos = transform.position;
        bomupBullPos += new Vector2(-13.25f, 5.4f);
        Instantiate(bomupBull, bomupBullPos, Quaternion.identity);
    }

    void bombFireRightDown()
    {
        bomdownBullPos = transform.position;
        bomdownBullPos += new Vector2(-13.25f, -5.4f);
        Instantiate(bomdownBull, bomdownBullPos, Quaternion.identity);
    }

    //specBulls
    void specFireLeftUp()
    {
        specupBullPos = transform.position;
        specupBullPos += new Vector2(13.25f, 5.4f);
        Instantiate(specupBull, specupBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireLeftDown()
    {
        specdownBullPos = transform.position;
        specdownBullPos += new Vector2(13.25f, -5.4f);
        Instantiate(specdownBull, specdownBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireRightUp()
    {
        specupBullPos = transform.position;
        specupBullPos += new Vector2(-13.25f, 5.4f);
        Instantiate(specupBull, specupBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireRightDown()
    {
        specdownBullPos = transform.position;
        specdownBullPos += new Vector2(-13.25f, -5.4f);
        Instantiate(specdownBull, specdownBullPos, Quaternion.identity);
        MBShot.Play();
    }
//========================================================================================================================================================

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void moveLeft()
    {
        transform.position = new Vector2(transform.position.x - MB4Speed * Time.deltaTime, transform.position.y);
    }

    void moveRight()
    {
        transform.position = new Vector2(transform.position.x + MB4Speed * Time.deltaTime, transform.position.y);
    }

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
        smExp1Pos += new Vector2(-10f, -7.5f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp3()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(15f, 2.2f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp4()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(7.9f, -4.8f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp5()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-3.6f, 7f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp6()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-17.2f, 0f);
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
        smExp2Pos += new Vector2(-10f, -7.5f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp3()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(15f, 2.2f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp4()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(7.9f, -4.8f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp5()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-3.6f, 7f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp6()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-17.2f, 0f);
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
        smExp3Pos += new Vector2(-10f, -7.5f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp3()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(15f, -2.2f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp4()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(7.9f, -4.8f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp5()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-3.6f, 7f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp6()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-17.2f, 0f);
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
        MBBigExp.Play();
        BossDeathFlash.SetActive(false);
        Destroy(gameObject);
    }

    //===========================Phase && Jet
    void MB4PhaseLeft()
    {
        MB4PhaserLeftPos = transform.position;
        MB4PhaserLeftPos += new Vector2(0f, 0f);
        Instantiate(MB4PhaserLeft, MB4PhaserLeftPos, Quaternion.identity);
    }

    void MB4PhaseRight()
    {
        MB4PhaserRightPos = transform.position;
        MB4PhaserRightPos += new Vector2(0f, 0f);
        Instantiate(MB4PhaserRight, MB4PhaserRightPos, Quaternion.identity);
    }

    void MBFourJetLeft()
    {
        MB4JetLeftPos = transform.position;
        MB4JetLeftPos += new Vector2(14.8f, 0f);
        Instantiate(MB4JetLeft, MB4JetLeftPos, Quaternion.identity);
    }

    void MBFourJetRight()
    {
        MB4JetRightPos = transform.position;
        MB4JetRightPos += new Vector2(-14.8f, 0f);
        Instantiate(MB4JetRight, MB4JetRightPos, Quaternion.identity);
    }
}
