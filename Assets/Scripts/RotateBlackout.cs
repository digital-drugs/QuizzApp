using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlackout : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if(Screen.width > Screen.height)
        {
            transform.eulerAngles = new(0,0, 90);
        }
        else
        {
            transform.eulerAngles = new(0, 0, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
