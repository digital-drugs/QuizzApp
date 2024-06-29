using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleComponent : MonoBehaviour
{
    [SerializeField] private Vector3 _scale = new(0.6f, 0.6f, 1);
    private void OnEnable()
    {
        OrientationChecker.OnOrientationChange += SetScale;
    }

    private void OnDisable()
    {
        OrientationChecker.OnOrientationChange -=  SetScale;
    }

    private void SetScale(bool value)
    {
        if (value) gameObject.transform.localScale = new(1, 1, 1);
        else gameObject.transform.localScale = _scale;
    }
}
