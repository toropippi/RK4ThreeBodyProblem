using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RK4dd : MonoBehaviour
{
    public struct dd//double double
    {
        public double hi;//indexでいうと0
        public double lo;//indexでいうと1
        public dd(double a, double b) { hi = a; lo = b; }
        /*
        public static dd operator +(dd z, dd w)
        {
            return new dd(z.hi + w.hi, z.lo + w.lo);
        }
        public static dd operator -(dd z, dd w)
        {
            return new dd(z.hi - w.hi, z.lo - w.lo);
        }
        */
        public static dd operator -(dd z)
        {
            return new dd(-z.hi, -z.lo);
        }
        public static dd operator *(dd z, double w)
        {
            return new dd(z.hi * w, z.lo * w);
        }
        public static dd operator *(double w,dd z)
        {
            return new dd(z.hi * w, z.lo * w);
        }
    };

    public Texture2D tex;
    Sprite sprite;
    int cnt;
    int WX = 640 * 2;
    int WY = 480 * 2;

    public double speed = 1024.0;//初期値1024でok
    double rspeed;
    double Tlimit = 0.0000000001;//
    public double h;
    public double t = 0.0;
    double lastt = 0.0;
    double scale;
    dd x1;
    dd y1;
    dd u1;
    dd v1;
    dd x2;
    dd y2;
    dd u2;
    dd v2;
    dd x3;
    dd y3;
    dd u3;
    dd v3;
    dd m1;
    dd m2;
    dd m3;
    double n_m1;
    double n_m2;
    double n_m3;

    dd a1;
    dd b1;
    dd c1;
    dd d1;
    dd e1;
    dd f1;
    dd g1;
    dd h1;
    dd j1;
    dd k1;
    dd l1;
    dd n1;

    dd a2;
    dd b2;
    dd c2;
    dd d2;
    dd e2;
    dd f2;
    dd g2;
    dd h2;
    dd j2;
    dd k2;
    dd l2;
    dd n2;

    dd a3;
    dd b3;
    dd c3;
    dd d3;
    dd e3;
    dd f3;
    dd g3;
    dd h3;
    dd j3;
    dd k3;
    dd l3;
    dd n3;

    dd a4;
    dd b4;
    dd c4;
    dd d4;
    dd e4;
    dd f4;
    dd g4;
    dd h4;
    dd j4;
    dd k4;
    dd l4;
    dd n4;

    dd[] retout;
    dd rvse6;

    public int loopcount;
    public int stopflg = 0;//普通は0、stopの時は1

    void Start()
    {
        //Texture2DからSpriteを作成
        tex = new Texture2D(WX, WY, TextureFormat.RGBA32, false);

        sprite = Sprite.Create(
          texture: tex,
          rect: new Rect(0, 0, WX, WY),
          pivot: new Vector2(0.5f, 0.5f)
        );
        GetComponent<SpriteRenderer>().sprite = sprite;
        retout = new dd[6];
        rvse6 = dd_div(new dd(1.0, 0.0), new dd(6.0, 0.0));
        MyReset();
    }

    //初期値にリセット
    public void MyReset()
    {
        for (int j = 0; j < WY; j++)
        {
            for (int i = 0; i < WX; i++)
            {
                tex.SetPixel(i, j, new Color(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }

        Tlimit = 0.000000000001;
        t = 0.0;
        lastt = 0.0;
        n_m1 = 3.0;
        n_m2 = 4.0;
        n_m3 = 5.0;
        x1 = new dd(n_m1, 0.0);
        y1 = new dd(n_m2, 0.0);
        u1 = new dd(0.0, 0.0);
        v1 = new dd(0.0, 0.0);
        x2 = new dd(0.0, 0.0);
        y2 = new dd(0.0, 0.0);
        u2 = new dd(0.0, 0.0);
        v2 = new dd(0.0, 0.0);
        x3 = new dd(n_m1, 0.0); 
        y3 = new dd(0.0, 0.0);
        u3 = new dd(0.0, 0.0);
        v3 = new dd(0.0, 0.0);
        m1 = new dd(n_m1, 0.0);
        m2 = new dd(n_m2, 0.0);
        m3 = new dd(n_m3, 0.0);

        double meanx = (x1.hi + x2.hi + x3.hi) / 3.0;
        double meany = (y1.hi + y2.hi + y3.hi) / 3.0;
        x1.hi -= meanx;
        x2.hi -= meanx;
        x3.hi -= meanx;
        y1.hi -= meany;
        y2.hi -= meany;
        y3.hi -= meany;
        scale = 140.0 / (meanx + meany);


        rspeed = 1.0 / speed;
        h = 0.1 / speed;
        loopcount = 0;

        tex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopflg == 0)
            RKrutin();
        cnt++;
    }


    //普通のdouble精度によるルンゲクッタによる数値亢進
    void RKrutin()
    {
        double startt = t;
        for (int j = 0; j < 2048; j++)
        {
            for (int i = 0; i < 32; i++)
            {
                ddfxy(
                    x1, y1,
                    x2, y2,
                    x3, y3,
                    n_m1, n_m2, n_m3, retout);
                a1 = dn_mul(u1, h);
                b1 = dn_mul(v1, h);
                c1 = dn_mul(retout[0], h);
                d1 = dn_mul(retout[1], h);
                e1 = dn_mul(u2, h);
                f1 = dn_mul(v2, h);
                g1 = dn_mul(retout[2], h);
                h1 = dn_mul(retout[3], h);
                j1 = dn_mul(u3, h);
                k1 = dn_mul(v3, h);
                l1 = dn_mul(retout[4], h);
                n1 = dn_mul(retout[5], h);

                ddfxy(
                    dd_add(x1, a1 * 0.5), dd_add(y1, b1 * 0.5),
                    dd_add(x2, e1 * 0.5), dd_add(y2, f1 * 0.5),
                    dd_add(x3, j1 * 0.5), dd_add(y3, k1 * 0.5),
                    n_m1, n_m2, n_m3, retout);
                a2 = dn_mul(dd_add(u1 , c1* 0.5), h);
                b2 = dn_mul(dd_add(v1 , 0.5 * d1), h);
                c2 = dn_mul(retout[0], h);
                d2 = dn_mul(retout[1], h);
                e2 = dn_mul(dd_add(u2 , g1 * 0.5), h);
                f2 = dn_mul(dd_add(v2 , h1 * 0.5), h);
                g2 = dn_mul(retout[2], h);
                h2 = dn_mul(retout[3], h);
                j2 = dn_mul(dd_add(u3 , l1 * 0.5), h);
                k2 = dn_mul(dd_add(v3 , n1 * 0.5), h);
                l2 = dn_mul(retout[4], h);
                n2 = dn_mul(retout[5], h);

                ddfxy(
                    dd_add(x1, a2 * 0.5), dd_add(y1, b2 * 0.5),
                    dd_add(x2, e2 * 0.5), dd_add(y2, f2 * 0.5),
                    dd_add(x3, j2 * 0.5), dd_add(y3, k2 * 0.5),
                    n_m1, n_m2, n_m3, retout);
                a3 = dn_mul(dd_add(u1 , c2 * 0.5), h);
                b3 = dn_mul(dd_add(v1 , d2 * 0.5), h);
                c3 = dn_mul(retout[0], h);
                d3 = dn_mul(retout[1], h);
                e3 = dn_mul(dd_add(u2 , g2 * 0.5), h);
                f3 = dn_mul(dd_add(v2 , h2 * 0.5), h);
                g3 = dn_mul(retout[2], h);
                h3 = dn_mul(retout[3], h);
                j3 = dn_mul(dd_add(u3 , l2 * 0.5), h);
                k3 = dn_mul(dd_add(v3 , n2 * 0.5), h);
                l3 = dn_mul(retout[4], h);
                n3 = dn_mul(retout[5], h);

                ddfxy(
                    dd_add(x1, a3), dd_add(y1, b3),
                    dd_add(x2, e3), dd_add(y2, f3),
                    dd_add(x3, j3), dd_add(y3, k3),
                    n_m1, n_m2, n_m3, retout);
                a4 = dn_mul(dd_add(u1 , c3), h);
                b4 = dn_mul(dd_add(v1 , d3), h);
                c4 = dn_mul(retout[0], h);
                d4 = dn_mul(retout[1], h);
                e4 = dn_mul(dd_add(u2 , g3), h);
                f4 = dn_mul(dd_add(v2 , h3), h);
                g4 = dn_mul(retout[2], h);
                h4 = dn_mul(retout[3], h);
                j4 = dn_mul(dd_add(u3 , l3), h);
                k4 = dn_mul(dd_add(v3 , n3), h);
                l4 = dn_mul(retout[4], h);
                n4 = dn_mul(retout[5], h);

                //ここが
                //x1 += (a1 + 2.0 * a2 + 2.0 * a3 + a4)/6.0;
                //にあたる
                //かろうじてルンゲクッタの式っぽい名残がみえるところ
                x1 = dd_add(x1, dd_mul(dd_add(dd_add(a1, a4), 2.0 * dd_add(a2, a3)), rvse6));
                y1 = dd_add(y1, dd_mul(dd_add(dd_add(b1, b4), 2.0 * dd_add(b2, b3)), rvse6));
                u1 = dd_add(u1, dd_mul(dd_add(dd_add(c1, c4), 2.0 * dd_add(c2, c3)), rvse6));
                v1 = dd_add(v1, dd_mul(dd_add(dd_add(d1, d4), 2.0 * dd_add(d2, d3)), rvse6));
                x2 = dd_add(x2, dd_mul(dd_add(dd_add(e1, e4), 2.0 * dd_add(e2, e3)), rvse6));
                y2 = dd_add(y2, dd_mul(dd_add(dd_add(f1, f4), 2.0 * dd_add(f2, f3)), rvse6));
                u2 = dd_add(u2, dd_mul(dd_add(dd_add(g1, g4), 2.0 * dd_add(g2, g3)), rvse6));
                v2 = dd_add(v2, dd_mul(dd_add(dd_add(h1, h4), 2.0 * dd_add(h2, h3)), rvse6));
                x3 = dd_add(x3, dd_mul(dd_add(dd_add(j1, j4), 2.0 * dd_add(j2, j3)), rvse6));
                y3 = dd_add(y3, dd_mul(dd_add(dd_add(k1, k4), 2.0 * dd_add(k2, k3)), rvse6));
                u3 = dd_add(u3, dd_mul(dd_add(dd_add(l1, l4), 2.0 * dd_add(l2, l3)), rvse6));
                v3 = dd_add(v3, dd_mul(dd_add(dd_add(n1, n4), 2.0 * dd_add(n2, n3)), rvse6));



                if (i % 2 == 0)
                {
                    t += h * 2;
                    double r12 = (x1.hi - x2.hi) * (x1.hi - x2.hi) + (y1.hi - y2.hi) * (y1.hi - y2.hi);
                    double r13 = (x1.hi - x3.hi) * (x1.hi - x3.hi) + (y1.hi - y3.hi) * (y1.hi - y3.hi);
                    double r23 = (x2.hi - x3.hi) * (x2.hi - x3.hi) + (y2.hi - y3.hi) * (y2.hi - y3.hi);
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
                                    h = minw * rspeed * 0.3;
                                    if (h < Tlimit) h = Tlimit;
                                }
                                else
                                {
                                    h = minw * rspeed * 0.45;
                                }
                            }
                            else
                            {
                                h = minw * rspeed * 0.6;
                            }
                        }
                        else
                        {
                            h = minw * rspeed * 0.8;
                        }
                    }
                    else
                    {
                        h = minf * rspeed;
                    }

                    if (t - lastt > 0.001)
                    {
                        lastt = t;
                        Pset(x1.hi * scale + 320.0, y1.hi * scale + 240.0, 90, 255, 90);
                        Pset(x2.hi * scale + 320.0, y2.hi * scale + 240.0, 255, 90, 90);
                        Pset(x3.hi * scale + 320.0, y3.hi * scale + 240.0, 255, 255, 255);
                    }
                }
            }//ループ終わり
            loopcount += 32;
            if (t - startt > 0.08) break;//一定時間すぎていれば
        }//ループ終わり




        //if cnt\1024 == 0:await 0
        //ここでpset
        //if cnt\4096 == 0{
        //color 90,255,90
        tex.Apply();

    }






    //HSPのpset命令みたいなもん
    void Pset(double x, double y, int r, int g, int b)
    {
        int ix = (int)(x * 2.0);
        int iy = (int)(y * 2.0);
        if (ix >= 0 && ix < WX && iy >= 0 && iy < WY)
        {
            tex.SetPixel(ix, iy, new Color(1.0f * r / 255.0f, 1.0f * g / 255.0f, 1.0f * b / 255.0f, 1.0f));
        }
    }

    //ちゃんとやるなら質量mもdd精度ししないといけない
    void ddfxy(dd x1, dd y1, dd x2, dd y2, dd x3, dd y3, double m1, double m2, double m3, dd[] outd)
    {
        dd xsub12 = dd_sub(x1, x2);
        dd xsub13 = dd_sub(x1, x3);
        dd xsub23 = dd_sub(x2, x3);
        dd ysub12 = dd_sub(y1, y2);
        dd ysub13 = dd_sub(y1, y3);
        dd ysub23 = dd_sub(y2, y3);
        dd r12 = dd_add(dd_pow2(xsub12), dd_pow2(ysub12));
        dd r13 = dd_add(dd_pow2(xsub13), dd_pow2(ysub13));
        dd r23 = dd_add(dd_pow2(xsub23), dd_pow2(ysub23));
        dd W12 = dd_mul(r12 , dd_sqrt(r12));
        dd W13 = dd_mul(r13 , dd_sqrt(r13));
        dd W23 = dd_mul(r23 , dd_sqrt(r23));
        dd x12 = dd_div(xsub12 , W12);
        dd y12 = dd_div(ysub12 , W12);
        dd x13 = dd_div(xsub13 , W13);
        dd y13 = dd_div(ysub13 , W13);
        dd x23 = dd_div(xsub23 , W23);
        dd y23 = dd_div(ysub23 , W23);
        outd[0] = -dd_add(dn_mul(x12, m2), dn_mul(x13, m3));
        outd[1] = -dd_add(dn_mul(y12, m2), dn_mul(y13, m3));

        outd[2] = dd_sub(dn_mul(x12, m1), dn_mul(x23, m3));
        outd[3] = dd_sub(dn_mul(y12, m1), dn_mul(y23, m3));

        outd[4] = dd_add(dn_mul(x13, m1), dn_mul(x23, m2));
        outd[5] = dd_add(dn_mul(y13, m1), dn_mul(y23, m2));
    }





























































    dd twosum(dd a)
    {
        double x = a.hi + a.lo;
        double tmp = x - a.hi;
        double y = (a.hi - (x - tmp)) + (a.lo - tmp);
        return new dd(x, y);
    }

    dd dsplit(double a)
    {
        double tmp = a * 134217729.0;//2^27+1
        double x = tmp - (tmp - a);
        double y = a - x;
        return new dd(x, y);
    }

    dd twoproduct(dd a)
    {
        double x = a.hi * a.lo;
        dd ca = dsplit(a.hi);
        dd cb = dsplit(a.lo);
        double y = (((ca.hi * cb.hi - x) + ca.lo * cb.hi) + ca.hi * cb.lo) + ca.lo * cb.lo;
        return new dd(x, y);
    }

    dd dd_add(dd x,dd y)
    {
        dd cz = twosum(new dd(x.hi, y.hi));
        cz.lo = cz.lo + x.lo + y.lo;
        return twosum(cz);
    }

    dd dd_sub(dd x, dd y)
    {
        dd cz = twosum(new dd(x.hi, -y.hi));
        cz.lo = cz.lo + x.lo - y.lo;
        return twosum(cz);
    }
    

    dd dd_mul(dd x,dd y)
    {
        dd cz = twoproduct(new dd(x.hi, y.hi));
        cz.lo = cz.lo + x.hi * y.lo + x.lo * y.hi + x.lo * y.lo;
        return twosum(cz);
    }

    dd dd_pow2(dd x)
    {
        double xx = x.hi * x.hi;
        dd ca = dsplit(x.hi);
        double s1 = ca.lo * ca.hi;
        double y = ((ca.hi * ca.hi - xx) + s1 + s1) + ca.lo * ca.lo;
        s1 = x.hi * x.lo;
        y = y + (s1 + s1);// + x.lo * x.loははずしている
        return twosum(new dd(xx,y));
    }

    //dd精度のxとdouble精度のyを乗算するとき、やや高速化を行う
    //nはnormalのdoubleという意味。d=dd精度、n=double精度、f=float精度、h=half精度という感じに定義したい
    dd dn_mul(dd x, double y)
    {
        dd cz = twoproduct(new dd(x.hi, y));
        cz.lo = cz.lo + x.lo * y;
        return twosum(cz);
    }

    dd dd_div(dd x,dd y)
    {
        double z1 = x.hi / y.hi;
        dd cz = twoproduct(new dd(-z1, y.hi));
        double z2 = ((((cz.hi + x.hi) - z1 * y.lo) + x.lo) + cz.lo) / y.hi;
        return twosum(new dd(z1, z2));
    }

    dd dd_sqrt(dd x)
    {
        if ((x.hi == 0.0) & (x.lo == 0.0))
        {
            return new dd(0, 0);
        }
        else
        {
            double z1 = Math.Sqrt(x.hi);
            dd cz = twoproduct(new dd(-z1, z1));
            double z2 = ((cz.hi + x.hi) + x.lo + cz.lo) / (2.0 * z1);
            return twosum(new dd(z1, z2));
        }
    }













}






