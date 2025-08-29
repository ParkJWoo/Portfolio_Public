using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OptionCloseBtn()
    {
        GameManager.instance.optionPanel.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.instance.PlayClickSound();
    }

    public void OptionOpenBtn()
    {
        GameManager.instance.optionPanel.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.instance.PlayClickSound();
    }
}
