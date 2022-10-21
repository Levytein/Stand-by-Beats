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
        if(sentences.Count == 0 && SceneManager.GetActiveScene().buildIndex != 1 )
        {
            EndDialogue();
            return;
        }
        else if(sentences.Count == 0 && SceneManager.GetActiveScene().buildIndex == 1)
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
        UIObject.SetActive(false);
    }
}
