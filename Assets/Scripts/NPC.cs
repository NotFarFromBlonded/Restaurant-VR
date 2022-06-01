using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPC : MonoBehaviour
{
    

    public DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {

        this.gameObject.GetComponent<NPC>().enabled = true;
        dialogueSystem.InRange();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.NPCName();
        }
    }

    public void OnTriggerExit()
    {
        dialogueSystem.OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
