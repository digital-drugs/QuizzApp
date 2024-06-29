using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHolder : MonoBehaviour
{
    #region Singleton
    public static UIHolder Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion
    public GameObject[] UIObjects;

    [Header("Answer Buttons - Correct/Wrong/Default")]
    public Sprite Correct;
    public Sprite Wrong;
    public Sprite Default;

    [Header("Text Components")]
    public TextMeshProUGUI QuestionText, CountText;

    [Header("Gameplay Panels ")]
    public GameObject PanelSelectable, PanelInput;

    [Header("Input Panel Elements")]
    public TMP_InputField InputField;
    public TextMeshProUGUI InputQuestionText;
    [Header("Statistics")]
    public TextMeshProUGUI LevelText, MatchesPlayedText, RightAnswersText;
    public Slider ExperienceSlider;
    public string InputRightAnswer { get; set; }

    public bool SelectableActive { get { return PanelSelectable.activeSelf; }set { PanelSelectable.SetActive(value); } } 
    public bool InputActive { get { return PanelInput.activeSelf; } set { PanelSelectable.SetActive(value); } }

}
