using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMask2 : MonoBehaviour
{
    public GameObject shadowPlane;
    public Transform player;
    public LayerMask shadowMaskLayer;
    public float shadowRadius = 10f;
    public int width = 100;
    public int height = 100;

    FogOfWarTexture fogTexture;

    void Start()
    {
        this.Initialize();
    }

    void Update()
    {
        Ray r = new Ray(player.position, player.position + Vector3.up * 100);
        Debug.DrawLine(player.position, player.position + Vector3.up * 100);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, shadowMaskLayer, QueryTriggerInteraction.Collide))
        {
            Debug.Log(hit);
            fogTexture.HandlerTetureColors(hit.point); 
        }

    }

    void Initialize()
    {
        fogTexture = new FogOfWarTexture(this.width,this.height, this.shadowRadius);
        shadowPlane.GetComponent<MeshRenderer>().material.mainTexture = fogTexture.GetFogTexture();
    }

    void SaveTex(Texture2D texture)
    {
        byte[] b = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath+"/t.png",b);
    }
}