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
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.0f);
        timeRemaining--;
        clockText.text = secondsToText();
        if(timeRemaining > 0)
        {
            StartCoroutine(Countdown());
        }
        else 
        {
            onTimeRunsOut.Invoke();
            clockText.text = "Run";
        }
    }

    public void RemoveAMinute()
    {
        Debug.Log(timeRemaining);
        timeRemaining -= 60;
        if(timeRemaining < 0)
        {
            timeRemaining = 0;
        }
        Debug.Log(timeRemaining);
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
}
