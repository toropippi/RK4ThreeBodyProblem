using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownCallback : MonoBehaviour
{
    double[] pitagora;
    private void Start()
    {
        pitagora = new double[] { 3, 4, 5, 5, 12, 13, 7, 24, 25, 8, 15, 17, 9, 40, 41, 11, 60, 61, 12, 35, 37, 13, 84, 85, 15, 112, 113, 16, 63, 65, 17, 144, 145, 19, 180, 181, 20, 21, 29, 20, 99, 101, 24, 143, 145, 28, 45, 53, 28, 195, 197, 33, 56, 65, 36, 77, 85, 39, 80, 89, 44, 117, 125, 48, 55, 73, 51, 140, 149, 52, 165, 173, 57, 176, 185, 60, 91, 109, 65, 72, 97, 85, 132, 157, 88, 105, 137, 95, 168, 193, 104, 153, 185, 119, 120, 169 };
    }
    public void OnValueChanged(int result)
    {
        RK4 rk4 = GameObject.Find("sprite0").GetComponent<RK4>();
        rk4.m1 = pitagora[result * 3];
        rk4.m2 = pitagora[result * 3 + 1];
        rk4.m3 = pitagora[result * 3 + 2];
        rk4.MyReset();

        RK4dd rk4dd = GameObject.Find("sprite0").GetComponent<RK4dd>();
        rk4dd.n_m1 = pitagora[result * 3];
        rk4dd.n_m2 = pitagora[result * 3 + 1];
        rk4dd.n_m3 = pitagora[result * 3 + 2];
        rk4dd.MyReset();
        // 処理
    }
}