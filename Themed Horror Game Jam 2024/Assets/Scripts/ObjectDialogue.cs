using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private int index = 0;

    public string[] dialogue;
    static public float WORDSPEED = (float)0.05;
    private bool runDialogue;
    private bool interacted;
    public bool repeatDialogue;
    public bool colliderTrigger;

    void Start()
    {
        if(dialoguePanel == null)
            Debug.Log("Dialogue Panel Null on: " + gameObject.name);
        interacted = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (runDialogue == false)
            return;

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
            yield return new WaitForSeconds(WORDSPEED);
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

    //to make dialogue trigger on overlap
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && colliderTrigger)
        {
            runDialogue = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& colliderTrigger)
        {
            runDialogue = false;

            interacted = !repeatDialogue;
            index = 0;

            RemoveText();
        }
    }
}
