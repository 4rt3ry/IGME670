using UnityEngine;
using System.Collections;

public class Loop4Clips : MonoBehaviour
{
    public float bpm;
    public int numBeatsPerSegment; //segment refers to the entire clip
    public AudioClip[] clips = new AudioClip[8]; //need a pair of clips for each of the 4 tracks 

    private double nextEventTime;
    private int skip;
    private AudioSource[] audioSources = new AudioSource[8];
    private bool running;
    double time;

    float deltaT;

    void Start()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            GameObject child = new GameObject("Player");
 
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
            audioSources[i].clip = clips[i];
            audioSources[i].volume = 0.0f; //initially all is quiet on the WaSwRe front
        }
        audioSources[0].volume = 1.0f; //we want to hear the foundation track throughout
        audioSources[1].volume = 1.0f; 

        skip = 0;

        bpm = 90.0f;
        numBeatsPerSegment = 4;
        deltaT = 60.0f / bpm * numBeatsPerSegment; //seconds/minute x minute/beats x beats/segment = seconds/segment 

        Debug.Log("bpm is " + bpm);
        Debug.Log("numBeatsPerSegment is " + numBeatsPerSegment);
        Debug.Log("deltaT is " + deltaT);
       
        running = false;
    }

    void Update()
    {
        if (!running)
        {
            nextEventTime = AudioSettings.dspTime + deltaT; //initial time set to be two seconds from "now"
            running = true;
            return;
        }

        if (AudioSettings.dspTime + 1.0f > nextEventTime)
        {
            // We are now less than 1 second before the time at which the sound should play,
            // so we will call PLayScheduled now in order for the system to have enough time
            // to prepare to start playing clips specified time.
            
            for (int i = 0; i < clips.Length/2; i++)
            {    
               audioSources[2*i+skip].PlayScheduled(nextEventTime);
            }

            skip = 1 - skip;  //note that this toggles between 0 and 1, making 2i + skip either even or odd
           
            nextEventTime += deltaT;     
        }
    }

    //clipIndex should be 1 for horizontal, 2 for vertical, 3 for circular highlights produced in WaveAnalyzer
    public void ToggleVolume(int clipIndex) 
    {
        audioSources[2 * clipIndex].volume = 1.0f - audioSources[2 * clipIndex].volume;
        audioSources[2 * clipIndex + 1].volume = 1.0f - audioSources[2 * clipIndex + 1].volume;
    }
}
