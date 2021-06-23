using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueUI;

    public Image tutorialimg;
    public Sprite[] tutorialImgs;

    private Queue<string> sentences;

    public bool hasImage;
    //private Queue<int> imageNumber; 
        
    void Start()
    {
        sentences = new Queue<string>();    
    }
    private void Update()
    {
        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.N))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        if (hasImage)
        {
            tutorialimg.sprite = tutorialImgs[dialogue.imageNumber];
        }
        
        sentences.Clear();//clear previous sentences

        //load all sentences
        foreach (string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }
        
        DisplayNextSentence();//shows first sentence
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();//get the next sentence
            dialogueText.text = sentence;//Update UI text
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue with {0} ended.", nameText);
    }
}
