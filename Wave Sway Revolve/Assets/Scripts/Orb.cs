using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Vector3 pos;
    public float phaseAngle;  //set by WaveManager script
    public float orbitalRadius; //set by WaveManager script
    public Vector3 center;  //set by WaveManager script
    public float tweek; //potentially, this can be used to adjust the rate at which theta changes (speed at which the orb travels)
    private const float halfPI = .5f * Mathf.PI; //this factor makes the orbital period last about 4 seconds
    

    private float theta;  //time converted into angle of orbit

    // Start is called before the first frame update
    void Start()
    {
        //Adjust size of orbs
        transform.localScale = new Vector3(.4f, .4f, .4f);  //Transform has position and localPosition, rotation and localRotation, localScale but no scale property!
        //phaseAngle = 0f; //in radians, set by WaveManager
        //orbitalRadius = 1f; // set by WaveManager
        theta = 0f;
        tweek = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //updating theta with deltaTime to ensure smooth movement
        theta += tweek*Time.deltaTime; //when theta reaches 4 seconds (or a multiple), halfPI*theta = 2 PI (or a multiple), the Orb completes another revolution
        //note that the Orb moves in a counterclockwise (CCW) orbit 
        pos = center + new Vector3(orbitalRadius * Mathf.Cos(halfPI*theta + phaseAngle), orbitalRadius * Mathf.Sin(halfPI * theta + phaseAngle), 0f);
        transform.position = pos;
    }
}