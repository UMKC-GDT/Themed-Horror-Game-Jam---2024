using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime;
//using UnityEngine.UIElements;

public class PhotobookMenu : MonoBehaviour
{
    public KeyCode openPhotobookKey = KeyCode.Tab;
    public KeyCode flipLeft = KeyCode.A;
    public KeyCode flipRight = KeyCode.D;
    public GameObject photoPrefab;
    public GameObject pagePrefab;
    public GameObject photobookMenuUI;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    //public static List<string> photosCollected = new List<string> ();
    private static int pageNum;
    public static int totalPages = 5;
    public static int pagesPerSet = 2;
    public static int totalSets = Mathf.CeilToInt((totalPages + 1) / pagesPerSet);
    public static int pageSet = 1;
    public static int parity;
    public static string pageFlipDirection;
    public static float pageXCoordinate = 179;
    public static float pageWidth = 350;
    public static float pageHeight = 350;
    public static float photoWidth = 250;
    public static float photoHeight = 250;

    public PhotobookGrabber photobookGrabber;
    public PlayerMovement movement;
    public PlayerCam cam;

    private GUIStyle guiStyle;
    private string msg;
    private bool grabbed; //used to determine that the photobook has been grabbed and sets up the ui once in update() accordingly.

    private AudioSource audio;
    private DialogueData objectData;


    GameObject CreatePhoto(GameObject page, int pageNum){
        GameObject photo = Instantiate(photoPrefab, page.transform);

        RectTransform photoRectTransform = photo.GetComponent<RectTransform>();

        photoRectTransform.anchoredPosition = new Vector2(0, 0);
        photoRectTransform.sizeDelta = new Vector2(photoWidth, photoHeight);
        photoRectTransform.name = "Photo_" + (pageNum);

        Image polariod = photo.GetComponent<Image>();
        

        polariod.sprite = Resources.Load<Sprite>("Polaroids/"+ "Photo_" + (pageNum));
       // Debug.Log(polariod.sprite);

        return photo;


    }

    void CreatePageSet(GameObject Photobook, int setNum){

        for (parity = pagesPerSet ; parity > 0; parity--){
            int pageNum = ((setNum * pagesPerSet) - (parity - 1));
            
            if (pageNum > totalPages){
                break;
            }

            string pageName = "Page_" + pageNum;
            string photoName = "Photo_" + ((setNum * pagesPerSet) - (parity - 1));
            GameObject Page;

            if(Photobook.transform.Find(pageName) == null){
                Page = CreatePage(Photobook, pageNum);
    
            } else {
                Page = GameObject.Find("- GAME MANAGER/PhotobookCanvas/Panel/Pages/" + pageName);
            }

            Page.SetActive(true);

            GameObject Photo;

            if (Page.transform.Find(photoName) == null)
            {
                Photo = CreatePhoto(Page, pageNum);
            }
            else
            {
                Photo = GameObject.Find("- GAME MANAGER/PhotobookCanvas/Panel/Pages/" + pageName + "/" + "Photo_" + pageNum);
            }
        }
    }

    void PageFlipButton(){
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        if (buttonName.Contains("Right")){
            pageFlipDirection = "Right";
        } else if (buttonName.Contains("Left")){
            pageFlipDirection = "Left";
        }
    }

    void PageFlip(string Direction){
        GameObject Pages = GameObject.Find("- GAME MANAGER/PhotobookCanvas/Panel/Pages");
        buttonLeft.SetActive(true);
        buttonRight.SetActive(true);
        
        for (int page = 0; page < Pages.transform.childCount ; page++) {
            Pages.transform.GetChild(page).gameObject.SetActive(false);
        }
        
        if (Direction == "Left" && pageSet != 1){
            if (audio != null)
            {
                audio.Play();
            }
            pageSet--;
        } else if (Direction == "Right" && pageSet != totalSets){
            if (audio != null)
            {
                audio.Play();
            }
            pageSet++;
        }

        if (pageSet == 1){
            buttonLeft.SetActive(false);
        } else if (pageSet == totalSets){
            buttonRight.SetActive(false);
        }

        CreatePageSet(Pages, pageSet);
    }

    void ClosePhotobook(){
        photobookMenuUI.SetActive(false);
        movement.isFrozen = false;
        cam.lockMouseMovement = false;
        msg = getGuiMsg(false);
    }

    void OpenPhotobook(){
        photobookMenuUI.SetActive(true);
        movement.isFrozen = true;
        cam.lockMouseMovement = true;
        msg = getGuiMsg(true);
        // photosCollected.Add("Photo_1"); // Debug
        // photosCollected.Add("Photo_3"); // Debug
        PageFlip("None");

        if (objectData != null)
        {
            DialogueManager.instance.RunDialogue(objectData.dialogue); // I added this to run dialogue if this object has it	
        }
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        objectData = GetComponent<DialogueData>();
        grabbed = false;
        setupGui();
    }

    void Update()
    {
        if (photobookGrabber.bookGrabbed && !grabbed){
            setupGui();
            grabbed = true;
        }
        if (Input.GetKeyDown(openPhotobookKey) && photobookGrabber.bookGrabbed){
            if (photobookMenuUI.activeInHierarchy){
                ClosePhotobook();
            } else {
                OpenPhotobook();
            }
        }

        if (photobookMenuUI.activeInHierarchy){

       


            if (Input.GetKeyDown(flipLeft) || pageFlipDirection == "Left"){
                
                PageFlip("Left");
            } else if (Input.GetKeyDown(flipRight) || pageFlipDirection == "Right"){
               
                PageFlip("Right");
            }
            pageFlipDirection = "None";
        }
    }

    private float GetParityXCoordinate(int pageNum){
        if (pageNum % 2 == 1){
            return -pageXCoordinate;
        } else {
            return pageXCoordinate;
        }
    }
    
    private GameObject CreatePage(GameObject Pages, int pageNum){
        float pageParity = GetParityXCoordinate(pageNum);
        GameObject Page = Instantiate(pagePrefab, Pages.transform);

        RectTransform pageRectTransform = Page.GetComponent<RectTransform>();

        pageRectTransform.anchoredPosition = new Vector2(pageParity, 0);
        pageRectTransform.sizeDelta = new Vector2(pageWidth, pageHeight);
        pageRectTransform.name = "Page_" + (pageNum);

        return Page;
    }



    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press Tab to Open the Photo Album";
    }

    private string getGuiMsg(bool isOpen)
    {
        string rtnVal;
        if (isOpen)
            rtnVal = "Press Tab to Close the Photo Album"; 
        else
            rtnVal = "Press Tab to Open the Photo Album";
        return rtnVal;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, Screen.height - 30, 200, 50), msg, guiStyle);
    }
}
