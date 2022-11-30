using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BPM : MonoBehaviour
{

    public static BPM activeBPM;
    public static BPM ActivePlayer
    {
        get
        {
            return activeBPM;
        }


    }

    public double bpm = 140.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    public float noteSpacing = 800f;

    public GameObject noteMarker;

    public float noteSpawnAhead = 2;
    private double noteSpawnTime = 0;
    private double nextNoteTime = 0;

    public double NextNoteTime 
    {
        get 
        {
            return nextNoteTime;
        }
    }

    private double lastTick = 0;

    public double LastTick
    {
        get
        {
            return lastTick;
        }
    }
    public MusicData currentSong;
    public AudioSource audioSource;


    public Transform BPMImages;


    private void Awake()
    {
        activeBPM = this;
    }

    void InitializeBPM()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        audioSource.clip = currentSong.song;
        bpm = currentSong.songBPM;


        noteSpawnTime = 60f / bpm * noteSpawnAhead;
        audioSource.PlayScheduled(startTick + currentSong.startDelay + noteSpawnTime);
    }
    void Start()
    {

        InitializeBPM();

    }

    public void PlaySong(MusicData newSong)
    {
        if(currentSong == newSong)
        {
            return;
        }
        currentSong = newSong;
        InitializeBPM();

    }
    void Update()
    {
       if(AudioSettings.dspTime >= nextNoteTime && BPMImages != null)
        {
            //NoteMarker temp = Instantiate(noteMarker, BPMImages.transform ).GetComponent<NoteMarker>();
            //temp.leftNut = true;
            //temp.timeOffset = (lastTick / sampleRate + noteSpawnTime ) ;
            //temp.timeStart = (lastTick / sampleRate - 60f / bpm) ;


            //temp = Instantiate(noteMarker, BPMImages.transform).GetComponent<NoteMarker>();
            //temp.timeOffset = (lastTick / sampleRate + noteSpawnTime) ;
            //temp.leftNut = false;
            //temp.timeStart = (lastTick / sampleRate - 60f / bpm);


            lastTick = nextTick - (60d / bpm * sampleRate);
            nextNoteTime = nextTick / sampleRate;
          
        }

        
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
               // data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }

    

}
