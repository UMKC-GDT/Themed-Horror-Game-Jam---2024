using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class ClockController : MonoBehaviour
{
    public int timeRemaining = 360;
    public UnityEvent onTimeRunsOut;
    public TextMeshProUGUI clockText;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.0f);
        clockText.text = secondsToText();
        if(timeRemaining > 0)
        {
            timeRemaining--;
            StartCoroutine(Countdown());
        }
        else 
        {
            Debug.Log("Time Ran Out");
            onTimeRunsOut.Invoke();
            clockText.text = "Run";
        }
    }

    public string secondsToText()
    {
        string result = "";
        
        if(timeRemaining > 60)
        {
            result += timeRemaining / 60 + ":";
        }
        else
        {
            result += "0:";
        }
        if(timeRemaining % 60 < 10)
        {
            result += "0";
        }
        result += timeRemaining % 60;

        return result;
    }

    public void RemoveAMinute()
    {
        timeRemaining -= 60;
    }
}
