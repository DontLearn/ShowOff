using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueUI; //to hide/show when talking

    private void Awake()
    {
        dialogue = this.GetComponentInParent<Dialogue>();
    }

    /// <summary>
    /// Method for starting a new dialogue. Trigger -> this method
    /// </summary>
    public void TriggerDialogue()
    {
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueUI.SetActive(true);
            TriggerDialogue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueUI.SetActive(false);
        }        
    }
}
