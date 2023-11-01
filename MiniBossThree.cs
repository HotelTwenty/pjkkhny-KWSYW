using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossThree : MonoBehaviour
{
    public GameObject leftGun, rightGun, bombDrop, bossHit, MBPhaser;
    Vector2 MBPhaserPos;
    float nextPhase = 0.0f;
    float phaseRate = .1f;

    public static int MBThreeHealth, pHealth;
    public static bool movingLR, movingUpDown, movingGround;
    public int curHealth;

    public GameObject smExp1, smExp2, smExp3, BExpWhite, BExpPurp, BExpOrange, BExpRed, BossDeathFlash;
    Vector2 smExp1Pos, smExp2Pos, smExp3Pos, BExpWhitePos, BExpPurpPos, BExpOrangePos, BExpRedPos;
    public AudioSource MBHit, MBMinExp, MBBigExp;

    // Start is called before the first frame update
    void Start()
    {
        bossHit.SetActive(false);
        leftGun.SetActive(false);
        rightGun.SetActive(false);
        //bombDrop.SetActive(false);
        MBThreeHealth = 150;
        movingGround = false;
        movingUpDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = MBThreeHealth;
        pHealth = KellyChildMove.kHealth;
        if(pHealth <= 0)
        {
            movingGround = false;
            movingUpDown = false;
            GetComponent<Animator> ().SetBool ("isGroundMoving", false);
            GetComponent<Animator> ().SetBool ("isAirMoving", false);
        }

        if(MBThreeHealth > 0)
        {
            if(MBThreeHealth > 100 && MBThreeHealth > 0)
            {
                movingLR = true;
                leftGun.SetActive(false);
                rightGun.SetActive(false);
                GetComponent<Animator> ().SetBool ("isMovingLeftRight", true);
            }

            else if(MBThreeHealth > 50 && MBThreeHealth <= 100)
            {
                bombDrop.SetActive(false);
                leftGun.SetActive(false);
                rightGun.SetActive(false);
                movingLR = false;
                movingGround = true;
                GetComponent<Animator> ().SetBool ("isMovingLeftRight", false);
                GetComponent<Animator> ().SetBool ("isGroundMoving", true);
            }

            else if(MBThreeHealth > 0 && MBThreeHealth <= 50)
            {
                bombDrop.SetActive(false);
                leftGun.SetActive(true);
                rightGun.SetActive(true);
                movingGround = false;
                movingLR = false;
                movingUpDown = true;
                GetComponent<Animator> ().SetBool ("isMovingLeftRight", false);
                GetComponent<Animator> ().SetBool ("isGroundMoving", false);
                GetComponent<Animator> ().SetBool ("isAirMoving", true);
                if(MBThreeHealth <= 25 && Time.time > nextPhase)
                {
                    nextPhase = Time.time + phaseRate;
                    MB3Phaze();
                }
            }
        }

        else
        {
            bombDrop.SetActive(false);
            leftGun.SetActive(false);
            rightGun.SetActive(false);
            GetComponent<Animator> ().SetBool ("isMoving", false);
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "PalyerLaser")
        {
            MBThreeHealth -= 1;
            bossHit.SetActive(true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }

        if(col.gameObject.tag == "PlayerBull")
        {
            MBThreeHealth -= 3;
            bossHit.SetActive(true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }
    }

    void offHit()
    {
        bossHit.SetActive(false);
    }

    //================================================================
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
        smExp1Pos += new Vector2(-5.3f, -6.1f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp3()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(7.4f, -1.8f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp4()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(0f, 6.5f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp5()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-3.2f, -2.2f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp6()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(3.1f, -5f);
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
        smExp2Pos += new Vector2(-5.3f, -6.1f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp3()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(7.4f, -1.8f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp4()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(0f, 6.6f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp5()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-3.2f, 2.2f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp6()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(3.1f, -5f);
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
        smExp3Pos += new Vector2(-5.3f, -6.1f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp3()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(7.4f, -1.8f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp4()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(0f, 6.5f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp5()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-3.2f, 2.2f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp6()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(3.1f, -5f);
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

    void MB3Phaze()
    {
        MBPhaserPos = transform.position;
        MBPhaserPos += new Vector2(0f, 0f);
        Instantiate(MBPhaser, MBPhaserPos, Quaternion.identity);
    }
}
