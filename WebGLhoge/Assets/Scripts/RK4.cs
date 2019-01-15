using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RK4 : MonoBehaviour
{
    public Texture2D tex;
    Sprite sprite;
    int cnt;
    int WX = 640*2;
    int WY = 480*2;

    public double speed = 1024.0;//距離1のとき時間刻み h=1/1024 になる
    public double rspeed;
    double Tlimit = 0.0000000001;//10^-10。最悪h時間(speed1024のとき)
    public double h;
    public double t = 0.0;
    double lastt=0.0;
    double scale;

    double x1;
    double y1;
    double u1;
    double v1;
    double x2;
    double y2;
    double u2;
    double v2;
    double x3;
    double y3;
    double u3;
    double v3;

    public double m1;
    public double m2;
    public double m3;

    double a1;
    double b1;
    double c1;
    double d1;
    double e1;
    double f1;
    double g1;
    double h1;
    double j1;
    double k1;
    double l1;
    double n1;

    double a2;
    double b2;
    double c2;
    double d2;
    double e2;
    double f2;
    double g2;
    double h2;
    double j2;
    double k2;
    double l2;
    double n2;

    double a3;
    double b3;
    double c3;
    double d3;
    double e3;
    double f3;
    double g3;
    double h3;
    double j3;
    double k3;
    double l3;
    double n3;

    double a4;
    double b4;
    double c4;
    double d4;
    double e4;
    double f4;
    double g4;
    double h4;
    double j4;
    double k4;
    double l4;
    double n4;
    double[] retout;
    
    public int loopcount;
    public int stopflg = 0;//普通は0、stopの時は1
    public int mode;//double精度が選択されているときは1。デフォルトでは1

    Pset pset;

    void Start()
    {
        mode = 1;
        pset =GameObject.Find("sprite0").GetComponent<Pset>();//コンポーネント取得

        //Texture2DからSpriteを作成
        tex = new Texture2D(WX,WY, TextureFormat.RGBA32, false);
        
        sprite = Sprite.Create(
          texture: tex,
          rect: new Rect(0, 0, WX, WY),
          pivot: new Vector2(0.5f, 0.5f)
        );

        //その他変数
        retout = new double[6];
        m1 = 3.0;
        m2 = 4.0;
        m3 = 5.0;
        MyReset();
    }

    //初期値にリセット
    public void MyReset()
    {
        if (mode==1)
            GetComponent<SpriteRenderer>().sprite = sprite;
        for (int j = 0; j < WY; j++)
        {
            for (int i = 0; i < WX; i++)
            {
                tex.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }
        tex.Apply();
        
        t = 0.0;
        lastt = 0.0;
        //m1 = 3.0;
        //m2 = 4.0;
        //m3 = 5.0;
        x1 = m1;
        y1 = m2;
        u1 = 0.0;
        v1 = 0.0;
        x2 = 0.0;
        y2 = 0.0;
        u2 = 0.0;
        v2 = 0.0;
        x3 = m1;
        y3 = 0.0;
        u3 = 0.0;
        v3 = 0.0;
        double meanx = (x1 + x2 + x3) / 3.0;
        double meany = (y1 + y2 + y3) / 3.0;
        x1 -= meanx;
        x2 -= meanx;
        x3 -= meanx;
        y1 -= meany;
        y2 -= meany;
        y3 -= meany;
        scale = 140.0 / (meanx + meany);

        Tlimit = 0.0000000001 / speed * 1000;//speed1024のとき最悪h時間10^-10
        rspeed = 1.0 / speed;
        h = 0.1 / speed;
        loopcount = 0;
    }









    // Update is called once per frame
    void Update()
    {
        if ((stopflg==0)&(mode==1))
            RKrutin();
        cnt++;
    }


    //double精度によるルンゲクッタによる数値亢進
    void RKrutin()
    {
        double startt = t;
        for (int j = 0; j < 2048;j++) {
            for (int i = 0; i < 32; i++)
            {
                fxy(x1, y1, x2, y2, x3, y3, m1, m2, m3, retout);
                a1 = h * u1;
                b1 = h * v1;
                c1 = h * retout[0];
                d1 = h * retout[1];
                e1 = h * u2;
                f1 = h * v2;
                g1 = h * retout[2];
                h1 = h * retout[3];
                j1 = h * u3;
                k1 = h * v3;
                l1 = h * retout[4];
                n1 = h * retout[5];

                fxy(x1 + a1 * 0.5, y1 + b1 * 0.5, x2 + e1 * 0.5, y2 + f1 * 0.5, x3 + j1 * 0.5, y3 + k1 * 0.5, m1, m2, m3, retout);
                a2 = h * (u1 + 0.5 * c1);
                b2 = h * (v1 + 0.5 * d1);
                c2 = h * retout[0];
                d2 = h * retout[1];
                e2 = h * (u2 + g1 * 0.5);
                f2 = h * (v2 + h1 * 0.5);
                g2 = h * retout[2];
                h2 = h * retout[3];
                j2 = h * (u3 + l1 * 0.5);
                k2 = h * (v3 + n1 * 0.5);
                l2 = h * retout[4];
                n2 = h * retout[5];

                fxy(x1 + a2 * 0.5, y1 + b2 * 0.5, x2 + e2 * 0.5, y2 + f2 * 0.5, x3 + j2 * 0.5, y3 + k2 * 0.5, m1, m2, m3, retout);
                a3 = h * (u1 + c2 * 0.5);
                b3 = h * (v1 + d2 * 0.5);
                c3 = h * retout[0];
                d3 = h * retout[1];
                e3 = h * (u2 + g2 * 0.5);
                f3 = h * (v2 + h2 * 0.5);
                g3 = h * retout[2];
                h3 = h * retout[3];
                j3 = h * (u3 + l2 * 0.5);
                k3 = h * (v3 + n2 * 0.5);
                l3 = h * retout[4];
                n3 = h * retout[5];

                fxy(x1 + a3, y1 + b3, x2 + e3, y2 + f3, x3 + j3, y3 + k3, m1, m2, m3, retout);
                a4 = h * (u1 + c3);
                b4 = h * (v1 + d3);
                c4 = h * retout[0];
                d4 = h * retout[1];
                e4 = h * (u2 + g3);
                f4 = h * (v2 + h3);
                g4 = h * retout[2];
                h4 = h * retout[3];
                j4 = h * (u3 + l3);
                k4 = h * (v3 + n3);
                l4 = h * retout[4];
                n4 = h * retout[5];

                x1 += (a1 + 2.0 * a2 + 2.0 * a3 + a4) * 0.166666666666666666666666666666666666666666666666666667;
                y1 += (b1 + 2.0 * b2 + 2.0 * b3 + b4) * 0.166666666666666666666666666666666666666666666666666667;
                u1 += (c1 + 2.0 * c2 + 2.0 * c3 + c4) * 0.166666666666666666666666666666666666666666666666666667;
                v1 += (d1 + 2.0 * d2 + 2.0 * d3 + d4) * 0.166666666666666666666666666666666666666666666666666667;
                x2 += (e1 + 2.0 * e2 + 2.0 * e3 + e4) * 0.166666666666666666666666666666666666666666666666666667;
                y2 += (f1 + 2.0 * f2 + 2.0 * f3 + f4) * 0.166666666666666666666666666666666666666666666666666667;
                u2 += (g1 + 2.0 * g2 + 2.0 * g3 + g4) * 0.166666666666666666666666666666666666666666666666666667;
                v2 += (h1 + 2.0 * h2 + 2.0 * h3 + h4) * 0.166666666666666666666666666666666666666666666666666667;
                x3 += (j1 + 2.0 * j2 + 2.0 * j3 + j4) * 0.166666666666666666666666666666666666666666666666666667;
                y3 += (k1 + 2.0 * k2 + 2.0 * k3 + k4) * 0.166666666666666666666666666666666666666666666666666667;
                u3 += (l1 + 2.0 * l2 + 2.0 * l3 + l4) * 0.166666666666666666666666666666666666666666666666666667;
                v3 += (n1 + 2.0 * n2 + 2.0 * n3 + n4) * 0.166666666666666666666666666666666666666666666666666667;
                


                if (i % 2 == 0) {
                    t += h*2;
                    double r12 = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
                    double r13 = (x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3);
                    double r23 = (x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3);
                    double minw = 99999.9;
                    if (r12 < minw) minw = r12;
                    if (r13 < minw) minw = r13;
                    if (r23 < minw) minw = r23;
                    double minf = Math.Sqrt(minw);

                    if (minf < 0.1)
                    {
                        if (minf < 0.032)
                        {
                            if (minf < 0.01)
                            {
                                if (minf < 0.001)
                                {
                                    h = minw * rspeed * 0.3;//距離が0.001以下の時
                                    if (h < Tlimit) h = Tlimit;
                                }
                                else
                                {
                                    h = minw * rspeed * 0.45;//距離が0.01以下の時
                                }
                            }
                            else
                            {
                                h = minw * rspeed * 0.6;//距離が0.032以下の時
                            }
                        }
                        else
                        {
                            h = minw * rspeed * 0.8;//距離が0.1以下の時
                        }
                    }
                    else
                    {
                        h = minf * rspeed;//距離が0.1以上の時
                    }

                    if (t - lastt > 0.001)//毎ループ描画するわけにもいかないので
                    {
                        lastt = t;
                        pset.PsetTex2D(tex, x1 * scale + 320.0, y1 * scale + 240.0, 90, 255, 90);
                        pset.PsetTex2D(tex, x2 * scale + 320.0, y2 * scale + 240.0, 255, 90, 90);
                        pset.PsetTex2D(tex, x3 * scale + 320.0, y3 * scale + 240.0, 255, 255, 255);
                    }
                }
            }//ループ終わり
            loopcount += 32;
            if (t - startt > 0.08) break;//一定時間すぎていれば
        }//ループ終わり


        tex.Apply();
    }






    //
    void fxy(double x1, double y1, double x2, double y2, double x3, double y3, double m1, double m2, double m3, double[] outd)
    {
        double r12 = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        double W12 = r12 * Math.Sqrt(r12);
        double r13 = (x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3);
        double W13 = r13 * Math.Sqrt(r13);
        double r23 = (x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3);
        double W23 = r23 * Math.Sqrt(r23);
        double x12 = (x1 - x2) / W12;
        double y12 = (y1 - y2) / W12;
        double x13 = (x1 - x3) / W13;
        double y13 = (y1 - y3) / W13;
        double x23 = (x2 - x3) / W23;
        double y23 = (y2 - y3) / W23;
        outd[0] = -x12 * m2 - x13 * m3;//fx(x1,y1,x2,y2,x3,y3,m1,m2,m3);のとき
        outd[1] = -y12 * m2 - y13 * m3;//fy(x1,y1,x2,y2,x3,y3,m1,m2,m3);のとき

        outd[2] = x12 * m1 - x23 * m3;//fx(x2,y2,x1,y1,x3,y3,m2,m1,m3);のとき
        outd[3] = y12 * m1 - y23 * m3;//fy(x2,y2,x1,y1,x3,y3,m2,m1,m3);のとき

        outd[4] = x13 * m1 + x23 * m2;//fx(x3,y3,x1,y1,x2,y2,m3,m1,m2);のとき
        outd[5] = y13 * m1 + y23 * m2;//fy(x3,y3,x1,y1,x2,y2,m3,m1,m2);のとき
    }
}






