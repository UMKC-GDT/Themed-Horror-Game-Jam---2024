using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

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

    public PlayerMovement movement;


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
            pageSet--;
        } else if (Direction == "Right" && pageSet != totalSets){
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
    }

    void OpenPhotobook(){
        photobookMenuUI.SetActive(true);
        movement.isFrozen = true;
        // photosCollected.Add("Photo_1"); // Debug
        // photosCollected.Add("Photo_3"); // Debug
        PageFlip("None");
    }

    void Update()
    {
        if (Input.GetKeyDown(openPhotobookKey)){
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
}

