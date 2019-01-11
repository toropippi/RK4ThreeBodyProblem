using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RK4 : MonoBehaviour
{
    public Texture2D tex;
    Sprite sprite;
    int cnt;
    int WX = 640;
    int WY = 480;

    double speed = 256.0;
    double Tlimit = 0.0000000001;
    double h = 0.001;
    double t = 0.0;
    double x1 = 3.0-1.5;
    double y1 = 4.0-1.5;
    double u1 = 0.0;
    double v1 = 0.0;
    double x2 = 0.0-1.5;
    double y2 = 0.0-1.5;
    double u2 = 0.0;
    double v2 = 0.0;
    double x3 = 3.0-1.5;
    double y3 = 0.0-1.5;
    double u3 = 0.0;
    double v3 = 0.0;
    double m1 = 3.0;
    double m2 = 4.0;
    double m3 = 5.0;
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
    //Random.Range(0, 60) + Const.CO.WX;
    void Start()
    {
        tex = new Texture2D(WX,WY, TextureFormat.RGBA32, false);
        //Texture2DからSpriteを作成
        for (int j = 0; j < WY; j++)
        {
            for (int i = 0; i < WX; i++)
            {
                tex.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }
        tex.Apply();


        sprite = Sprite.Create(
          texture: tex,
          rect: new Rect(0, 0, WX, WY),
          pivot: new Vector2(0.5f, 0.5f)
        );
        GetComponent<SpriteRenderer>().sprite = sprite;


        speed = 256.0;
        Tlimit = 0.0000000001;
        h = 0.001;
        t = 0.0;
        x1 = 3.0 - 1.5;
        y1 = 4.0 - 1.5;
        u1 = 0.0;
        v1 = 0.0;
        x2 = 0.0 - 1.5;
        y2 = 0.0 - 1.5;
        u2 = 0.0;
        v2 = 0.0;
        x3 = 3.0 - 1.5;
        y3 = 0.0 - 1.5;
        u3 = 0.0;
        v3 = 0.0;
        m1 = 3.0;
        m2 = 4.0;
        m3 = 5.0;
        retout = new double[6];
    }

    // Update is called once per frame
    void Update()
    {
        RKrutin();
        //transform.position = transform.position - new Vector3(0.55f, 0.0f, 0.0f);
        cnt++;
    }


    //るんげくったによる数値亢進
    void RKrutin()
    {
        for (int j = 0; j < 64;j++) {
            for (int i = 0; i < 256; i++)
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

                x1 += h * (a1 + 2.0 * a2 + 2.0 * a3 + a4) / 6.0;
                y1 += h * (b1 + 2.0 * b2 + 2.0 * b3 + b4) / 6.0;
                u1 += h * (c1 + 2.0 * c2 + 2.0 * c3 + c4) / 6.0;
                v1 += h * (d1 + 2.0 * d2 + 2.0 * d3 + d4) / 6.0;
                x2 += h * (e1 + 2.0 * e2 + 2.0 * e3 + e4) / 6.0;
                y2 += h * (f1 + 2.0 * f2 + 2.0 * f3 + f4) / 6.0;
                u2 += h * (g1 + 2.0 * g2 + 2.0 * g3 + g4) / 6.0;
                v2 += h * (h1 + 2.0 * h2 + 2.0 * h3 + h4) / 6.0;
                x3 += h * (j1 + 2.0 * j2 + 2.0 * j3 + j4) / 6.0;
                y3 += h * (k1 + 2.0 * k2 + 2.0 * k3 + k4) / 6.0;
                u3 += h * (l1 + 2.0 * l2 + 2.0 * l3 + l4) / 6.0;
                v3 += h * (n1 + 2.0 * n2 + 2.0 * n3 + n4) / 6.0;
                t += h;


                if (i % 16 == 0) {
                    double r12 = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
                    double r13 = (x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3);
                    double r23 = (x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3);
                    double minw = 99999.9;
                    if (r12 < minw) minw = r12;
                    if (r13 < minw) minw = r13;
                    if (r23 < minw) minw = r23;
                    double minf = Math.Sqrt(minw);
                    h = minf / speed;
                    if (minf < 0.1) h = minw / speed / 2.0;
                    if (minf < 0.01) h = minw / speed / 8.0;
                    if (h < Tlimit) h = Tlimit;
                }
            }

            Pset(x1 * 70.0 + 320.0, y1 * 70.0 + 240.0, 90, 255, 90);
            Pset(x2 * 70.0 + 320.0, y2 * 70.0 + 240.0, 255, 90, 90);
            Pset(x3 * 70.0 + 320.0, y3 * 70.0 + 240.0, 255, 255, 255);
            //ループ終わり
        }




        //if cnt\1024 == 0:await 0
        //ここでpset
        //if cnt\4096 == 0{
        //color 90,255,90
        tex.Apply();
        if (cnt % 16 == 0)
        {
            Debug.Log("cnt=" + cnt + "/  h*100000=" + h * 100000.0 + " t=" + t + "  u1v1="+u1+"  "+v1+"  y1="+y1+"");
        }

    }



    //HSPのPSET
    void Pset(double x, double y,int r,int g,int b)
    {
        int ix = Mathf.Clamp((int)x, 0, WX - 1);
        int iy = Mathf.Clamp((int)y, 0, WY - 1);
        tex.SetPixel(ix, iy, new Color(1.0f*r/255.0f, 1.0f * g / 255.0f, 1.0f * b / 255.0f, 1.0f));
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









