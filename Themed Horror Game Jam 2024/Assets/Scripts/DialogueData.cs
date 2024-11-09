using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DialogueData : MonoBehaviour
{
    public Data dialogue;

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
