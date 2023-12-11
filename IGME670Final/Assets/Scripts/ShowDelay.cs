using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class ShowDelay : MonoBehaviour
{

    // FOR SOME DUMB REASON, YOU CAN'T ACCESS FIELDS OF AN AudioMixerGroup.
    public int numDelayMarkers;
    public float delay = 500;

    [SerializeField] private Transform markerParent;
    [SerializeField] private Metronome metronome;
    private SpriteRenderer[] markers;
    private Color[] colors;

    private bool mute = false;

    private void Start()
    {
        markers = GetComponentsInChildren<SpriteRenderer>();


        for (int i = 0; i < markers.Length; i++)
        {
            for (int j = 0; j < numDelayMarkers; j++)
            {
                //Debug.Log("instantiating a thing");
                SpriteRenderer currentMarker = Instantiate(markers[i], markerParent);
                currentMarker.transform.position = markers[i].transform.position;
                currentMarker.transform.localPosition += Vector3.right * delay / 1000 * 10 / 8 * (j + 1);
                Color currentColor = currentMarker.color;
                currentMarker.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f / (j + 1));
            }
        }
        //markers = GetComponentsInChildren<SpriteRenderer>();
        colors = markers.Select(sr => new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 0.7f)).ToArray();

        ToggleDelayMarkers(false);
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < markers.Length; i++)
        {
            if (markers[i].transform.position.x < metronome.Marker.transform.position.x)
            {
                markers[i].color = new Color(markers[i].color.r, markers[i].color.g, markers[i].color.b, 1);
            }
            else
            {
                markers[i].color = colors[i];
            }
            if (mute)
            {
                markers[i].color = new Color(colors[i].r, colors[i].g, colors[i].b, markers[i].color.a * 0.3f);
            }
        }

        
    }

    public void ToggleMute()
    {
        mute = !mute;
    }

    public void ToggleDelayMarkers(bool? toggle = null)
    {
        markerParent.gameObject.SetActive(toggle ?? !markerParent.gameObject.activeInHierarchy);
    }

    public void OnToggleDelayMarkers(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) ToggleDelayMarkers();
    }
}
