using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    string myStr;
    public float tickDuration = 0.1f, lastTickTime = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab Pressed");
        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            if((Time.time - lastTickTime) > tickDuration)
            {
                Debug.Log("Tabbing");
                lastTickTime = Time.time;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Debug.Log("Tabbed");
        }
    }

    public void MemberFunction()
    {
        Debug.Log("Hello");
    }
}
