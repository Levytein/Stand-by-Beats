using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private BeatSystem bS;

    void Start()
    {
        bS = GetComponent<BeatSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Hotel Music (120 bpm)");
            instance.start();
            //bS.AssignBeatEvent(instance);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //bS.StopAndClear(instance);
        }
    }
}
