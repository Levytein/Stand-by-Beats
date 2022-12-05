using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteMarker : MonoBehaviour
{
    public bool leftNut = true;
    public int timeStart;
    public int timeOffset;
    public int currentPos;
    public float startTime;
    public float noteOffset;
    public GameObject bs;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI EffectOne;
    public TextMeshProUGUI EffectTwo;
   


    private void Start()
    {
        bs = GameObject.Find("BPM");
        noteOffset = bs.GetComponent<BeatSystem>().noteSpacing;
        startTime = Time.time;
    }

    private void LateUpdate()
    {
        bs.GetComponent<BeatSystem>().musicInstance.getTimelinePosition(out currentPos);

        transform.localPosition = Vector3.Lerp((leftNut ? Vector3.left : Vector3.right) * noteOffset, Vector3.zero, (float) (currentPos - timeStart) / (timeOffset) );
        if(currentPos >= (timeStart + timeOffset))
        {
            Destroy(gameObject);
        }

        if(startTime - timeStart >= 2.0f)
        {
            Destroy(gameObject);
        }

    }
}
