using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;

    //public float localTime { get; private set; } = 60.0f;

    public void LetGameStart()
    {
        AudioManager.instance.PlayClickSound(true);
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();
        AudioManager.instance.PlayMusic();

        if (GameManager.instance.mode == 1)
        {
            if (GameManager.instance.stage != 0)
            {
                anim.SetBool("isClick", true);
                levelPanel.SetActive(false);
                GameManager.instance.board.gameObject.SetActive(true);
                GameManager.instance.timeTxt.gameObject.SetActive(true);

                GameManager.instance.time = 60.0f; // 타이머 초기화
                GameManager.instance.isGamePlaying = true; // 게임 시작상태
            }
            
        }
        else if (GameManager.instance.mode == 2)
        {
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.stage = 3;
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 90.0f; // 타이머 초기화
            GameManager.instance.isGamePlaying = true; // 게임 시작상태
        }
    }
}