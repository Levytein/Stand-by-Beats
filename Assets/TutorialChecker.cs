using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialChecker : MonoBehaviour
{
    // Start is called before the first frame update

    private bool wKey = false;
    private bool sKey = false;
    private bool aKey = false;
    private bool dKey = false;
    public GameObject TutorialCheckerObject;
    public GameObject goodFlashText;
    public bool isDialogueOver;

    public TextMeshProUGUI WText;
    public TextMeshProUGUI AText;
    public TextMeshProUGUI DText;
    public TextMeshProUGUI SText;
   

    void Start()
    {
        Player.ActivePlayer.OnPlayerMov += GetMovement; 
    }

    private void OnDestroy()
    {
        Player.ActivePlayer.OnPlayerMov -= GetMovement;
    }

    private void Update()
    {
        if (isDialogueOver == true)
        {
            TutorialCheckerObject.SetActive(true);
        }
        if(dKey && aKey && sKey && wKey)
        {
            TutorialCheckerObject.SetActive(false);
            goodFlashText.SetActive(true);
        }
    }


    // Update is called once per frame
    void GetMovement(Vector2 mov)
    {
        if(mov.x > 0 )
        {
            dKey = true;
            DText.SetText("Move Right: 1 / 1");
        }
        if (mov.x < 0)
        {
            aKey = true;
            AText.SetText("Move Left: 1 / 1");
        }
        if (mov.y > 0)
        {
            wKey = true;
            WText.SetText("Move Up: 1 / 1");
        }
        if (mov.y < 0)
        {
            sKey = true;
            SText.SetText("Move Down: 1 / 1");
        }

    }
}
