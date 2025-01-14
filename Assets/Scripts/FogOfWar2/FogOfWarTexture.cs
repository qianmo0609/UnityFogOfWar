using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FogOfWarTexture
{
    Texture2D fogTxtur;
    Color[] pixelColors;

    int width;
    int height;
    float shadowRadius;

    public FogOfWarTexture(int width,int height,float rad)
    {
        this.width = width;
        this.height = height;
        this.shadowRadius = rad;
        fogTxtur = new Texture2D(width,height);
        pixelColors = new Color[width * height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                pixelColors[i * height + j] = Color.black;
            }
        }
        this.SetPixels(pixelColors);
    }

    void SetPixels(Color[] colors)
    {
        fogTxtur.SetPixels(pixelColors);
        fogTxtur.Apply();
    }

    public void HandlerTetureColors(Vector3 point)
    {
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.height; j++)
            {
                if(Mathf.Abs(j - this.width/2 + point.x) < this.shadowRadius && Mathf.Abs(i- this.height/2 + point.z) < this.shadowRadius && pixelColors[i * this.height + j] == Color.black)
                {
                    pixelColors[i * this.height + j] = new Color(0,0,0,0);
                }
            }
        }
        this.SetPixels(pixelColors);
    }

    public Texture2D GetFogTexture()
    {
        return this.fogTxtur;
    }
}
