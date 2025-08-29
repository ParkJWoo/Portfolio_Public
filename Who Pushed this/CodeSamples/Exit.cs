using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    public void GameEnd()
    {
        AudioManager.instance.PlayClickSound();
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();

        AudioManager.instance.isHell = false;
        AudioManager.instance.isInfinity = false;
        AudioManager.instance.SwitchMusic(false, false);

        anim.SetBool("isClick", true);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
