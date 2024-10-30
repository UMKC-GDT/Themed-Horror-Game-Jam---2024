using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // loads "Level_0 "

    }
    private void Update()
    {
        CloseCredits();
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void CloseCredits()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit(); // Quit Game
    }
}
