using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 30;
    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
