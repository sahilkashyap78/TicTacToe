using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static Action<Vector3> s_MouseUpPosition;
    private const int LEFT_CLICK = 0;

    void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        Touch currentTouch;
#if UNITY_EDITOR
        currentTouch = new Touch();
        currentTouch.position = Input.mousePosition;
        currentTouch.phase = TouchPhase.Stationary;
        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            currentTouch.phase = TouchPhase.Began;
        }
        else if (Input.GetMouseButtonUp(LEFT_CLICK))
        {
            currentTouch.phase = TouchPhase.Ended;
        }

#elif UNITY_ANDROID
        currentTouch = new Touch();
        if(Input.touchCount > 0)
        {
            currentTouch = Input.GetTouch(0);
        }

#endif
        CheckCurrentTouchPhase(currentTouch);

    }

    private void CheckCurrentTouchPhase(Touch currentTouch)
    {
        if (currentTouch.phase == TouchPhase.Ended)
        {
            s_MouseUpPosition?.Invoke(currentTouch.position);
        }
    }

}
