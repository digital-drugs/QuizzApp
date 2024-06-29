using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    AudioSource _audioSource;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("volume")) _musicSlider.value = PlayerPrefs.GetFloat("volume");
        UpdateVolume(_musicSlider.value);
        _musicSlider.onValueChanged.AddListener(UpdateVolume);
    }
    public void UpdateVolume(float value)
    {
        _audioSource.volume = value;
        PlayerPrefs.SetFloat("volume", _musicSlider.value);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(UpdateVolume);
    }
}
