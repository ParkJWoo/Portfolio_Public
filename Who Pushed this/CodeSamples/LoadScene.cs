using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Animator anim;
    public void LoadMainScene()                                  //  MainScene 호출
    {
        //anim.SetBool("isClick", true);
        SceneManager.LoadScene("MainScene");
        AudioManager.instance.PlayClickSound();
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();
        AudioManager.instance.SwitchMusic(false, false);
    }

    public void LoadStartScene()                                  //  MainScene 호출
    {
        //anim.SetBool("isClick", true);
        SceneManager.LoadScene("StartScene");
        AudioManager.instance.PlayClickSound();
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();
        AudioManager.instance.SwitchMusic(false, false);
    }
}