using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    public bool canpickup; //a bool to see if you can or cant pick up the item
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    public bool hasItem; // a bool to see if you have an item in your hand
    // Start is called before the first frame update

    private GUIStyle guiStyle;
    private string msg;

    private AudioSource audio;

    private ObjectDialogue dialogueComponent;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        dialogueComponent = GetComponent<ObjectDialogue>();
        canpickup = false;    //setting both to false
        hasItem = false;
        setupGui();
    }

    // Update is called once per frame
    void Update()
    {
        if (canpickup == true && myHands.transform.childCount == 0) // if you enter the collider of the object
        {
            msg = getGuiMsg(hasItem);
            if (Input.GetKeyDown(KeyCode.Q))  // can be e or any key
            {
                if (dialogueComponent != null)
                {
                    dialogueComponent.RunDialogue(); // I added this to run dialogue if this object has it
                }
                if (audio != null)
                {
                    audio.Play();
                }

                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                ObjectIwantToPickUp.GetComponent<Collider>().enabled = false;
                ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
                ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                hasItem = true;
                msg = getGuiMsg(hasItem);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R) && hasItem == true) // if you have an item and get the key to remove the object, again can be any key
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            ObjectIwantToPickUp.GetComponent<Collider>().enabled = true;
            ObjectIwantToPickUp.transform.parent = null; // make the object not be a child of the hands
            hasItem = false;
            msg = getGuiMsg(hasItem);
        }
    }
     void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.CompareTag("Player") ) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            Debug.Log("tag");
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = this.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canpickup = false; //when you leave the collider set the canpickup bool to false
           
        }
    }



    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press Q to pick up object";
    }

    private string getGuiMsg(bool isInHand)
    {
        string rtnVal;
        if (!isInHand)
        {
            rtnVal = "Press Q to pick up object";
        }
        else
        {
            rtnVal = "Press R to drop object";
        }

        return rtnVal;
    }

    void OnGUI()
    {
        if (hasItem || canpickup)  //show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
        }
    }
}