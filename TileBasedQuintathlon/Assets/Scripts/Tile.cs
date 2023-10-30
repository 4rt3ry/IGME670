using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private AudioClip audio;
    public AudioClip Audio => audio;
}
