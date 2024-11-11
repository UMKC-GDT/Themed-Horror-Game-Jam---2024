using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DialogueData : MonoBehaviour
{
    public Data dialogue;
    public DialogueManager manager;


    //to make dialogue trigger on overlap
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogue.isColliderDialogue)
        {
            manager.RunDialogue(dialogue);
            
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && dialogue.isColliderDialogue)
        {
           // runDialogue = false;

            //interacted = !repeatDialogue;

            manager.RemoveText();
            dialogue.isColliderDialogue = dialogue.repeatDialogue;
        }
    }
}

    [System.Serializable]
    public struct Data
    {
        public string name;
        public string[] dialogueLines;
        public float wordSpeed;
        public float durationToEnd;

        public bool repeatDialogue;
        public bool isColliderDialogue;
        
     

}
