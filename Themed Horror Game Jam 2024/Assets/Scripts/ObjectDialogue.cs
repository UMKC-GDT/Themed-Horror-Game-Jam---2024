using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectDialogue : MonoBehaviour
{

    public static ObjectDialogue instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private int index = 0;

    public string[] dialogue;
    public float wordSpeed;
    private bool runDialogue;
    private bool interacted;
    public bool repeatDialogue;

    // Start is called before the first frame update
    void Start()
    {
        interacted = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (runDialogue)
        {
            if (interacted)
            {
                RemoveText();
            }
            else
            {
                if(!dialoguePanel.activeInHierarchy)
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }

                if (Input.GetMouseButtonDown(0))
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
            }
            if (index >= dialogue.Length -1 && dialoguePanel.activeInHierarchy)
            {
                //if this is a peice of dialogue that should be repeated then interact will remain false 
                interacted = !repeatDialogue;
                index = 0;
                RemoveText();
            }
        }
        
    }

    //this gets called in the move object controller script
    public void RunDialogue()
    {
        runDialogue = true;
    }

    public void RemoveText()
    {
        runDialogue = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }


    public void NextLine()
    {
        if (index < dialogue.Length - 1)
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

    //to make dialogue trigger on collision
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runDialogue = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runDialogue = false;
            RemoveText();
        }
    }
    */
}
