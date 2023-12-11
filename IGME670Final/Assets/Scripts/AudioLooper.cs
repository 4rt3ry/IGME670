using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    public const float bpm = 120.0f;
    public const float measures = 4.0f;
    public const float timeSignature = 1.0f;

    public static float lengthSeconds => bpm / 60.0f * measures * timeSignature;
    public static float lengthMinutes => lengthSeconds / 60.0f;

    private AudioSource[] audioSources;
    private int nextSource = 0;
    private double nextEventTime = 0;

    private bool delayOn = true;
    private bool mute = false;
    private float startingVolume;

    [SerializeField] private AudioMixerGroup audioEffects;

    private void Awake()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        startingVolume = audioSources[0].volume;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(lengthSeconds);
        Debug.Log(lengthMinutes);

        nextEventTime = AudioSettings.dspTime + 2.0f;
        ToggleDelay();
    }


    // Update is called once per frame
    void Update()
    {
        if (AudioSettings.dspTime + 1.0f > nextEventTime)
        {
            if (!Metronome.playing) StartCoroutine(startMetronome((float)(nextEventTime - AudioSettings.dspTime + 0.1)));
            audioSources[nextSource].PlayScheduled(nextEventTime);
            nextEventTime += lengthSeconds;
            nextSource = 1 - nextSource;
        }
    }

    private void ToggleDelay()
    {
        delayOn = !delayOn;
        audioSources[0].outputAudioMixerGroup = delayOn ? audioEffects : null;
        audioSources[1].outputAudioMixerGroup = delayOn ? audioEffects : null;
    }

    private void ToggleMute()
    {
        mute = !mute;
        audioSources[0].volume = mute ? 0 : startingVolume;
        audioSources[1].volume = mute ? 0 : startingVolume;
    }

    public void OnToggleDelay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            ToggleDelay();
    }

    public void OnToggleMute(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            ToggleMute();
    }

    IEnumerator startMetronome(float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);
        Metronome.playing = true;
    }
}
