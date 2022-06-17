using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public GameObject dialogueGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Space;

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    public int dI;
    private NPC npc;

    
    public bool waiterName = false;

    private FMOD.Studio.EventInstance d;
    public int npcType = 0;
    public int customerGender = 0;
        

    void Start()
    {
        
        dialogueText.text = "";
    }

    void Update()
    {
  
    }

    public void InRange()
    {
        outOfRange = false;
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);
        nameText.text = Names;
        
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            dI = 0;

            while (dI < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[dI++]));
                    DialogueVoiceOver(npcType, customerGender, dI);
                    if (dI >= dialogueLength)
                    {
                        
                        dialogueEnded = true;
                    }
                    
                }
                
                
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;
            
            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dI = 0;
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dI = 0;
            d.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void Place3DDialogueAudio()
    {
        d = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue");
        d.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    public void DialogueVoiceOver(int npcType, int customerGender, int dialogueIndex)
    {
        d.setTimelinePosition(0);
        d.setParameterByName("NPCType", npcType);
        d.setParameterByName("CustomerGender", customerGender);
        d.setParameterByName("DialogueIndex", dialogueIndex);

        d.start();
        //d.release();
    }

    private void OnApplicationQuit()
    {
        d.release();
    }
}
