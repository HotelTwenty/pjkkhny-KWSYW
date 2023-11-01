using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KellyChildMove : MonoBehaviour
{
    Vector2 move;
    public float xVal;
    PlayerControls controls;
    //================ FIX UP DIAG FLAMES ==================================================//
    public float moveX;
    public bool isGrounded = false;
    int playerJumpPower = 15000;

    public static bool facingRight = true;


    bool isPink, canMove;
    public bool bullAct, diagFire;

    //Bullets
    public GameObject KBRight, KBRightDown, KBRightUp, KBLeft, KBLeftDown, KBLeftUp;
    Vector2 KBRightPos, KBRightDownPos, KBRightUpPos, KBLeftPos, KBLeftDownPos, KBLeftUpPos;
    float nextFire = 0.0f;
    float fireRate = .1f;

    //flames
    public GameObject KefacingRight, KefacingRightDown, KefacingRightUp, KeFLeft, KeFLeftDown, KeFLeftUp;
    Vector2 KefacingRightPos, KefacingRightDownPos, KefacingRightUpPos, KeFLeftPos, KeFLeftDownPos, KeFLeftUpPos;
    float nextFlame = 0.0f;
    float flameRate = .005f;

    bool isFiringLeft, isFiringLeftUp, isFiringLeftDown, isFiringRight, isFiringRightDown, isFiringRightUp;
    //UIThings
    public GameObject KHealthHit, flameOp, saveUI, DeathDarkener;
    public static int kHealth;
    public int curHealth;
    public bool kJumping, fngRight, fngRightDown, fngRightUp, fngLeft, fngLeftDown, fngLeftUp;

    Vector3 SpawnPos1;
    public AudioSource kcHit, kcSave;

    void Awake()
    {
        controls = new PlayerControls();
        kJumping = false;
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
        
        fngRight = true;
        fngRightDown = false;
        fngRightUp = false;
        fngLeft = false;
        fngLeftDown = false;
        fngLeftUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        saveUI.SetActive(false);
        canMove = true;
        isPink = false;
        bullAct = true;
        diagFire = false;
        kHealth = 500;
        SpawnPos1 = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        xVal = move.x;
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m * 0, Space.World);

        if(fngRight == true)
        {
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngRightDown == true)
        {
            fngRight = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngRightUp == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngLeft = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngLeft == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeftDown = false;
            fngLeftUp = false;
        }

        if(fngLeftDown == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftUp = false;
        }

        if(fngLeftUp == true)
        {
            fngRight = false;
            fngRightDown = false;
            fngRightUp = false;
            fngLeft = false;
            fngLeftDown = false;
        }

        if(transform.position.x > 6757f && transform.position.x < 7023f)
        {
            if(transform.position.y > 15f && transform.position.y < 140f)
            {
                isPink = true;
            }
        }

        else
        {
            isPink = false;
        }

        if(kHealth <= 0)
        {
            DeathDarkener.SetActive(true);
            onHit();
            Invoke("offHit", 2f);
            Invoke("playerSpawnShift", .1f);
            Invoke("PlayerRespawn", 5f);
            
        }
        curHealth = kHealth;

        if(canMove == true)
        {
            bool isWeapSwitchPressed = controls.GamePlay.SwitchWeap.ReadValue<float>() > 0.1f;
            if(isPink == false)
            {
                GetComponent<Animator> ().SetBool ("isPink", false);
                GetComponent<Animator> ().SetBool ("isPRunning", false);
                GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                GetComponent<Animator>().SetBool("isPHit", false);
                kellyMove();
            }

            else
            {
                GetComponent<Animator> ().SetBool ("isPink", true);
                GetComponent<Animator> ().SetBool ("isRunning", false);
                GetComponent<Animator> ().SetBool ("isAimingUp", false);
                GetComponent<Animator> ().SetBool ("isAimingDown", false);
                GetComponent<Animator>().SetBool("isHit", false);
                kellyPinkMove();
            }

            if (isWeapSwitchPressed == true)
            {
                if(bullAct == true)
                {
                    bullAct = false;
                }

                else
                {
                    bullAct = true;
                }
            }

            if(bullAct == false)
            {
                flameOp.SetActive(true);
            }

            else
            {
                flameOp.SetActive(false);
            }
        }
        
    }

    void Fire()
    {
        if(fngRightDown == true)
        {
            BullFireRightDown();
        }

        else if(fngRightUp == true)
        {
            BullFireRightUp();
        }

        else if(fngLeft == true)
        {
            BullFireLeft();
        }

        else if(fngLeftDown == true)
        {
            BullFireLeftDown();
        }

        else if(fngLeftUp == true)
        {
            BullFireLeftUp();
        }

        else if(fngRight == true)
        {
            BullFireRight();
        }
    }

    void Fire2()
    {
        if(fngRightDown == true)
        {
            FlameFireRightDown();
        }

        else if(fngRightUp == true)
        {
            FlameFireRightUp();
        }

        else if(fngLeft == true)
        {
            FlameFireLeft();
        }

        else if(fngLeftDown == true)
        {
            FlameFireLeftDown();
        }

        else if(fngLeftUp == true)
        {
            FlameFireLeftUp();
        }

        else if(fngRight == true)
        {
            FlameFireRight();
        }
    }

    void kellyMove()
    {
        bool isLeftKeyHeld = controls.GamePlay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.GamePlay.MoveRight.ReadValue<float>() > 0.1f;
        bool isUpKeyHeld = controls.GamePlay.MoveUp.ReadValue<float>() > 0.1f;
        bool isDownKeyHeld = controls.GamePlay.MoveDown.ReadValue<float>() > 0.1f;

        bool isFire1KeyHeld = controls.GamePlay.Shoot.ReadValue<float>() > 0.1f;
        bool isJumpKeyHeld = controls.GamePlay.JumpSkip.ReadValue<float>() > 0.1f;

        if (canMove == true)
        {
            if(isFire1KeyHeld == true)
            {
                if (bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        Fire();
                    }
                }

                else
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        Fire2();
                    }
                }            
            }

            if(isLeftKeyHeld || move.x < -0.1f)
            {
                transform.position = new Vector2(transform.position.x - 40 * Time.deltaTime, transform.position.y);
                if(kJumping == false)
                {
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                }

                if(isGrounded == true)
                {
                    if(isUpKeyHeld || move.y > 0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isAimingUp", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = true;
                    }

                    else if(isDownKeyHeld || move.y < -0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = true;
                        fngLeftUp = false;
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isAimingDown", false);
                    GetComponent<Animator> ().SetBool ("isAimingUp", false);
                    if(kJumping == true)
                    {
                        if(isUpKeyHeld || move.y > 0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isAimingUp", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = true;
                        }

                        else if(isDownKeyHeld || move.y < -0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isAimingDown", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = true;
                            fngLeftUp = false;
                        }        

                        if(isRightKeyHeld == true || move.x > 0.1f)
                        {
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }

                        if(isLeftKeyHeld == true || move.x < -0.1f)
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                        }               
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                if(facingRight == true)
                {
                    FlipPlayer();
                    facingRight = false;
                }
            }

            else if(isRightKeyHeld || move.x > 0.1f)
            {
                transform.position = new Vector2(transform.position.x + 40 * Time.deltaTime, transform.position.y);
                if(kJumping == false)
                {
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                }

                if(isGrounded == true)
                {
                    if(isUpKeyHeld || move.y > 0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isAimingUp", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = true;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }

                    else if(isDownKeyHeld || move.y < -0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", true);
                        fngRight = false;
                        fngRightDown = true;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isAimingDown", false);
                    GetComponent<Animator> ().SetBool ("isAimingUp", false);
                    if(kJumping == true)
                    {
                        if(isUpKeyHeld || move.y > 0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isAimingUp", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = true;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }

                        else if(isDownKeyHeld || move.y < -0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isAimingDown", true);
                            fngRight = false;
                            fngRightDown = true;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }  

                        if(isRightKeyHeld == true || move.x > 0.1f)
                        {
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        if(isLeftKeyHeld == true || move.x < -0.1f)
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                        }                          
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                if(facingRight == false)
                {
                    FlipPlayer();
                    facingRight = true;
                }
            }

            else
            {
                GetComponent<Animator> ().SetBool ("isRunning", false);
                GetComponent<Animator> ().SetBool ("isAimingDown", false);
                GetComponent<Animator> ().SetBool ("isAimingUp", false);
                if(facingRight == true)
                {
                    fngRight = true;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }

                else
                {
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = true;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }
            }

            if(isJumpKeyHeld == true && isGrounded == true)
            {
                Jump();
            }
        }
        
        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator> ().SetBool ("isRunning", true);
            transform.position = new Vector2(transform.position.x + moveX * 40 * Time.deltaTime, transform.position.y);
    

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isAimingUp", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireLeftUp();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireLeftUp();
                    }
                }
            }

            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isAimingDown", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireLeftDown();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireLeftDown();
                    }
                }
            }

            else
            {
                diagFire = false;
                GetComponent<Animator> ().SetBool ("isAimingDown", false);
                GetComponent<Animator> ().SetBool ("isAimingUp", false);
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator> ().SetBool ("isRunning", true);
            transform.position = new Vector2(transform.position.x + moveX * 40 * Time.deltaTime, transform.position.y);
    
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isAimingUp", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireRightUp(); 
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireRightUp();
                    }
                }
            }

            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isAimingDown", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireRightDown();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireRightDown();
                    }
                }
            }

            else
            {
                diagFire = false;
                GetComponent<Animator> ().SetBool ("isAimingDown", false);
                GetComponent<Animator> ().SetBool ("isAimingUp", false);
            }
        }

        else
        {
            GetComponent<Animator> ().SetBool ("isRunning", false);
            GetComponent<Animator> ().SetBool ("isAimingDown", false);
            GetComponent<Animator> ().SetBool ("isAimingUp", false);
        }

        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }

        if(facingRight == true && diagFire == false)
        {
            if(Input.GetKey(KeyCode.F) && bullAct == true)
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    BullFireRight();
                }
            }

            else if(Input.GetKey(KeyCode.F) && bullAct == false)
            {
                if(Time.time > nextFlame)
                {
                    nextFlame = Time.time + flameRate;
                    FlameFireRight();
                }
            }
        }

        else if (facingRight == false && diagFire == false)
        {
            if(Input.GetKey(KeyCode.F) && bullAct == true)
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    BullFireLeft();
                }
            }

            else if(Input.GetKey(KeyCode.F) && bullAct == false)
            {
                if(Time.time > nextFlame)
                {
                    nextFlame = Time.time + flameRate;
                    FlameFireLeft();
                }
            }
        }*/
    }
    

    void kellyPinkMove()
    {
        bool isLeftKeyHeld = controls.GamePlay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.GamePlay.MoveRight.ReadValue<float>() > 0.1f;
        bool isUpKeyHeld = controls.GamePlay.MoveUp.ReadValue<float>() > 0.1f;
        bool isDownKeyHeld = controls.GamePlay.MoveDown.ReadValue<float>() > 0.1f;

        bool isFire1KeyHeld = controls.GamePlay.Shoot.ReadValue<float>() > 0.1f;
        bool isJumpKeyHeld = controls.GamePlay.JumpSkip.ReadValue<float>() > 0.1f;

        if (canMove == true)
        {
            if(isFire1KeyHeld == true)
            {
                if (bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        Fire();
                    }
                }

                else
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        Fire2();
                    }
                }            
            }

            if(isLeftKeyHeld || move.x < -0.1f)
            {
                transform.position = new Vector2(transform.position.x - 40 * Time.deltaTime, transform.position.y);
                if(kJumping == false)
                {
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                }

                if(isGrounded == true)
                {
                    if(isUpKeyHeld || move.y > 0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = true;
                    }

                    else if(isDownKeyHeld || move.y < -0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = true;
                        fngLeftUp = false;
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                    GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                    if(kJumping == true)
                    {
                        if(isUpKeyHeld || move.y > 0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = true;
                        }

                        else if(isDownKeyHeld || move.y < -0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = true;
                            fngLeftUp = false;
                        }        

                        if(isRightKeyHeld == true || move.x > 0.1f)
                        {
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }

                        if(isLeftKeyHeld == true || move.x < -0.1f)
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                        }               
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                if(facingRight == true)
                {
                    FlipPlayer();
                    facingRight = false;
                }
            }

            else if(isRightKeyHeld || move.x > 0.1f)
            {
                transform.position = new Vector2(transform.position.x + 40 * Time.deltaTime, transform.position.y);
                if(kJumping == false)
                {
                    GetComponent<Animator> ().SetBool ("isRunning", true);
                }

                if(isGrounded == true)
                {
                    if(isUpKeyHeld || move.y > 0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                        fngRight = false;
                        fngRightDown = false;
                        fngRightUp = true;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }

                    else if(isDownKeyHeld || move.y < -0.1f)
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                        fngRight = false;
                        fngRightDown = true;
                        fngRightUp = false;
                        fngLeft = false;
                        fngLeftDown = false;
                        fngLeftUp = false;
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                else
                {
                    GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                    GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                    if(kJumping == true)
                    {
                        if(isUpKeyHeld || move.y > 0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = true;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }

                        else if(isDownKeyHeld || move.y < -0.1f)
                        {
                            GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                            fngRight = false;
                            fngRightDown = true;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }  

                        if(isRightKeyHeld == true || move.x > 0.1f)
                        {
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        if(isLeftKeyHeld == true || move.x < -0.1f)
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                        }                          
                    }

                    else
                    {
                        GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                        GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                        if(facingRight == true)
                        {
                            fngRight = true;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = false;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                        
                        else
                        {
                            fngRight = false;
                            fngRightDown = false;
                            fngRightUp = false;
                            fngLeft = true;
                            fngLeftDown = false;
                            fngLeftUp = false;
                        }
                    }
                }

                if(facingRight == false)
                {
                    FlipPlayer();
                    facingRight = true;
                }
            }

            else
            {
                GetComponent<Animator> ().SetBool ("isRunning", false);
                GetComponent<Animator> ().SetBool ("isPAimingDown", false);
                GetComponent<Animator> ().SetBool ("isPAimingUp", false);
                if(facingRight == true)
                {
                    fngRight = true;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = false;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }

                else
                {
                    fngRight = false;
                    fngRightDown = false;
                    fngRightUp = false;
                    fngLeft = true;
                    fngLeftDown = false;
                    fngLeftUp = false;
                }
            }

            if(isJumpKeyHeld == true && isGrounded == true)
            {
                Jump();
            }
        }
        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator> ().SetBool ("isPRunning", true);
            transform.position = new Vector2(transform.position.x + moveX * 40 * Time.deltaTime, transform.position.y);
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireLeftUp();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireLeftUp();
                    }
                }
            }

            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireLeftDown();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireLeftDown();
                    }
                }
            }

            else
            {
                diagFire = false;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator> ().SetBool ("isPRunning", true);
            transform.position = new Vector2(transform.position.x + moveX * 40 * Time.deltaTime, transform.position.y); 
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isPAimingUp", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireRightUp();    
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireRightUp();
                    }
                }
            }

            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                diagFire = true;
                GetComponent<Animator> ().SetBool ("isPAimingDown", true);
                if(Input.GetKey(KeyCode.F) && bullAct == true)
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        BullFireRightDown();
                    }
                }

                else if(Input.GetKey(KeyCode.F) && bullAct == false)
                {
                    if(Time.time > nextFlame)
                    {
                        nextFlame = Time.time + flameRate;
                        FlameFireRightDown();
                    }
                }
            }

            else
            {
                diagFire = false;
            }
        }

        else
        {

            GetComponent<Animator> ().SetBool ("isPRunning", false);
            GetComponent<Animator> ().SetBool ("isPAimingDown", false);
            GetComponent<Animator> ().SetBool ("isPAimingUp", false);
        }

        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }

        if(facingRight == true && diagFire == false)
        {
            if(Input.GetKey(KeyCode.F) && bullAct == true)
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    BullFireRight();
                }
            }

            else if(Input.GetKey(KeyCode.F) && bullAct == false)
            {
                if(Time.time > nextFlame)
                {
                    nextFlame = Time.time + flameRate;
                    FlameFireRight();
                }
            }
        }

        else if (facingRight == false && diagFire == false)
        {
            if(Input.GetKey(KeyCode.F) && bullAct == true)
            {
                if(Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    BullFireLeft();
                }
            }

            else if(Input.GetKey(KeyCode.F) && bullAct == false)
            {
                if(Time.time > nextFlame)
                {
                    nextFlame = Time.time + flameRate;
                    FlameFireLeft();
                }
            }
        }*/
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Ground") 
	    {
            isGrounded = true;
        }

        if (col.gameObject.tag == "EnemyGreen")
	    {
            kHealth -= 10;
            onHit();
            kcHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyBlue")
	    {
            kHealth -= 20;
            onHit();
            kcHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyPurple")
	    {
            kHealth -= 40;
            onHit();
            kcHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "Enemy") //EnemyPink
	    {
            kHealth -= 80;
            onHit();
            kcHit.Play();
            Invoke("offHit", .3f);
        }

        if (col.gameObject.tag == "EnemyRed") 
	    {
            kHealth -= 500;
            onHit();
            kcHit.Play();
            Invoke("offHit", .3f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "SPCyan":
            kHealth = 500;
            kcSave.Play();
            SpawnPos1 = new Vector3(2608f, 40f, 0f);
            saveUI.SetActive(true);
            Invoke("closeSaveUI", 2f);
            break;

            case "SPViolet":
            kHealth = 500;
            kcSave.Play();
            SpawnPos1 = new Vector3(2568f, -130f, 0f);
            saveUI.SetActive(true);
            Invoke("closeSaveUI", 2f);
            break;

            
            case "SPCBoss":
            kHealth = 500;
            kcSave.Play();
            SpawnPos1 = new Vector3(4879f, 424f, 0f);
            saveUI.SetActive(true);
            Invoke("closeSaveUI", 2f);
            break;

            case "SPVBoss":
            kHealth = 500;
            kcSave.Play();
            SpawnPos1 = new Vector3(4879f, -128f, 0f);
            saveUI.SetActive(true);
            Invoke("closeSaveUI", 2f);
            break;

            case "SPBB":
            kHealth = 500;
            kcSave.Play();
            SpawnPos1 = new Vector3(6116f, 60f, 0f);
            saveUI.SetActive(true);
            Invoke("closeSaveUI", 2f);
            break;

            case "GTrig":
            canMove = false;
            Invoke("MoveAgain", 44f);
            break;

            case "MBTrigs":
            kcSave.Play();
            break;

            case "OutTele":
            kHealth = 500;
            break;
        }
    }

    void MoveAgain()
    {
        canMove = true;
    }

    void onHit()
    {
        GetComponent<Animator>().SetBool("isHit", true);
        GetComponent<Animator>().SetBool("isPHit", true);
        KHealthHit.SetActive(false);
    }

    void offHit()
    {
        GetComponent<Animator>().SetBool("isHit", false);
        GetComponent<Animator>().SetBool("isPHit", false);
        KHealthHit.SetActive(false);
    }

    //Bullets
    void BullFireRight()
    {
        KBRightPos = transform.position;
        KBRightPos += new Vector2(3.1f, -1.93f);
        Instantiate(KBRight, KBRightPos, Quaternion.identity);
    }

    void BullFireRightDown()
    {
        KBRightDownPos = transform.position;
        KBRightDownPos += new Vector2(2.16f, -3.5f);
        Instantiate(KBRightDown, KBRightDownPos, Quaternion.identity);
    }

    void BullFireRightUp()
    {
        KBRightUpPos = transform.position;
        KBRightUpPos += new Vector2(2.16f, .79f);
        Instantiate(KBRightUp, KBRightUpPos, Quaternion.identity);
    }

    void BullFireLeft()
    {
        KBLeftPos = transform.position;
        KBLeftPos += new Vector2(-3.1f, -1.93f);
        Instantiate(KBLeft, KBLeftPos, Quaternion.identity);
    }

    void BullFireLeftDown()
    {
        KBLeftDownPos = transform.position;
        KBLeftDownPos += new Vector2(-2.16f, -3.5f);
        Instantiate(KBLeftDown, KBLeftDownPos, Quaternion.identity);
    }

    void BullFireLeftUp()
    {
        KBLeftUpPos = transform.position;
        KBLeftUpPos += new Vector2(-2.16f, .79f);
        Instantiate(KBLeftUp, KBLeftUpPos, Quaternion.identity);
    }

    //flames
    void FlameFireRight()
    {
        KefacingRightPos = transform.position;
        KefacingRightPos += new Vector2(3.1f, -1.93f);
        Instantiate(KefacingRight, KefacingRightPos, Quaternion.identity);
    }

    void FlameFireRightDown()
    {
        KefacingRightDownPos = transform.position;
        KefacingRightDownPos += new Vector2(2.16f, -3.5f);
        Instantiate(KefacingRightDown, KefacingRightDownPos, Quaternion.identity);
    }

    void FlameFireRightUp()
    {
        KefacingRightUpPos = transform.position;
        KefacingRightUpPos += new Vector2(2.16f, .79f);
        Instantiate(KefacingRightUp, KefacingRightUpPos, Quaternion.identity);
    }

    
    void FlameFireLeft()
    {
        KeFLeftPos = transform.position;
        KeFLeftPos += new Vector2(-3.1f, -1.93f);
        Instantiate(KeFLeft, KeFLeftPos, Quaternion.identity);
    }

    void FlameFireLeftDown()
    {
        KeFLeftDownPos = transform.position;
        KeFLeftDownPos += new Vector2(-2.16f, -3.5f);
        Instantiate(KeFLeftDown, KeFLeftDownPos, Quaternion.identity);
    }

    void FlameFireLeftUp()
    {
        KeFLeftUpPos = transform.position;
        KeFLeftUpPos += new Vector2(-2.16f, .79f);
        Instantiate(KeFLeftUp, KeFLeftUpPos, Quaternion.identity);
    }

    void PlayerRespawn()
    {
        kHealth = 500;
        DeathDarkener.SetActive(false);
    }

    void playerSpawnShift()
    {
        transform.position = SpawnPos1;
    }

    void closeSaveUI()
    {
        saveUI.SetActive(false);
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
