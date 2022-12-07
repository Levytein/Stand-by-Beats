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

    private bool attacked = false;
    private bool rolled = false;

    private int counter = 0;
    public GameObject TutorialCheckerObject;
    public GameObject SecondTutObject;
    public GameObject goodFlashText;
    public GameObject goodFlashText2;

    public bool isDialogueOver;
    public bool isMovementOver;
    public bool isAttackingOver;

    public TextMeshProUGUI WText;
    public TextMeshProUGUI AText;
    public TextMeshProUGUI DText;
    public TextMeshProUGUI SText;

    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI RollText;


    void Start()
    {
        Player.ActivePlayer.OnPlayerMov += GetMovement;
        Player.ActivePlayer.OnPlayeRol += GetRoll;
        Player.ActivePlayer.OnATKCheck += GetAttack;
    }

    private void OnDestroy()
    {
        Player.ActivePlayer.OnPlayerMov -= GetMovement;
        Player.ActivePlayer.OnPlayeRol -= GetRoll;
        Player.ActivePlayer.OnATKCheck -= GetAttack;
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
            isMovementOver = true;
            SecondTutObject.SetActive(true);
        
        }
        if(attacked && rolled == true )
        {
            
            SecondTutObject.SetActive(false);
            isAttackingOver = true;
            goodFlashText2.SetActive(true);
          
            
            
        }
    }


    void GetAttack()
    {
        AttackText.SetText("Attack: 1 / 1");
        attacked = true;
    }
    void GetRoll()
    {
        RollText.SetText("Dodge: 1 / 1");
        rolled = true;
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

    IEnumerator WaitforGood()
    {

        yield return new WaitForSeconds(2.0f);
        goodFlashText.SetActive(false);
    }
}
