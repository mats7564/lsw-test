using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expressions : MonoBehaviour
{
    [SerializeField] GameObject playerBalloon;
    [SerializeField] Animator animPlayerBalloon;

    [SerializeField] GameObject studentBalloon;
    [SerializeField] Animator animStudentBalloon;
    
    [SerializeField] GameObject saleBalloon;
    [SerializeField] Animator animSaleBalloon;

    [SerializeField] GameObject tutorialTalkUI;

    bool enterShop;
    bool enterTalk;

    public static event Action StartTalk;
    public static event Action CanStartAfterTalk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CallConversation();
        CallShopConversation();
    }

    void UpdateUI()
    {
        if (enterTalk)
        {
            tutorialTalkUI.SetActive(true);
        }
        else
        {
            tutorialTalkUI.SetActive(false);
        }
    }

    void CallConversation()
    {
        if (Input.GetKeyDown(KeyCode.C) && enterTalk && !studentBalloon.activeInHierarchy)
        {
            StudentAngry();
        }
    }

    void CallShopConversation()
    {
        if (Input.GetKeyDown(KeyCode.Z) && enterShop && !playerBalloon.activeInHierarchy && !saleBalloon.activeInHierarchy)
        {
            StartTalk?.Invoke();
            PlayerExclamation();
            Invoke("SaleResponds", 2f);  
        }
    }


    void StudentAngry()
    {
        studentBalloon.SetActive(true);
        animStudentBalloon.SetBool("Angry", true);

        Invoke("Reset", 1.2f);
    }
    void PlayerExclamation()
    {
        playerBalloon.SetActive(true);
        animPlayerBalloon.SetBool("Exclamation", true);

        Invoke("Reset", 2f);
    }
    void SaleResponds()
    {
        saleBalloon.SetActive(true);
        animSaleBalloon.SetBool("Talk", true);

        Invoke("QuestionShop", 2f);
        Invoke("Reset", 2f);
    }

    void QuestionShop()
    {
        CanStartAfterTalk?.Invoke();
    }
    void Reset()
    {
        animSaleBalloon.SetBool("Talk", false);
        animPlayerBalloon.SetBool("Exclamation", false);
        animStudentBalloon.SetBool("Angry", false);

        animSaleBalloon.SetBool("Reset", true);
        animPlayerBalloon.SetBool("Reset", true);
        animStudentBalloon.SetBool("Reset", true);

        saleBalloon.SetActive(false);
        playerBalloon.SetActive(false);
        studentBalloon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.CompareTag("Shop"))
        {
            enterShop = true;
        }
        if (collision.CompareTag("Talk"))
        {
            enterTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {  
        if (collision.CompareTag("Shop"))
        {
            enterShop = false;
        }
        if (collision.CompareTag("Talk"))
        {
            enterTalk = false;
        }
    }
}
