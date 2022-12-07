using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Sprite characterSprite;
    public static bool hubCompleted = false;
    public static bool firstEDM = false;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex != 1)
        {
            TriggerDialogue();
        }*/
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (collision.CompareTag("Player") && hubCompleted == false)
            {
                TriggerDialogue();
                hubCompleted = true;
            }
        }
       if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (collision.CompareTag("Player") && firstEDM == false)
            {
                TriggerDialogue();
                firstEDM = true;
            }
        }
        

        else if(collision.CompareTag("Player"))
        {
            StartCoroutine(WaitforDialogue());
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue,characterSprite);
        Destroy(this.gameObject);
    }

    IEnumerator WaitforDialogue()
    {
        
        yield return new WaitForSeconds(3.0f);
        TriggerDialogue();
    }
    
}
