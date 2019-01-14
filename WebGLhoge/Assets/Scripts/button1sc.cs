using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class button1sc : MonoBehaviour
{
    public void ButtonPush()
    {
        Debug.Log("再生");
        GameObject.Find("sprite0").GetComponent<RK4dd>().stopflg = 0;
    }
}