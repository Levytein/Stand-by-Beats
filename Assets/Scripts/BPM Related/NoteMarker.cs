using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMarker : MonoBehaviour
{
    public bool leftNut = true;
    public int timeStart;
    public int timeOffset;
    public int currentPos;
    public int beatOffset;
    public float noteOffset;
    public GameObject bs;

    private void Start()
    {
        bs = GameObject.Find("BPM");
        beatOffset = timeOffset / 10;
        noteOffset = bs.GetComponent<BeatSystem>().noteSpacing;
    }

    private void LateUpdate()
    {
        bs.GetComponent<BeatSystem>().musicInstance.getTimelinePosition(out currentPos);

        transform.localPosition = Vector3.Lerp((leftNut ? Vector3.left : Vector3.right) * noteOffset, Vector3.zero, (float) (currentPos - timeStart) / (timeOffset) );
        if(currentPos >= (timeStart + timeOffset - beatOffset))
        {
            Destroy(gameObject);
        }
     
    }
}
