using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class ClickOnBeat : MonoBehaviour
{
    public GameObject bS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Fire(InputAction.CallbackContext context)
    {
        bool onBeat = bS.GetComponent<BeatSystem>().BeatCheck();
        Debug.Log(onBeat);
    }
}
