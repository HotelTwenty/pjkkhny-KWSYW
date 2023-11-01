using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRightTwo : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    //jets
    public GameObject pJet;
    Vector2 pJetPos;
    float nextJet = 0.0f;
    float jetRate = 0.001f;

    //missiles
    public GameObject Miss1;
    Vector2 Miss1Pos;
    float nextMiss = 0.0f;
    float missRate = 1f;

    //Bulls
    public GameObject pBull;
    Vector2 pBullPos;
    float nextBull = 0.0f;
    float bullRate = 0.1f;

    //lazers
    public GameObject pLazer;
    Vector2 pLazerPos;
    float nextLazer = 0.0f;
    float lazerRate = 0.001f;

    //phaser
    public GameObject phaser;
    Vector2 phaserPos;
    float nextPhaser = 0.0f;
    float phaserRate = 0.01f;

    //phaser
    public GameObject backPhase;
    Vector2 backPhasePos;
    float nextBPhaser = 0.0f;
    float bphaserRate = 0.01f;

    //clearer
    public GameObject clearer, clearer2, clearer3, clearer4, DSKull;
    Vector2 clearerPos, clearerPos2, clearerPos3, clearerPos4, DSkullPos;
    float nextClear = 0.0f;
    float clearRate = 0.1f;
    public static bool ClearAvail, camClear, canMove;
    public static int PHealth, missCount;
    public int curHealth;

    public GameObject missGrey, MissAmmoGrey, destRingGrey, DestRingAmmoGrey, HBFlash;
    public AudioSource pkHit, PkPhaseBack, pkPhaseUD, pkWarp;

    void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        PHealth = 500;
        missCount = 10;
        ClearAvail = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + 5 * Time.deltaTime, transform.position.y);
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
        curHealth = PHealth;
        if(canMove == true)
        {
            bool isLeftKeyHeld = controls.GamePlay.MoveLeft.ReadValue<float>() > 0.1f;
            bool isRightKeyHeld = controls.GamePlay.MoveRight.ReadValue<float>() > 0.1f;
            bool isUpKeyHeld = controls.GamePlay.MoveUp.ReadValue<float>() > 0.1f;
            bool isDownKeyHeld = controls.GamePlay.MoveDown.ReadValue<float>() > 0.1f;

            bool isFireKeyHeld = controls.GamePlay.Shoot.ReadValue<float>() > 0.1f;
            bool isMissKeyHeld = controls.GamePlay.Missile.ReadValue<float>() > 0.1f;
            bool isLaserKeyHeld = controls.GamePlay.Laser.ReadValue<float>() > 0.1f;

            if(move.y > 0.5f || isUpKeyHeld == true)
            {
                GetComponent<Animator> ().SetBool ("doRoll", true);
                transform.Translate(0, 30 * Time.deltaTime, 0);
                if(Time.time > nextPhaser)
                {
                    nextPhaser = Time.time + phaserRate;
                    PlayerPhaser();
                    pkPhaseUD.Play();
                }
            }

            else if(move.y < -0.5f || isDownKeyHeld == true)
            {
                GetComponent<Animator> ().SetBool ("doRoll", true);
                transform.Translate(0, -30 * Time.deltaTime, 0);
                if(Time.time > nextPhaser)
                {
                    nextPhaser = Time.time + phaserRate;
                    PlayerPhaser();
                    pkPhaseUD.Play();
                }
            }

            else
            {
                GetComponent<Animator> ().SetBool ("doRoll", false);
            }

            if(move.x < -0.5f || isLeftKeyHeld == true)
            {
                transform.Translate(-30 * Time.deltaTime, 0, 0);
                if(Time.time > nextBPhaser)
                {
                    nextBPhaser = Time.time + bphaserRate;
                    backPhaser();
                    PkPhaseBack.Play();
                }
            }

            else if(move.x > 0.5f || isRightKeyHeld == true)
            {
                transform.Translate(40 * Time.deltaTime, 0, 0);
                if(Time.time > nextBPhaser)
                {
                    nextJet = Time.time + jetRate;
                    PlayerJet();
                }
            }

            else
            {
                GetComponent<Animator>().SetBool("doRoll", false);
                if(camClear == false && ClearAvail == false)
                {
                    transform.Translate(5 * Time.deltaTime, 0, 0);
                }
            }

            if(isFireKeyHeld)
            {
                if(Time.time > nextBull)
                {
                    nextBull = Time.time + bullRate;
                    PlayerBull();
                }
            }

            if(isMissKeyHeld)
            {
                if (missCount > 0)
                {
                    if(Time.time > nextMiss)
                    {
                        nextMiss = Time.time + missRate;
                        PlayerMiss1();
                    }
                    missCount -= 1;
                }
                
                else
                {
                    missGrey.SetActive(true);
                    MissAmmoGrey.SetActive(true);
                }
            }

            if(isLaserKeyHeld)
            {
                if(Time.time > nextLazer)
                {
                    nextLazer = Time.time + lazerRate;
                    PlayerLazer();
                }
            }
        }

        if(PHealth <= 0)
        {
            canMove = false;
            GetComponent<Animator>().SetBool("isHit", true);
            if(transform.position.y > -875f && transform.position.y < -133f)
            {
                if(transform.position.x > 1120f && transform.position.x < 5655f)
                {
                    Invoke("bottSpawn", 2f);
                    Invoke("PlayerReHealth", 4f);
                }

                else
                {
                    
                    Invoke("defSpawn", 2f);
                    Invoke("PlayerReHealth", 4f);
                }
                
            }

            else if(transform.position.y > 332f && transform.position.y < 1099f)
            {
                Invoke("topSpawn", 2f);
                Invoke("PlayerReHealth", 4f);
            }

            else
            {
                Invoke("defSpawn", 2f);
                Invoke("PlayerReHealth", 4f);
            }
        }
    }

    void LateUpdate()
    {
        bool isClearKeyHeld = controls.GamePlay.Clearer.ReadValue<float>() > 0.1f;
        if(ClearAvail == true)
        {
            if(isClearKeyHeld == true)
            {
                camClear = true;
                destRingGrey.SetActive(true);
                DestRingAmmoGrey.SetActive(true);
                if(Time.time > nextClear)
                {
                    nextClear = Time.time + clearRate;
                    Invoke("PlayerClear", 1.1f);
                    Invoke("PlayerClear2", 1.2f);
                    Invoke("PlayerClear3", 1.3f);
                    Invoke("PlayerClear4", 1.4f);
                    //R2
                    Invoke("PlayerClear", 1.5f);
                    Invoke("PlayerClear2", 1.6f);
                    Invoke("PlayerClear3", 1.7f);
                    Invoke("PlayerClear4", 1.8f);
                    //R3
                    Invoke("PlayerClear", 1.9f);
                    Invoke("PlayerClear2", 2f);
                    Invoke("PlayerClear3", 2.1f);
                    Invoke("PlayerClear4", 2.2f);
                    //R4
                    Invoke("PlayerClear", 2.3f);
                    Invoke("PlayerClear2", 2.4f);
                    Invoke("PlayerClear3", 2.5f);
                    Invoke("PlayerClear4", 2.6f);
                    //R5
                    Invoke("PlayerClear", 2.7f);
                    Invoke("PlayerClear2", 2.8f);
                    Invoke("PlayerClear3", 2.9f);
                    Invoke("PlayerClear4", 3f);
                    Invoke("PlayerDeathSkull", 3.4f);
                }
                Invoke("offCamClear", 3f);
                Invoke("offClearAvail", 3f);
            }
        }
    }

    void offCamClear()
    {
        camClear = false;
    }

    void offClearAvail()
    {
        ClearAvail = false;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void PlayerLazer()
    {
        pLazerPos = transform.position;
        pLazerPos += new Vector2(5f, 0f);
        Instantiate(pLazer, pLazerPos, Quaternion.identity);
    }

    //Bull
    void PlayerBull()
    {
        pBullPos = transform.position;
        pBullPos += new Vector2(5f, 0f);
        Instantiate(pBull, pBullPos, Quaternion.identity);
    }

    //Jet
    void PlayerJet()
    {
        pJetPos = transform.position;
        pJetPos += new Vector2(-6f, 0f);
        Instantiate(pJet, pJetPos, Quaternion.identity);
    }

    //Miss
    void PlayerMiss1()
    {
        Miss1Pos = transform.position;
        Miss1Pos += new Vector2(1.5f, -.5f);
        Instantiate(Miss1, Miss1Pos, Quaternion.identity);
    }

    //Phaser
    void PlayerPhaser()
    {
        phaserPos = transform.position;
        phaserPos += new Vector2(0f, 0f);
        Instantiate(phaser, phaserPos, Quaternion.identity);
    }

    void backPhaser()
    {
        backPhasePos = transform.position;
        backPhasePos += new Vector2(0f, 0f);
        Instantiate(backPhase, backPhasePos, Quaternion.identity);
    }

    //Clearer
    void PlayerClear()
    {
        clearerPos = transform.position;
        clearerPos += new Vector2(0f, 0f);
        Instantiate(clearer, clearerPos, Quaternion.identity);
    }

    void PlayerClear2()
    {
        clearerPos2 = transform.position;
        clearerPos2 += new Vector2(0f, 0f);
        Instantiate(clearer2, clearerPos2, Quaternion.identity);
    }

    void PlayerClear3()
    {
        clearerPos3 = transform.position;
        clearerPos3 += new Vector2(0f, 0f);
        Instantiate(clearer3, clearerPos3, Quaternion.identity);
    }

    void PlayerClear4()
    {
        clearerPos4 = transform.position;
        clearerPos4 += new Vector2(0f, 0f);
        Instantiate(clearer4, clearerPos4, Quaternion.identity);
    }

    void PlayerDeathSkull()
    {
        DSkullPos = transform.position;
        DSkullPos += new Vector2(0f, 0f);
        Instantiate(DSKull, DSkullPos, Quaternion.identity);
    }

    void moveAgain()
    {
        canMove = true;
    }

    void onHit()
    {
        GetComponent<Animator>().SetBool("isHit", true);
        HBFlash.SetActive(true);
    }

    void offHit()
    {
        GetComponent<Animator>().SetBool("isHit", false);
        HBFlash.SetActive(false);
    }

    void topSpawn()
    {
        transform.position = new Vector3(1774f, 748f, 0f);
        GetComponent<Animator>().SetBool("isHit", false);
        canMove = true;
    }

    void bottSpawn()
    {
        transform.position = new Vector3(1200f, -755f, 0f);
        GetComponent<Animator>().SetBool("isHit", false);
        canMove = true;
    }

    void defSpawn()
    {
        transform.position = new Vector3(900f, 0f, 0f);
        GetComponent<Animator>().SetBool("isHit", false);
        canMove = true;
    }

    void PlayerReHealth()
    {
        PHealth = 500;
    }

    void OnCollisionEnter2D (Collision2D col)
	{
        if (col.gameObject.tag == "ToWormBase") 
	    {
            transform.position = new Vector3(5264.4f, 653f, 0f);
            pkWarp.Play();
        }

        if (col.gameObject.tag == "EnemyGreen")
	    {
            PHealth -= 10;
            onHit();
            pkHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyBlue")
	    {
            PHealth -= 20;
            onHit();
            pkHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyPurple")
	    {
            PHealth -= 40;
            onHit();
            pkHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "Enemy") //EnemyPink
	    {
            PHealth -= 80;
            onHit();
            pkHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyRed") 
	    {
            PHealth -= 500;
            onHit();
            pkHit.Play();
            Invoke("offHit", .3f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Path1":
            canMove = false;
            Invoke("moveAgain", 60.5f);
            break;

            case "Path2":
            canMove = false;
            Invoke("moveAgain", 55.5f);
            break;
        }
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
