using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownCallback2 : MonoBehaviour
{
    public void OnValueChanged2(int result)
    {
        GameObject.Find("sprite0").GetComponent<RK4>().mode= 1-result;//0番目==doubleを選択、modeが1になる
        GameObject.Find("sprite0").GetComponent<RK4dd>().mode = result;//1番目==double-doubleを選択、modeが1になる
        GameObject.Find("sprite0").GetComponent<RK4>().MyReset();
        GameObject.Find("sprite0").GetComponent<RK4dd>().MyReset();
    }
}