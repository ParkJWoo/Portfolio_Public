using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Animator easyanim;
    public Animator hardanim;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public int level;

    public GameObject st1Btn;
    public GameObject st2Btn;
    public GameObject st3Btn;

    public GameObject startBtn;

    public void LevelBtn()
    {
        AudioManager.instance.PlayClickSound();
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();
        AudioManager.instance.SwitchMusic(false, false);
        
        st1Btn.SetActive(true);
        st2Btn.SetActive(true);
        st3Btn.SetActive(true);
        startBtn.SetActive(false);


        GameManager.instance.mode = level;
        GameManager.instance.stage = 0;                               //스테이지변수 초기화
        GameManager.instance.cardCount = 0;
        if (level == 1)
        {
            st1Btn.SetActive(true);
            st2Btn.SetActive(true);
            st3Btn.SetActive(true);

            startBtn.SetActive(false);

            easyanim.SetBool("isClick", true);
            hardanim.SetBool("isClick", false);

            anim1.SetBool("isClick", false);               //스테이지 버튼 눌린애니 초기화
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);
        }
        else if (level == 2)
        {
            anim1.SetBool("isClick", false);
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);

            st1Btn.SetActive(false);
            st2Btn.SetActive(false);
            st3Btn.SetActive(false);

            startBtn.SetActive(false);

            easyanim.SetBool("isClick", false);
            if (GameManager.instance.Clear >= 3)       //쉬움 3스테까지 클리어했다면
            {
                hardanim.SetBool("isClick", true);
                startBtn.SetActive(true);
            }
            else                                      //쉬움 3스테까지 클리어를 못했다면
            {
                GameManager.instance.mode = 1;
            }

        }
    }
}
