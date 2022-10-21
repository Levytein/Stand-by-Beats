using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Sprite characterSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && SceneManager.GetActiveScene().buildIndex != 1)
        {
            TriggerDialogue();
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
