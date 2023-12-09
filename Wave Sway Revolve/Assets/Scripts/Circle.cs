using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int segments = 50;
    public float radius = 1.0f;
    LineRenderer lineRenderer;
    public bool isActive { get; set; }

    void Start()
    {
        //isActive = false;  //this initialization is taken care of in WaveAnalyzer
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = 0.03f;
        lineRenderer.useWorldSpace = false;  //position of each circle will be set to the center point about which the Orb revolves
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        //float z;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        { 
            //this draws a circle in counterclockwise (CCW) order about the local origin, starting at (radius, 0)
            //Use the sin and cos functions to find the x and y positions of each lineRenderer segment incrementing with the angle
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    //NOTE: Update is not needed; LineRenderer redraws whenever SetPosition() is called
}