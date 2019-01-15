using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniCounter : MonoBehaviour
{
    private int screenLongSide;
    private Rect boxRect;
    private GUIStyle style = new GUIStyle();
    RK4 rk4;
    RK4dd rk4dd;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UpdateUISize();
    }

    private void Start()
    {
        rk4 = GameObject.Find("sprite0").GetComponent<RK4>();
        rk4dd = GameObject.Find("sprite0").GetComponent<RK4dd>();
        screenLongSide = 0;
    }

    private void Update()
    {
        // Update the UI size if the resolution has changed
        if (screenLongSide != Mathf.Max(Screen.width, Screen.height))
        {
            UpdateUISize();
        }
    }
    
    private void UpdateUISize()
    {
        screenLongSide = Mathf.Max(Screen.width, Screen.height);
        var rectLongSide = screenLongSide / 10;
        boxRect = new Rect(rectLongSide/2, rectLongSide, rectLongSide, rectLongSide / 3);
        style.fontSize = (int)(screenLongSide / 56.0);
        style.normal.textColor = Color.white;
    }
    
    private void OnGUI()
    {
        GUI.Box(boxRect, "");
        if (rk4.mode == 1)//倍精度が選択されている状況なら
        {
            GUI.Label(boxRect, "Time=" + rk4.t.ToString("f4") + "\nloopcount=" + rk4.loopcount + "\nStandard 1/h=" + rk4.speed + "\nh=" + rk4.h + "", style);
        }

        if (rk4dd.mode == 1)//疑似４倍精度が選択されている状況なら
        {
            GUI.Label(boxRect, "Time=" + rk4dd.t.ToString("f4") + "\nloopcount=" + rk4dd.loopcount + "\nStandard 1/h=" + rk4dd.speed + "\nh=" + rk4dd.h + "", style);
        }
    }
}