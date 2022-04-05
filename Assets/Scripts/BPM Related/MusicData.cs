using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "unnamedMusicdata", menuName = "Data/Musicdata")]
public class MusicData : ScriptableObject
{
    public AudioClip song;
    public float songBPM = 120f;

    public double startDelay = 0.0f;
    
}
