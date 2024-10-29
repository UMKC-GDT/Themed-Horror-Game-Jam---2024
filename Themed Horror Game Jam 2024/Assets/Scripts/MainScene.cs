using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // loads "Level_0 "

    }

    public void QuitGame()
    {
        Application.Quit(); // Quit Game
    }
}
