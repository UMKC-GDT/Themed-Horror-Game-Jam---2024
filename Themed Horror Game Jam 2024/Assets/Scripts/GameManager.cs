using System.Collections;
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
    public bool ending = false;

    void Start()
    {
        cursedObjectsDestroyed = 0;
        isOver = false;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (cursedObjectsDestroyed >= total)
        {
            Debug.Log("you win");
            isOver = true;
            //winPanel.SetActive(true);
            if(ending == false)
                StartCoroutine(Ending());
            ending = true;
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

    IEnumerator Ending()
    {
        float fadeDuration = 1.0f, timePassed = 0;
        while(timePassed < fadeDuration)
        {
            yield return new WaitForSeconds(0.1f);
            RenderSettings.ambientIntensity = timePassed;
            Debug.Log(timePassed / fadeDuration);
            timePassed += 0.1f;
        }
        RenderSettings.ambientIntensity = 1f;
        yield return new WaitForSeconds(2f);
        winPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
