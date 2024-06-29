using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _portraitCanvas,_landscapeCanvas;

    // Update is called once per frame
    void Update()
    {
        if (OrientationChecker.Portrait)
        {
            _portraitCanvas.SetActive(true);
            _landscapeCanvas.SetActive(false);
        }
        else
        {
            _portraitCanvas.SetActive(false);
            _landscapeCanvas.SetActive(true);
        }
    }
}
