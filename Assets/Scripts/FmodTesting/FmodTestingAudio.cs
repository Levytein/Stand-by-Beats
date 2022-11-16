using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FmodTestingAudio : MonoBehaviour
{
    [SerializeField, Range(0, 30)] private int roomsCleard = 0;
    [SerializeField, Range(0, 3)] private int level = 0;
    [SerializeField, Range(0, 3)] private int bossPhase = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("space")) roomsCleard++;

        var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.SetParameter("Rooms Cleared", roomsCleard);
        emitter.SetParameter("Flags", level);
        emitter.SetParameter("Boss Phase", bossPhase);
    }
}
