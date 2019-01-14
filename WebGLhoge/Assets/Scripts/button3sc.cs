using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class button3sc : MonoBehaviour
{
    public void ButtonPush()
    {
        Debug.Log("リセット");
        GameObject.Find("sprite0").GetComponent<RK4dd>().MyReset();
    }
}