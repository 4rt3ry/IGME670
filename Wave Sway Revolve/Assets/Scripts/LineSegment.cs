using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment : MonoBehaviour
{
    LineRenderer lineRenderer;
    public bool isActive { get; set; }

    void Start()
    {
        isActive = false;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.useWorldSpace = false;
        SetPoints(Vector3.zero, Vector3.zero);
    }

    public void SetPoints(Vector3 beginPoint, Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, beginPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    //NOTE: Update is not needed; LineRenderer redraws whenever SetPosition() is called
  
}
