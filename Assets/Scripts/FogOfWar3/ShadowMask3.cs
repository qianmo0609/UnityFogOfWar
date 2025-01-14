using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShadowMask3 : MonoBehaviour
{
    public Material mat;
    public Material fogMat;

    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 100;
    public float radius = 3.0f;

    public void OnRenderObject()
    {
        this.OnRenderObj2();
    }

    void onRenderObj1()
    {
        // Apply the line material
        mat.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        for (int i = 0; i < lineCount; ++i)
        {
            float a = i / (float)lineCount;
            float angle = a * Mathf.PI * 2;
            // Vertex colors change from red to green
            GL.Color(new Color(a, 1 - a, 0, 0.8F));
            // One vertex at transform position
            GL.Vertex3(0, 0, 0);
            // Another vertex at edge of circle
            GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        }
        GL.End();
        GL.PopMatrix();
    }

    void OnRenderObj2()
    {
        mat.SetPass(0);
        GL.PushMatrix();
        
        /*GL.Begin(GL.QUADS);
        DrawRect();
        GL.End();*/

        //Draw circles
        fogMat.SetPass(0);
        GL.Begin(GL.QUADS);
        //Draw
        DrawCircle(new Vector2(1, 1), new Vector2(1,1));

        GL.End();
        GL.PopMatrix();
    }

    //pos should be between 0 and 1
    private void DrawCircle(Vector2 pos, Vector2 radius)
    {
        GL.TexCoord3(0.0f, 0.0f, 0f);
        GL.Vertex3(pos.x - radius.x, pos.y - radius.y, 0f);

        GL.TexCoord3(0f, 1f, 0f);
        GL.Vertex3(pos.x - radius.x, pos.y + radius.y, 0f);

        GL.TexCoord3(1f, 1f, 0f);
        GL.Vertex3(pos.x + radius.x, pos.y + radius.y, 0f);

        GL.TexCoord3(1f, 0f, 0f);
        GL.Vertex3(pos.x + radius.x, pos.y - radius.y, 0f);
    }

    private void DrawRect()
    {
        GL.TexCoord3(0.0f, 0.0f, 0f);
        GL.Vertex3(0f, 0f, 0f);

        GL.TexCoord3(0f, 1f, 0f);
        GL.Vertex3(0f, 1f, 0f);

        GL.TexCoord3(1f, 1f, 0f);
        GL.Vertex3(1f, 1f, 0f);

        GL.TexCoord3(1f, 0f, 0f);
        GL.Vertex3(1f, 0f, 0f);
    }
}
