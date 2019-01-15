using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class button0sc : MonoBehaviour
{
    public InputField inputField;

    public void ButtonPush()
    {
        Debug.Log("speed設定");
        double dtmp = double.Parse(inputField.text);
        if (dtmp < 1.0)
            dtmp = 1.0;
        GameObject.Find("sprite0").GetComponent<RK4>().speed = dtmp;
        GameObject.Find("sprite0").GetComponent<RK4>().rspeed = 1.0 / dtmp;
        GameObject.Find("sprite0").GetComponent<RK4dd>().speed = dtmp;
        GameObject.Find("sprite0").GetComponent<RK4dd>().rspeed = 1.0 / dtmp;
    }
}