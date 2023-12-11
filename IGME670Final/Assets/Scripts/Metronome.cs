using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    [SerializeField] private SpriteRenderer marker;

    private float time = 0;
    private float musicTime = 0;

    public static bool playing = false;

    public SpriteRenderer Marker => marker;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            marker.transform.localPosition = new Vector3(musicTime - 5, 0, 0);

            time += Time.deltaTime;
            musicTime = (time % AudioLooper.lengthSeconds) * 10 / AudioLooper.lengthSeconds;
        }
    }
}
