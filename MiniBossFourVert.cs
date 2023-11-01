using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossFourVert : MonoBehaviour
{
    int MB4Speed;
    bool isFacingDown;
    Vector3 downSpawn, upSpawn, startPos;
    public static int MBFourVertHealth, PHealth;
    public int curHealth;

    public GameObject smExp1, smExp2, smExp3, BExpWhite, BExpPurp, BExpOrange, BExpRed, BossDeathFlash;
    Vector2 smExp1Pos, smExp2Pos, smExp3Pos, BExpWhitePos, BExpPurpPos, BExpOrangePos, BExpRedPos;

    //NormalBulls
    public GameObject leftBull, rightBull;
    Vector2 leftBullPos, rightBullPos;
    float nextBull = 0.0f;
    float bullRate = .2f;

    //bombBulls
    public GameObject bomleftBull, bomrightBull;
    Vector2 bomleftBullPos, bomrightBullPos;
    float nextBombBull = 0.0f;
    float bombBullRate = .5f;

    //specBulls
    public GameObject specleftBull, specrightBull;
    Vector2 specleftBullPos, specrightBullPos;
    float nextSpecBull = 0.0f;
    float specBullRate = .5f;

    public GameObject MB4JetUp, MB4JetDown, MB4PhaserUp, MB4PhaserDown;
    Vector2 MB4JetUpPos, MB4JetDownPos, MB4PhaserUpPos, MB4PhaserDownPos;
    float nextPhase = 0.0f;
    float phaseRate = .1f;

    public AudioSource MBHit, MBShot, MBMinExp, MBBigExp;

    // Start is called before the first frame update
    void Start()
    {
        MB4Speed = 40;
        isFacingDown = false;
        downSpawn = new Vector3(5393.9f, 373f, 0f);
        upSpawn = new Vector3(5455.9f, 207.8f, 0f);
        startPos = new Vector3(5455.9f, 293.7f, 0f);
        MBFourVertHealth = 150;
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = MBFourVertHealth;
        PHealth = KellyChildMove.kHealth;
        if(PHealth <= 0)
        {
            transform.position = startPos;
            MBFourVertHealth = 150;
            if(isFacingDown == true)
            {
                FlipPlayer();
            }
        }
        
        if(MBFourVertHealth > 0)
        {
            if(Time.time > nextPhase)
            {
                nextPhase = Time.time + phaseRate;
                if(MBFourVertHealth > 25 && MBFourVertHealth <= 150)
                {
                    if(isFacingDown == true)
                    {
                        MBFourJetDown();
                    }

                    else
                    {
                        MBFourJetUp();
                    }
                }

                else if(MBFourVertHealth > 0 && MBFourVertHealth <= 25)
                {
                    if(isFacingDown == true)
                    {
                        MB4PhaseDown();
                    }

                    else
                    {
                        MB4PhaseUp();
                    }                  
                }
            }

            if(isFacingDown == false)
            {
                moveUp();
            }

            else
            {
                moveDown();
            }

            if(MBFourVertHealth > 100)
            {
                if(Time.time > nextBull)
                {
                    nextBull = Time.time + bullRate;
                    if(isFacingDown == true)
                    {
                        stdFireDownLeft();
                        stdFireDownRight();
                    }

                    else
                    {
                        stdFireUpLeft();
                        stdFireUpRight();
                    }
                }
            }

            else if(MBFourVertHealth <= 100 && MBFourVertHealth > 50)
            {
                if(Time.time > nextSpecBull)
                {
                    nextSpecBull = Time.time + specBullRate;
                    if(isFacingDown == true)
                    {
                        specFireDownLeft();
                        specFireDownRight();
                    }

                    else
                    {
                        specFireUpLeft();
                        specFireUpRight();
                    }
                }
            }

            else if(MBFourVertHealth <= 50 && MBFourVertHealth > 0)
            {
                if(Time.time > nextBombBull)
                {
                    nextBombBull = Time.time + bombBullRate;
                    if(isFacingDown == true)
                    {
                        bombFireDownLeft();
                        bombFireDownRight();
                    }

                    else
                    {
                        bombFireUpLeft();
                        bombFireUpRight();
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
            if(isFacingDown == true)
            {
                FlipPlayer();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "PalyerLaser")
        {
            MBFourVertHealth -= 1;
            GetComponent<Animator> ().SetBool ("isHit", true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }

        if(col.gameObject.tag == "PlayerBull")
        {
            MBFourVertHealth -= 3;
            GetComponent<Animator> ().SetBool ("isHit", true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "MB4TurnDown":
            {
                if(isFacingDown == false)
                {
                    FlipPlayer();
                    transform.position = downSpawn;
                }
            }
            break;

            case "MB$TurnUp":
            {
                if(isFacingDown == true)
                {
                    FlipPlayer();
                    transform.position = upSpawn;
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
    void stdFireUpRight()
    {
        rightBullPos = transform.position;
        rightBullPos += new Vector2(6.5f, -13.25f);
        Instantiate(rightBull, rightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireUpLeft()
    {
        leftBullPos = transform.position;
        leftBullPos += new Vector2(-6.5f, -13.25f);
        Instantiate(leftBull, leftBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireDownRight()
    {
        rightBullPos = transform.position;
        rightBullPos += new Vector2(6.5f, 13.5f);
        Instantiate(rightBull, rightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void stdFireDownLeft()
    {
        leftBullPos = transform.position;
        leftBullPos += new Vector2(-6.5f, 13.5f);
        Instantiate(leftBull, leftBullPos, Quaternion.identity);
        MBShot.Play();
    }

    //bombBulls
    void bombFireUpRight()
    {
        bomrightBullPos = transform.position;
        bomrightBullPos += new Vector2(6.5f, -13.5f);
        Instantiate(bomrightBull, bomrightBullPos, Quaternion.identity);
    }

    void bombFireUpLeft()
    {
        bomleftBullPos = transform.position;
        bomleftBullPos += new Vector2(-6.5f, -13.5f);
        Instantiate(bomleftBull, bomleftBullPos, Quaternion.identity);
    }

    void bombFireDownRight()
    {
        bomrightBullPos = transform.position;
        bomrightBullPos += new Vector2(6.5f, 13.5f);
        Instantiate(bomrightBull, bomrightBullPos, Quaternion.identity);
    }

    void bombFireDownLeft()
    {
        bomleftBullPos = transform.position;
        bomleftBullPos += new Vector2(-6.5f, 13.5f);
        Instantiate(bomleftBull, bomleftBullPos, Quaternion.identity);
    }

    //specBulls
    void specFireUpRight()
    {
        specrightBullPos = transform.position;
        specrightBullPos += new Vector2(6.5f, -13.25f);
        Instantiate(specrightBull, specrightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireUpLeft()
    {
        specleftBullPos = transform.position;
        specleftBullPos += new Vector2(-6.5f, -13.25f);
        Instantiate(specleftBull, specleftBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireDownRight()
    {
        specrightBullPos = transform.position;
        specrightBullPos += new Vector2(6.5f, 13.25f);
        Instantiate(specrightBull, specrightBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void specFireDownLeft()
    {
        specleftBullPos = transform.position;
        specleftBullPos += new Vector2(-6.5f, -13.25f);
        Instantiate(specleftBull, specleftBullPos, Quaternion.identity);
        MBShot.Play();
    }
//========================================================================================================================================================

    void FlipPlayer()
    {
        isFacingDown = !isFacingDown;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;
    }

    void moveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - MB4Speed * Time.deltaTime);
    }

    void moveUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + MB4Speed * Time.deltaTime);
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
        smExp1Pos += new Vector2(6.3f, 3.2f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp3()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(0f, 14.8f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp4()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-9f, 5.7f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp5()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(3.7f, -9f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp6()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-2.8f, -14.7f);
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
        smExp2Pos += new Vector2(6.3f, 3.2f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp3()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(0f, 14.8f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp4()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-9f, 5.7f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp5()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(3.7f, -9f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp6()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-2.8f, -14.7f);
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
        smExp3Pos += new Vector2(6.3f, 3.2f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp3()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(0f, 14.8f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp4()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-9f, 5.7f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp5()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(3.7f, -9f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp6()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-2.8f, -14.7f);
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

//===========================Phase && Jet
    void MB4PhaseUp()
    {
        MB4PhaserUpPos = transform.position;
        MB4PhaserUpPos += new Vector2(0f, 0f);
        Instantiate(MB4PhaserUp, MB4PhaserUpPos, Quaternion.identity);
    }

    void MB4PhaseDown()
    {
        MB4PhaserDownPos = transform.position;
        MB4PhaserDownPos += new Vector2(0f, 0f);
        Instantiate(MB4PhaserDown, MB4PhaserDownPos, Quaternion.identity);
    }

    void MBFourJetUp()
    {
        MB4JetUpPos = transform.position;
        MB4JetUpPos += new Vector2(0f, -14f);
        Instantiate(MB4JetUp, MB4JetUpPos, Quaternion.identity);
    }

    void MBFourJetDown()
    {
        MB4JetDownPos = transform.position;
        MB4JetDownPos += new Vector2(0f, 14f);
        Instantiate(MB4JetDown, MB4JetDownPos, Quaternion.identity);
    }
}
