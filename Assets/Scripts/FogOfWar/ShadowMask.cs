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
    private Vector3[] verteies; // ��������λ��
    private Color[] verteiesColors; //�����������ɫ

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
                //������ƽ��Ķ�������任������ռ�
                Vector3 v = shadowPlane.transform.TransformPoint(verteies[i]);
                //����任������ռ��µĶ�����������ײ��ľ���
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
        //����������ɫ
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
