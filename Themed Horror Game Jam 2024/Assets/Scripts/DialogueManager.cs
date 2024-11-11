using System;
using System.Collections; 
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject dialoguePanel; //the panel the text will display on
    public TextMeshProUGUI dialogueText; // the text that will display


    private List <Data> DialogueObjects = new List <Data> ();

    private bool runDialogue;
    private int index;
    private Data currentDialogue;
    float timePassed;
    private bool interupted;

    private string currentObjectName;
    private int listIndex;


    // Start is called before the first frame update
    void Start()
    {
       index = 0;
       timePassed = 0.0f;
        listIndex = -1;
        currentObjectName = "";
        interupted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueObjects.Count > 0)
        {
            if (!runDialogue)
                return;

            //Debug.Log("size: " + DialogueObjects.Count);


            

            //start
            if (!dialoguePanel.activeInHierarchy || interupted) 
            {
                StopAllCoroutines();
                dialogueText.text = "";
                timePassed = 0.0f;
                index = 0;

                //set it to the dialogue of the current command 
                for (int i = 0; i < DialogueObjects.Count; i++)
                {
                    if (DialogueObjects[i].name == currentObjectName)
                    {
                        currentDialogue = DialogueObjects[i];
                        listIndex = i;

                    }
                }

                dialoguePanel.SetActive(true);
                interupted = false;
                StartCoroutine(Typing());
            }

            //if click
            if (Input.GetMouseButtonDown(0)) 
            {
                if (dialogueText.text == currentDialogue.dialogueLines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = currentDialogue.dialogueLines[index];
                }
            }

            //if duration
            if (dialogueText.text == currentDialogue.dialogueLines[index])
            {
                if(timePassed < currentDialogue.durationToEnd)
                    timePassed = timePassed + 0.01f;
                else
                {
                    timePassed = 0.0f;
                    NextLine();
                }
                   
            }


            //finish
            if (index >= currentDialogue.dialogueLines.Length - 1 && dialoguePanel.activeInHierarchy)  // if the dialogye is over 
            {

                //if this is a peice of dialogue that should be repeated then interact will remain false 
                //interacted = !repeatDialogue;
                
                RemoveText();
               // DialogueObjects.RemoveAt(0);
                
            }

        }
        
    }

    public void RunDialogue(Data data)
    {
        
        
        if (!DialogueObjects.Contains(data))
            DialogueObjects.Add(data);

        currentObjectName = data.name;


        runDialogue = true;

        if (dialoguePanel.activeInHierarchy)
        {
            interupted = true;
        }
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
        foreach (char letter in currentDialogue.dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(DialogueObjects[0].wordSpeed);
        }
    }


    public void NextLine()
    {
        if (index < currentDialogue.dialogueLines.Length - 1)
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
}
