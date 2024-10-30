using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

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
