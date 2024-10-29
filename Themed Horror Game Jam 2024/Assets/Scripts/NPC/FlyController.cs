using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    Quaternion target;

    float lastFocusTime = 0;
    public float focusTime = 1.0f, speed = 10;

    void Update()
    {
        if(Time.time - lastFocusTime > focusTime)
        {
            lastFocusTime = Time.time;
            target = Random.rotation;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);
    }
}
