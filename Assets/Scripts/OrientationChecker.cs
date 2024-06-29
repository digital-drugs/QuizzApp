using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChecker : MonoBehaviour
{
    public static bool Portrait;
    private bool _currentState;
    static int Width;
    public static event Action<bool> OnOrientationChange;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Width = Screen.width;

        if (Screen.height >= Width) { Portrait = true; }
        else { Portrait = false; }
    }

    void Update()
    {
        if (Width != Screen.width)
        {
            Width = Screen.width;

            if (Screen.height >= Width) { Portrait = true; }
            else { Portrait = false; }
        }

        if (_currentState != Portrait)
        {
            print("Orientation Changed");
            OnOrientationChange?.Invoke(Portrait);
            _currentState = Portrait;
        }
    }
}
