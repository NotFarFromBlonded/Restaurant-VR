using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPC : MonoBehaviour
{
    

    public DialogueSystem dialogueSystem;
    public movement mv;
    
    public string Name;

    public Animator anim,head_move;
    public GameObject Player;

    public GameObject waiter;
    public GameObject waiter1;
    public GameObject bill;

    public bool waiter_1,headnodd;

    [TextArea(5, 10)]
    public string[] sentences;
    public bool rotateplayer=false;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>().GetComponent<DialogueSystem>();
        headnodd=false;
        
    }

    void Update()
    {
        if (waiter_1)
        {
            dialogueSystem.npcType = 3;
            waiter_1 = false;
        }
        
    }

    public void OnTriggerStay(Collider other)
    {

        this.gameObject.GetComponent<NPC>().enabled = true;
        dialogueSystem.InRange();

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.E) && mv.timer == 0f)
        {
            //this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Place3DDialogueAudio();
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.NPCName();


        }
        if ((dialogueSystem.dI == 1) && dialogueSystem.waiterName)
        {
            if(!headnodd){
                head_move.SetTrigger("isheadmoving");
                headnodd=true;
            }

        }
        // else{
        //     head_move.SetBool("isheadmoving",false);
        // }


        if ((dialogueSystem.dI == 3) && dialogueSystem.waiterName)
        {
            anim.SetBool("take_a_seat", true);
             rotateplayer=true;
            headnodd=false;
        }
        else
        {
            anim.SetBool("take_a_seat", false);
        }

        if(this.gameObject.name == "Waiter_1" && dialogueSystem.dI == 5 && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("take_a_seat", false);
            waiter.SetActive(true);
            waiter1.SetActive(false);
           
        }
        if (this.gameObject.name == "Waiter_1" && dialogueSystem.dI == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("take_a_seat", false);
            bill.SetActive(true);

        }
        if (this.gameObject.name == "Waiter_1" && dialogueSystem.dI == 3 && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("take_a_seat", false);
            bill.SetActive(false);
            // Quaternion tablerot= Quaternion.Euler(new Vector3(0,137f,0));
            // Player.transform.rotation=tablerot;
           
            

        }

        if(this.gameObject.name == "TableCustomer" && dialogueSystem.dI == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(mv.TCSintantiator());
            mv.timer = 3.5f;
            mv.TCSinstantiated = true;
        }

        if(this.gameObject.name == "TableCustomerSeated" && dialogueSystem.dI == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            
            
                
            StartCoroutine(mv.TCSdestroyer());
            mv.timer = 3.5f;
            mv.TCSinstantiated = false;


        } 
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && this.gameObject.tag == "Waiter")
        {
            
            if (this.gameObject.name == "Waiter_1")
            {
                waiter_1 = true;
                dialogueSystem.npcType = 3;
            }
            else
            {

                dialogueSystem.npcType = 0;
                dialogueSystem.waiterName = true;
            }


        }
        if(other.gameObject.tag == "Player" && this.gameObject.tag == "Customer:Male")
        {
            dialogueSystem.npcType = 1;
            dialogueSystem.customerGender = 0;
            
        }
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "Customer:Female")
        {
            dialogueSystem.npcType = 1;
            dialogueSystem.customerGender = 1;

        }
        if(other.gameObject.tag == "Player" && this.gameObject.tag == "Cashier")
        {
            dialogueSystem.npcType = 2;
        }
        
        if(other.gameObject.tag == "Player" && this.gameObject.tag == "TableCustomer")
        {
            if(this.gameObject.name == "TableCustomerSeated")
            {
                dialogueSystem.npcType = 5;
            } else
            {
                dialogueSystem.npcType = 4;
            }
            
        }
        
    }

    public void OnTriggerExit()
    {
        dialogueSystem.OutOfRange();
        dialogueSystem.waiterName = false;
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
