using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPhysics : MonoBehaviour
{

    public int lineCount = 100;
    public float radius = 10.0f;

    static Material lineMaterial;

    public float x = -10f;
    public float y = 0;

    // Just creates a multicolored line
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    // Will be called after all regular rendering is done
    // is a method always called like update 
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

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
            addByFunction();

        }
        GL.End();
        GL.PopMatrix();
    }

    void OnPostRender()
    {
        // Set your materials
        GL.PushMatrix();
        // yourMaterial.SetPass( );
        // Draw your stuff
        GL.PopMatrix();
    }

    void addByFunction()
    {
        y = y_function(x);


        // want x to look longer but not be long so x*factor 
        Vector3 previous_vertex = new Vector3(x*5, y, 0);
        GL.Vertex3(previous_vertex.x,previous_vertex.y,previous_vertex.z);

        for (float i = 0; i < 3; i=i+0.03f)
        {
            GL.Vertex3(previous_vertex.x, previous_vertex.y, previous_vertex.z);
            // Another vertex at edge of circle

            x = i * 5;
            y = y_function(i);
            GL.Vertex3(i*5 , y , 0);

            previous_vertex = new Vector3(i*5 , y , 0);

        }
        

    }

    float y_function(float x)
    {
        return  (- Mathf.Pow(1 * x, 2) +5*x);
    }


    void Update()
    {
        moveToPointer(x, y);
    }

    // this follows arc but not released 
    // use a measuring object and do a reference translation aka - 6units in gl translating to actual units in the game


    void moveToPointer(float x, float y)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Vector3 move = this.transform.position + new Vector3(x,y,0);

            this.transform.Translate(move * Time.deltaTime);
        }
    }




    // How does the transform movement work 
}