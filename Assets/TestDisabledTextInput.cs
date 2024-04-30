using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestDisabledTextInput : MonoBehaviour
{
    private Rigidbody _rb;

    void Awake()
    {
       // GetComponent<TextMeshProUGUI>().text = "asdadssdasdasdasda";
       _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
