using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _answerText;  
    [SerializeField] private Image _imageComponent;
    [field:SerializeField]public Button Button { get ; set ; }
    
    private void Awake()
    {
        ButtonHolder.OnWrongChoiceSelectedEvent +=(bool obj)=> HighlightedCorrectOption();
        QuestionManager.SpriteResetEvent +=  ResetSprite;
        ButtonHolder.OnSelectedEvent += SetInteractable;
        Button.onClick.AddListener(CheckCorrectness);
        ResetSprite();
    }

    private void SetInteractable()
    {
        print("AnswerButton Interactable: " + Button.interactable);
        StartCoroutine(TimedInteraction());
    }

    private IEnumerator TimedInteraction()
    {
        Button.interactable = !Button.interactable;
        yield return new WaitForSeconds(QuestionManager.Instance.QuestionPreloadTime);
        Button.interactable = !Button.interactable;
    }

    private void OnDisable()
    {
        QuestionSetup.OnNextQuestion -= ResetSprite;
        ButtonHolder.OnSelectedEvent -= SetInteractable;
        ButtonHolder.OnWrongChoiceSelectedEvent -= (bool obj) => HighlightedCorrectOption();

    }
    public void SetAnswerText(string newText)
    {
        _answerText.text = newText;
    }


    public void OnClick()
    {
        if(!HighlightedCorrectOption())
        {
            _imageComponent.sprite = UIHolder.Instance.Wrong;
            ButtonHolder.Instance.HighlightCorrectOption();
            print(gameObject.name);
        }
    }

    private void ResetSprite()
    {
        _imageComponent.sprite = UIHolder.Instance.Default;
    }

    private bool HighlightedCorrectOption()
    {
        if (_answerText.text == QuestionManager.Instance.Correct)
        {
            _imageComponent.sprite = UIHolder.Instance.Correct;
            return true;
        }
        return false;
    }

    private void CheckCorrectness()
    {
        if (_answerText.text == QuestionManager.Instance.Correct)
        {
            print("Correct = " + QuestionManager.Instance.Correct);
            QuestionManager.Instance.IsCorrect = true;
        }
        else
        {
            QuestionManager.Instance.IsCorrect = false;
            print("Correct = " + QuestionManager.Instance.Correct);
        }
    }
}
