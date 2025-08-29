using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtn : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
