using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PhotobookGrabber : MonoBehaviour
{
    public GameObject PhotobookOverlay; //reference to the photobook prefab
    bool canpickup; //a bool to see if you can or cant pick up the photobook
    GameObject PhotobookPhysical; // the gameobject onwhich you collided with
    public bool bookGrabbed;
    
    private GUIStyle guiStyle;
    private string msg;

    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        
        bookGrabbed = false;
        canpickup = false;
        audio = GetComponent<AudioSource>();
        setupGui();
    }

    // Update is called once per frame
    void Update()
    {
        if (canpickup == true && PhotobookPhysical.activeInHierarchy) // if you enter the collider of the object
        {
            msg = getGuiMsg(false);
            if (Input.GetKeyDown(KeyCode.Q))  // can be e or any key
            {
                if (audio != null)
                {
                    audio.Play();
                }
                PhotobookPhysical.SetActive(false);
                bookGrabbed = true;
                
                msg = getGuiMsg(true);
            }
        }
    }

    void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.CompareTag("Player")) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            Debug.Log("In Range of Photobook");
            canpickup = true;  //set the pick up bool to true
            PhotobookPhysical = this.gameObject.transform.GetChild(0).gameObject; ; //set the gameobject you collided with to one you can reference
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
        msg = "Press Q to pick up the Photo Album";
    }

    private string getGuiMsg(bool pickedUp)
    {
        string rtnVal;
        if (!pickedUp)
        {
            rtnVal = "Press Q to pick up the Photo Album";
        }
        else
        {
            rtnVal = "";
        }

        return rtnVal;
    }

    void OnGUI()
    {
        if (canpickup)  //show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
        }
    }
}
