using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBTwo : MonoBehaviour
{
    public static int MBTwoHealth;
    public int curHealth;
    public static int pHealth;

    public GameObject MBLaze;
    Vector2 MBLazePos;
    float nextLazer = 0.0f;
    float LazerRate = .05f;

    public GameObject MBBull;
    Vector2 MBBullPos;
    float nextBull = 0.0f;
    float bullRate = .5f;

    public GameObject MBBomb;
    Vector2 MBBombPos;
    float nextBomb = 0.0f;
    float bombRate = .5f;

    public GameObject MB2Phaser;
    Vector2 MB2PhaserPos;
    float nextPhase = 0.0f;
    float phaseRate = .1f;

    private Transform target;
    public GameObject smExp1, smExp2, smExp3, BExpWhite, BExpPurp, BExpOrange, BExpRed, BossDeathFlash;
    Vector2 smExp1Pos, smExp2Pos, smExp3Pos, BExpWhitePos, BExpPurpPos, BExpOrangePos, BExpRedPos;
    public GameObject MB2Hit;

    public AudioSource MBHit, MBShot, MBLaser, MBMinExp, MBBigExp;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        MBTwoHealth = 300;
        MB2Hit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = MBTwoHealth;
        pHealth = KellyChildMove.kHealth;
        if(pHealth <= 0)
        {
            GetComponent<Animator> ().SetBool ("isMoving", false);
            MBTwoHealth = 300;
        }

        if(MBTwoHealth > 0)
        {
            GetComponent<Animator> ().SetBool ("isMoving", true);
            if(MBTwoHealth > 200 && MBTwoHealth > 0)
            {
                if(target.position.y > transform.position.y && Time.time > nextLazer)
                {
                    nextLazer = Time.time + LazerRate;
                    LeftLaser();
                    RightLaser();
                    MBLaser.Play();
                }
            }

            else if(MBTwoHealth <= 200  && MBTwoHealth > 0)
            {
                if(target.position.y > transform.position.y && Time.time > nextBull)
                {
                    nextBull = Time.time + bullRate;
                    LeftShot();
                    RightShot();
                }

                if(MBTwoHealth <= 100  && MBTwoHealth > 0)
                {
                    if(target.position.y > transform.position.y && Time.time > nextBomb)
                    {
                        nextBomb = Time.time + bombRate;
                        MBTwoBomb();
                    }

                    if(Time.time > nextPhase)
                    {
                        nextPhase = Time.time + phaseRate;
                        MBTwoPhaser();
                    }
                }
            }
        }

        else
        {
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
            MBTwoHealth -= 1;
            MB2Hit.SetActive(true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }

        if(col.gameObject.tag == "PlayerBull")
        {
            MBTwoHealth -= 3;
            MB2Hit.SetActive(true);
            MBHit.Play();
            Invoke("offHit", .7f);
        }
    }

    void offHit()
    {
        MB2Hit.SetActive(false);
    }

    //Lasers=============================================================================================
    void LeftLaser()
    {
        MBLazePos = transform.position;
        MBLazePos += new Vector2(-13.74f, -8f);
        Instantiate(MBLaze, MBLazePos, Quaternion.identity);
    }

    void RightLaser()
    {
        MBLazePos = transform.position;
        MBLazePos += new Vector2(13.74f, -8f);
        Instantiate(MBLaze, MBLazePos, Quaternion.identity);
        MBLaser.Play();
    }

    //Bullets=============================================================================================
    void LeftShot()
    {
        MBBullPos = transform.position;
        MBBullPos += new Vector2(-9.8f, -5.48f);
        Instantiate(MBBull, MBBullPos, Quaternion.identity);
        MBShot.Play();
    }

    void RightShot()
    {
        MBBullPos = transform.position;
        MBBullPos += new Vector2(9.8f, -5.48f);
        Instantiate(MBBull, MBBullPos, Quaternion.identity);
    }

    //Lasers=============================================================================================
    void MBTwoBomb()
    {
        MBBombPos = transform.position;
        MBBombPos += new Vector2(0f, -8.7f);
        Instantiate(MBBomb, MBBombPos, Quaternion.identity);
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
        smExp1Pos += new Vector2(-13f, -4f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp3()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(-8f, -0f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp4()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(8f, 10f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
    }

    void whiteExp5()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(13f, -6f);
        Instantiate(smExp1, smExp1Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void whiteExp6()
    {
        smExp1Pos = transform.position;
        smExp1Pos += new Vector2(9f, 6f);
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
        smExp2Pos += new Vector2(-13f, -4f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp3()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(-8f, 0f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp4()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(8f, 10f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
    }

    void orangeExp5()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(13f, -6f);
        Instantiate(smExp2, smExp2Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void orangeExp6()
    {
        smExp2Pos = transform.position;
        smExp2Pos += new Vector2(9f, 6f);
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
        smExp3Pos += new Vector2(-13f, -4f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp3()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(-8f, 0f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp4()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(8f, 10f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
    }

    void purpExp5()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(13f, -6f);
        Instantiate(smExp3, smExp3Pos, Quaternion.identity);
        MBMinExp.Play();
    }

    void purpExp6()
    {
        smExp3Pos = transform.position;
        smExp3Pos += new Vector2(9f, 6f);
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

    void MBTwoPhaser()
    {
        MB2PhaserPos = transform.position;
        MB2PhaserPos += new Vector2(0f, 0f);
        Instantiate(MB2Phaser, MB2PhaserPos, Quaternion.identity);
    }
}
