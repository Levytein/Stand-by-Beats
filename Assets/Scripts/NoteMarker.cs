using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMarker : MonoBehaviour
{
    public bool leftNut = true;
    public double timeStart;
    public double timeOffset;
    

    private void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp((leftNut ? Vector3.left : Vector3.right) * BPM.activeBPM.noteSpacing, Vector3.zero, (float) ((AudioSettings.dspTime - timeStart)/( timeOffset - timeStart )) );
        if(AudioSettings.dspTime > timeOffset)
        {
            Destroy(gameObject);
        }
     
    }
}
