using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageButton : MonoBehaviour
{
    public Animator anim;
    public int cardCount = 4;

    public void NextStageBtn()
    {
        AudioManager.instance.PlayClickSound();
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();

        GameManager.instance.nextPanel.SetActive(false);

        if (GameManager.instance.mode == 1)
        {
            GameManager.instance.time = 60.0f; // 타이머 초기화
        }

        else if (GameManager.instance.mode == 2)
        {
            GameManager.instance.time = 90.0f; // 타이머 초기화
        }

        //GameManager.instance.time = 60.0f; // 타이머 초기화
        Time.timeScale = 1.0f;

        GameManager.instance.stage += 1;
        GameManager.instance.cardCount = cardCount * 2;

        GameManager.instance.isGamePlaying = true;

        Debug.Log(GameManager.instance.stage);
        Debug.Log(GameManager.instance.cardCount);

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.board.Start();

        Debug.Log(GameManager.instance.time);

        GameManager.instance.timeTxt.gameObject.SetActive(true);
    }
}