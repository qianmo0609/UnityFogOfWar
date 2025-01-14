using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMask : MonoBehaviour
{
    public GameObject shadowPlane;
    public Transform player;
    public LayerMask shadowMaskLayer;
    public float shadowRadius = 10f;

    private float radiusCircle { get {  return shadowRadius * shadowRadius; } }

    private Mesh mesh;
    private Vector3[] verteies; // 顶点坐标位置
    private Color[] verteiesColors; //顶点坐标的颜色

    void Start()
    {
        this.Initialize();
    }

    void Update()
    {
        Ray r = new Ray(player.position, player.position + Vector3.up * 100);
        Debug.DrawLine(player.position, player.position + Vector3.up * 100);
        RaycastHit hit;
        if (Physics.Raycast(r,out hit,1000,shadowMaskLayer,QueryTriggerInteraction.Collide))
        {
            Debug.Log(hit);
            for (int i = 0; i < verteies.Length; i++)
            {
                //将迷雾平面的顶点坐标变换到世界空间
                Vector3 v = shadowPlane.transform.TransformPoint(verteies[i]);
                //计算变换到世界空间下的顶点坐标与碰撞点的距离
                float distance = Vector3.SqrMagnitude(v - hit.point);
                if(distance < radiusCircle)
                {
                    float alpha = Mathf.Min(verteiesColors[i].a,distance/radiusCircle);
                    verteiesColors[i].a = alpha;
                }
            }
        }

        UpdateVerteiesColors();
    }

    void UpdateVerteiesColors()
    {
        //更换顶点颜色
        mesh.colors = verteiesColors;
    }

    void Initialize()
    {
        mesh = shadowPlane.GetComponent<MeshFilter>().mesh;
        verteies = mesh.vertices;
        verteiesColors = new Color[verteies.Length];
        for (int i = 0; i < verteies.Length; i++)
        {
            verteiesColors[i] = Color.black;
        }
        UpdateVerteiesColors();
    }
}
