using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecker : MonoBehaviour
{
    // Start is called before the first frame update





    void Start()
    {
        Player.ActivePlayer.OnPlayerMov += GetMovement; 
    }

    private void OnDestroy()
    {
        Player.ActivePlayer.OnPlayerMov -= GetMovement;
    }




    // Update is called once per frame
    void GetMovement(Vector2 mov)
    {
        
    }
}
