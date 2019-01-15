using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    //HSPのpset命令みたいなもん
    public void PsetTex2D(Texture2D tex,double x, double y, int r, int g, int b)
    {
        int ix = (int)(x * 2.0);
        int iy = (int)(y * 2.0);
        if (ix >= 0 && ix < tex.width && iy >= 0 && iy < tex.height)
        {
            tex.SetPixel(ix, iy, new Color(1.0f * r / 255.0f, 1.0f * g / 255.0f, 1.0f * b / 255.0f, 1.0f));
        }
    }


}
