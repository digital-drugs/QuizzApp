using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreserveAspect : MonoBehaviour
{
    private Image _image;
    private Sprite _sprite;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(FixedTime());
    }

    private IEnumerator FixedTime()
    {

        yield return null;
    }

}
