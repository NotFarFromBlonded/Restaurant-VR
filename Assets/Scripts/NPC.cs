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
            dialogueSystem.Place3DDialogueAudio();
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.NPCName();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && this.gameObject.tag == "Waiter")
        {
            if (this.gameObject.name == "Waiter_1")
            {
                dialogueSystem.npcType = 3;
            } else
            {
                dialogueSystem.npcType = 0;
                dialogueSystem.waiterName = true;
            }
            
        } else if(other.gameObject.tag == "Player" && this.gameObject.tag == "Customer:Male")
        {
            dialogueSystem.npcType = 1;
            dialogueSystem.customerGender = 0;
            
        }
        else if (other.gameObject.tag == "Player" && this.gameObject.tag == "Customer:Female")
        {
            dialogueSystem.npcType = 1;
            dialogueSystem.customerGender = 1;

        } else if(other.gameObject.tag == "Player" && this.gameObject.tag == "Cashier")
        {
            dialogueSystem.npcType = 2;
        }
        
    }

    public void OnTriggerExit()
    {
        dialogueSystem.OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
