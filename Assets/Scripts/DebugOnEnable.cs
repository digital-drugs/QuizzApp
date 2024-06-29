using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnEnable : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private void OnEnable()
    {
        print($"{gameObject.name} enabled");
    }
}
