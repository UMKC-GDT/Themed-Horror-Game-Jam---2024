using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cursedObjectsDestroyed;
    public int total = 6;

    public bool isOver;

    public GameObject winPanel;

    public GameObject losePanel;

    public PlayerMovement player;

    void Start()
    {
        cursedObjectsDestroyed = 0;
        isOver = false;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void Update()
    {
        if(cursedObjectsDestroyed >= total)
        {
            Debug.Log("you win");
            isOver=true;
            winPanel.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (player.lostgame)
        {
            losePanel.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void DestroyedRightObject()
    {
        cursedObjectsDestroyed++;
    }
}
