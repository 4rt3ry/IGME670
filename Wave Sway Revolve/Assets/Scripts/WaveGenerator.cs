using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    public GameObject waveAnalyzer;  //to be set in the Inspector

    public Orb orb;  //reference to the prefab in Project > Assets > Models, to be set in the Inspector

    public Orb[,] orbs;  //rows x columns
    const int numberRows = 10;
    const int numberColumns = 12;

    int distance = 2; //distance from one ball's center to a neighboring ball's center, the same spacing in both horizontal and vertical directions 

    // Start is called before the first frame update - NOTE: the script execution order needs to be set explicitly in the Unity editor
    void Start()
    {
        orbs = new Orb[numberRows, numberColumns];
        waveAnalyzer.GetComponent<WaveAnalyzer>().orbs = orbs;

        //Instantiate the 2d array using nested for loops
        //Each orb's center, phaseAngle, and orbitalRadius must change accordingly with it's position in the grid
        //Offset the starting angle for each column by 30 degrees
        //Have the radius decrease inversely to the rows (as the row number increases, radius decreases)

        for (int m = 0; m < numberRows; m++)
        {
            for (int n = 0; n < numberColumns; n++)
            {
                orbs[m, n] = Instantiate(orb, Vector3.zero, Quaternion.identity); 
                //NOTE:  there is a discrepancy between the Unity API docs and what is seen when mouse "hovers", which shows return value is GameObject
                orbs[m, n].center = new Vector3(distance * n, distance * m, 0);
                orbs[m, n].phaseAngle = (2 * Mathf.PI / numberColumns) * n; //phase differs by 30 degrees from column to column
                orbs[m, n].orbitalRadius = .1f * (m + 1); //chosen so that the radii changes linearly with depth; .1 is just an arbitrary pick
                //alternatively, we could write setter methods in Orb for the center, phaseAngle, and orbitalRadius and call them here
            }
        }
    }

    //NOTE: no need for Update() since Orb script has its own Update()
   
}



