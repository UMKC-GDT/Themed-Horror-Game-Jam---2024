using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectDialogue : MonoBehaviour
{
    public GameObject dialoguePanel; //the panel the text will display on
    public TextMeshProUGUI dialogueText; // the text that will display
    
    private int index = 0; //index of dilogue array
    public string[] dialogue; 

    static public float WORDSPEED = (float)0.05;
    
    
    private bool runDialogue; //if dilogue should run
    private bool interacted; //if dialogue has alread ran
    public bool interupted; //if dialogue is interupted by another objects dialogue call 

    public bool repeatDialogue; //if this dialogue should repeat
    public bool colliderTrigger; //if this dialogue should trigger when collided with 


    void Start()
    {
        interacted = false; 
        interacted = false;

        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }
    
    void Update()
    {

        if (runDialogue == false)
            return;
        

        if (interacted) // dont run again if its already been run
        {
            RemoveText();
            return;
        }

       
            
        if(!dialoguePanel.activeInHierarchy) //if dialogue panel is not open, open it and start typing 
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
            

        if (Input.GetMouseButtonDown(0)) //if input interupts dialogue 
        {
            if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index];
                    
            }
        }

        

        if (index >= dialogue.Length -1 && dialoguePanel.activeInHierarchy)  // if the dialogye is over 
        {
            //if this is a peice of dialogue that should be repeated then interact will remain false 
            interacted = !repeatDialogue;
            //index = 0;
            RemoveText();
        }
    }

    //this gets called in the move object controller script
    public void RunDialogue()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            interupted = true;
        }
        
        runDialogue = true;
    }

    public void RemoveText()
    {
        Debug.Log("Remove:" + dialogueText.text + ", index: " + index);
        //StopAllCoroutines();
        runDialogue = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(WORDSPEED);
        }
    }


    public void NextLine()
    {
        if (index < dialogue.Length - 1 )
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    //to make dialogue trigger on overlap
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && colliderTrigger)
        {
            RunDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& colliderTrigger)
        {
            runDialogue = false;

            interacted = !repeatDialogue;

            RemoveText();
            colliderTrigger = repeatDialogue;
        }
    }
}
