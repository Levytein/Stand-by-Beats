using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecker : MonoBehaviour
{
    // Start is called before the first frame update

    private bool wKey = false;
    private bool sKey = false;
    private bool aKey = false;
    private bool dKey = false;
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
        if(mov.x > 0 )
        {
            dKey = true;
        }
        if (mov.x > 0)
        {
            dKey = true;
        }
        if (mov.x > 0)
        {
            dKey = true;
        }
        if (mov.x > 0)
        {
            dKey = true;
        }

    }
}
