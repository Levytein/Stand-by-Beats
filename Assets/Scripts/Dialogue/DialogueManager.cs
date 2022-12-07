using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sentenceText;
    public Image characterTalking;
    public GameObject UIObject;
    public GameObject bpmBar;
    private Queue<string> sentences;
    public TutorialChecker  tutChecker;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue (Dialogue dialogue, Sprite character)
    {
        Time.timeScale = 0;
        UIObject.SetActive(true);
        if(bpmBar != null)
        {
            bpmBar.SetActive(false);
        }

        characterTalking.sprite = character;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }


    public void DisplayNextSentence()
    {
        if(sentences.Count == 0 && SceneManager.GetActiveScene().buildIndex == 5 && Martini.martiniDead == true)
        {
            EndDialogue();
            SceneManager.LoadScene(6);
        }
        if(sentences.Count == 0 && SceneManager.GetActiveScene().buildIndex != 1 )
        {
            EndDialogue();
            return;
        }
        else if(sentences.Count == 0 && SceneManager.GetActiveScene().buildIndex == 1 && tutChecker != null)
        {
            Debug.Log("Im reaching the dialogue manager");
            tutChecker.isDialogueOver = true;
            EndDialogue();
            return;
        }
            string sentence = sentences.Dequeue();
        sentenceText.text = sentence;


    }

    void EndDialogue()
    {
        Debug.Log("End conversation");
        Time.timeScale = 1;

        if (bpmBar != null)
        {
            bpmBar.SetActive(true);
        }
      
        UIObject.SetActive(false);
    }
}
