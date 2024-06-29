using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    public static event Action OnSelectedEvent;
    public static event Action<bool> OnWrongChoiceSelectedEvent;
    #region Singleton
    public static ButtonHolder Instance { get; private set; }
    

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion
    public AnswerButton[] AnswerButtons;

    private void OnEnable()
    {
        foreach (var button in AnswerButtons)
        {
            button.Button.onClick.AddListener(SwitchInteractionState);
        }
    }

    private void OnDisable()
    {
        foreach (var button in AnswerButtons)
        {
            button.Button.onClick.RemoveListener(SwitchInteractionState);
        }
    }

    public void SwitchInteractionState()
    {
        OnSelectedEvent?.Invoke();
    }

    public void HighlightCorrectOption()
    {
        OnWrongChoiceSelectedEvent?.Invoke(true);
    }
}
