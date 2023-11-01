using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private AudioSource[] audioSources;
    private int nextSource = 0;

    private float startTime;

    bool looping = false;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // IMPORTANT!!!!!!!!!!!!!!
        // The entire premise of 2 audio sources is to switch between for minimal sound clipping/delays
        
        if (looping)
        {
            if (!audioSources[nextSource].isPlaying)
            {
                NextSource();
            }
        }
    }

    public void StartAudioLoop(float seekTime)
    {

        audioSources[0].Stop();
        audioSources[1].Stop();
        startTime = (float)(AudioSettings.dspTime - seekTime);
        NextSource(seekTime);
        looping = true;
    }
    public void StopAudioLoop()
    {
        looping = false;
    }

    private void NextSource(float seekTime = -1)
    {
        if (seekTime > 0)
        {
            audioSources[nextSource].Play();
            audioSources[nextSource].time = seekTime;
        }
        else
        {
            audioSources[nextSource].time = 0;
            audioSources[nextSource].PlayScheduled(GetNextLoopTime());
        }
        nextSource = 1 - nextSource;
    }

    private double GetNextLoopTime()
    {
        return TileMap.loopPointSeconds - (AudioSettings.dspTime - startTime) % TileMap.loopPointSeconds + AudioSettings.dspTime;
    }
}
