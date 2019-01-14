using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class button2sc : MonoBehaviour
{
    public void ButtonPush()
    {
        Debug.Log("ストップ");
        GameObject.Find("sprite0").GetComponent<RK4dd>().stopflg=1;
    }
}