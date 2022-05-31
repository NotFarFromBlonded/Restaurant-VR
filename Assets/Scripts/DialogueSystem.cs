using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public GameObject dialogueGUI;

    public string Names;

    public string[] dialogueLines;

    public bool dialogueActive = false;
    public bool outOfRange = true;

    int i = 0;

    void Start()
    {
        dialogueGUI.SetActive(false);
        dialogueText.text = "";
    }

    void Update()
    {
        
        
    }

    public void InRange()
    {
        outOfRange = false;
    }

    public void StartConvo()
    {
        dialogueGUI.SetActive(true);
        nameText.text = Names;
        dialogueText.text = dialogueLines[0];
    }

    public void EndConvo()
    {
        i = 0;
        dialogueActive = false;
        dialogueGUI.SetActive(false);
    }

    public void Convo()
    {
        if (Input.GetKeyDown(KeyCode.E) && outOfRange==false)
        {
            StartConvo();
            dialogueActive = true;
        } 
        
        if (dialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && i < dialogueLines.Length - 1)
            {
                dialogueText.text = dialogueLines[i++];
            } else if(Input.GetKeyDown(KeyCode.Space) && i == dialogueLines.Length - 1)
            {
                EndConvo();
                dialogueActive = false;
            } else if (Input.GetKeyDown(KeyCode.E))
            {
                EndConvo();
                dialogueActive = false;
            }
        }
    }
    
    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            dialogueActive = false;
            dialogueGUI.SetActive(false);
        }
    }
}
