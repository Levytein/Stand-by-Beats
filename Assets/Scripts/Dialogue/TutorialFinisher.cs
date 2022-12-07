using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFinisher : MonoBehaviour
{
    public Dialogue dialogue;
    public Sprite characterSprite;

    public TutorialChecker tutChecker;

    private void Update()
    {
        if (tutChecker.isAttackingOver == true)
        {
            TriggerDialogue();
            tutChecker.isMovementOver = false;
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, characterSprite);
        Destroy(GetComponent<TutorialFinisher>());
    }

    IEnumerator WaitforDialogue()
    {

        yield return new WaitForSeconds(3.0f);
        TriggerDialogue();
    }
}

