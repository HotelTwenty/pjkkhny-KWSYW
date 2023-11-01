using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMan : MonoBehaviour
{
    public Animator AYL, AST, BA, TW, OTh, TYY, TYB, TYR, GELog;
    public GameObject GodKillers;
    // Start is called before the first frame update
    void Start()
    {
        GodKillers.SetActive(false);
        Invoke("openAYL", 5f);
        Invoke("closeAYL", 10f);
        Invoke("openAST", 11f);
        Invoke("closeAST", 16f);
        Invoke("openBA", 17f);
        Invoke("closeBA", 22f);
        Invoke("openTW", 23f);
        Invoke("openOTh", 27f);
        Invoke("closeTW", 29f);
        Invoke("closeOTh", 32f);
        Invoke("openGK", 35f);
        Invoke("closeGK", 37f);
        Invoke("openThankMsg", 42f);
        Invoke("closeThankMsg", 48f);
        Invoke("toMM", 51f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toMM()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void openAYL()
    {
        AYL.SetBool("isOpen", true);
    }

    void closeAYL()
    {
        AYL.SetBool("isOpen", false);
    }

    void openAST()
    {
        AST.SetBool("isOpen", true);
    }

    void closeAST()
    {
        AST.SetBool("isOpen", false);
    }

    void openBA()
    {
        BA.SetBool("isOpen", true);
    }

    void closeBA()
    {
        BA.SetBool("isOpen", false);
    }

    void openTW()
    {
        TW.SetBool("isOpen", true);
    }

    void closeTW()
    {
        TW.SetBool("isOpen", false);
    }

    void openOTh()
    {
        OTh.SetBool("isOpen", true);
    }

    void closeOTh()
    {
        OTh.SetBool("isOpen", false);
    }

    void openGK()
    {
        GodKillers.SetActive(true);
    }

    void closeGK()
    {
        GodKillers.SetActive(false);
    }

    void openThankMsg()
    {
        TYY.SetBool("isOpen", true);
        TYR.SetBool("isOpen", true);
        TYB.SetBool("isOpen", true);
        GELog.SetBool("isOpen", true);
    }

    void closeThankMsg()
    {
        TYY.SetBool("isOpen", false);
        TYR.SetBool("isOpen", false);
        TYB.SetBool("isOpen", false);
        GELog.SetBool("isOpen", false);
    }
}
