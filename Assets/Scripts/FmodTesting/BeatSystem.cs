using FMOD.Studio;
using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

class BeatSystem : MonoBehaviour
{
    [SerializeField, Range(0, 30)] public int roomsCleard = 0;
    [SerializeField, Range(0, 3)] public int level = 1;
    [SerializeField, Range(0, 3)] public int bossPhase = 0;
    [SerializeField, Range(0, 3)] public int paused = 0;
    [SerializeField] private bool doDestroy = false;

    //public static BeatSystem instance;
    public GameObject noteMarker;
    public Transform BPMImages;
    public float noteSpacing = 500.0f;
    public float calibration;

    public class TimelineInfo
    {
        public int currentBeat = 0;
        public int currentPosition = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    public static EmitterRef emitter;
    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public static int lastBeat = 0;
    public static string lastMarker = null;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public float timeSinceLastBeat = 0;
    public float GraceTime = 0.5f;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    public FMOD.Studio.EventInstance musicInstance;
    public FMODUnity.EventReference fmodEvent;

    void Start()
    {
        if(!doDestroy) DontDestroyOnLoad(this.gameObject);

        calibration = 0.0f; // ToDo: Connect with the options menu script to recieve player's calibration settings (Keep between -10.0f and 10.0f)

        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    private void Update()
    {
        Metronome();

        musicInstance.setParameterByName("Rooms Cleared", roomsCleard);
        musicInstance.setParameterByName("Flags", level);
        musicInstance.setParameterByName("Boss Phase", bossPhase);
        musicInstance.setParameterByName("Pause Status", paused);

        musicInstance.getTimelinePosition(out timelineInfo.currentPosition);
    }

    void Metronome()
    {
        if (lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;
            timeSinceLastBeat = 0.0f;
            JudgmentBar();

            if (beatUpdated != null)
            {
                beatUpdated();
            }
        }
        
        timeSinceLastBeat += 1000.0f * Time.deltaTime;
    }

    void JudgmentBar()
    {
        NoteMarker temp = Instantiate(noteMarker, BPMImages.transform).GetComponent<NoteMarker>();
        temp.leftNut = true;
        temp.timeOffset = Mathf.Abs((int)((60000.0f / 120.0f) - timeSinceLastBeat));
        musicInstance.getTimelinePosition(out temp.timeStart);


        temp = Instantiate(noteMarker, BPMImages.transform).GetComponent<NoteMarker>();
        temp.leftNut = false;
        temp.timeOffset = Mathf.Abs((int)((60000.0f / 120.0f) - timeSinceLastBeat));
        musicInstance.getTimelinePosition(out temp.timeStart);
    }

    public bool BeatCheck()
    {
        if (Mathf.Abs((float)((60000.0f / 120.0f) - timeSinceLastBeat)) <= (GraceTime * 1000.0f + calibration) || Mathf.Abs(timeSinceLastBeat) <= (GraceTime * 1000.0f + calibration)) return true;
        else return false;
    }

    public float CurrentPosition()
    {
        Debug.Log("called");
        return (float)timelineInfo.currentPosition;
    }

    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Beat = {0}", timelineInfo.currentBeat));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}