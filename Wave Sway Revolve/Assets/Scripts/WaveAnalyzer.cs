using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnalyzer : MonoBehaviour
{
    int numberRows = 10;
    int numberColumns = 12;

    //int distance = 2; //distance from one ball's center to a neighboring ball's center, the same here in both horizontal and vertical directions 
    public Vector3 center;  

    private Circle[,] circles;
    public Circle circle; //reference to the prefab in Project > Assets > Models, set in the Inspector

    private LineSegment[,,] lineSegments;  // [,,0] for horizontal, [,,1] for vertical
    public LineSegment lineSegment; //reference to the prefab in Project > Assets > Models, set in the Inspector

    
    bool drawCircles = false;
    bool drawHorizontalSegments = false;
    bool drawVerticalSegments = false;
    
    public Orb[,] orbs;  //set in WaveGenerator after it initializes array

    public GameObject audioClipSequencer;
    Loop4Clips loop4Clips;


    void Start()
    {

        circles = new Circle[numberRows,numberColumns];
        lineSegments = new LineSegment[numberRows, numberColumns, 2];

        for (int m = 0; m < numberRows; m++)
        {
            for (int n = 0; n < numberColumns; n++)
            {
                //NOTE:  there is an assumption here that the Start() method of WaveGenerator will run before the Start() method of WaveAnalyzer 
                center = orbs[m, n].center; //new Vector3(distance * n, distance * m, 0);
                circles[m, n] = Instantiate(circle, center, Quaternion.identity);
                circles[m, n].radius = orbs[m, n].orbitalRadius; //.1f * (m + 1);
                circles[m, n].isActive = false;
                circles[m, n].gameObject.SetActive(circles[m, n].isActive);

                for (int k = 0; k < 2; k++)  //k = 0 horizontal, 1 vertical
                {
                    lineSegments[m, n, k] = Instantiate(lineSegment, Vector3.zero, Quaternion.identity);

                    lineSegments[m, n, k].isActive = false;
                }
            }
        }

        loop4Clips = audioClipSequencer.GetComponent<Loop4Clips>();

    }

    void Update()
    {
        int m, n;
        Vector3 beginPoint, endPoint;

        //NOTE:  the circles don't change, but the endpoints of the line segments do

        if (drawHorizontalSegments)  //the conditional isn't necessary, but its more efficient when SetActive(false) 
        {
            for (m = 0; m < numberRows; m++)
            {
                for (n = 1; n < numberColumns; n++)  //skip n = 0 case for horizontal line segment
                {
                    beginPoint = orbs[m, n - 1].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 0].SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (drawVerticalSegments)   //the conditional isn't necessary, but its more efficient when SetActive(false) 
        {          
            for (m = 1; m < numberRows; m++) //skip m = 0 case for vertical line segment
            {
                for (n = 0; n < numberColumns; n++)
                {
                    beginPoint = orbs[m - 1, n].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 1].SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            drawHorizontalSegments = !drawHorizontalSegments;
            
            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    lineSegments[m, n, 0].isActive = !lineSegments[m, n, 0].isActive;
                    lineSegments[m, n, 0].gameObject.SetActive(lineSegments[m, n, 0].isActive);
                }
            }
            loop4Clips.ToggleVolume(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            drawVerticalSegments = !drawVerticalSegments;
            
            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    lineSegments[m, n, 1].isActive = !lineSegments[m, n, 1].isActive;
                    lineSegments[m, n, 1].gameObject.SetActive(lineSegments[m, n, 1].isActive);
                }
            }
            loop4Clips.ToggleVolume(2);
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            drawCircles = !drawCircles;
            
            for (m = 0; m < numberRows; m++)
            {
                for (n = 0; n < numberColumns; n++)
                {
                    circles[m, n].isActive = !circles[m, n].isActive;
                    circles[m, n].gameObject.SetActive(circles[m, n].isActive);
                }
            }
            loop4Clips.ToggleVolume(3);
        }


    }  
}