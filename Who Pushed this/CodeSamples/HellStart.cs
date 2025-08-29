using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;
    public float closeSpeed;
    public int stage;
    public int cardCount;

    public MainBg mainBg;

    public void LetHellStart()
    {
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();
        AudioManager.instance.PlayClickSound(true);

        GameManager.instance.stage = 4;
        GameManager.instance.cardCount = cardCount;
        GameManager.instance.mode = 1;

        if (GameManager.instance.stage != 0 && GameManager.instance.mode != 0)
        {
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 60.0f;
            GameManager.instance.isGamePlaying = true;
        }

        if(mainBg != null)               //배경 이벤트
        {
            mainBg.ActivateFade();
        }
    }

    
}
