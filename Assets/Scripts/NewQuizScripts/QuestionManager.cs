using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ButtonHolder))]
public class QuestionManager : MonoBehaviour
{
    #region Singleton
    public static QuestionManager Instance;
    private string _categoryNumber = "-1";
    public string CategoryNumber { get { return _categoryNumber; } set { _categoryNumber = value; } }
    private void Start()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        foreach (var answerButton in ButtonHolder.Instance.AnswerButtons)
        {
            answerButton.Button.onClick.AddListener(OnAnswerButtonClicked);
        }
        
    }
    #endregion

    [field: SerializeField] public float QuestionPreloadTime { get; set; }
    [SerializeField] private int _correctAnswerChoice = 2;
    public static event Action OnAnswerButtonClickedEvent, SpriteResetEvent;


    public string Correct { get; private set; }
    private bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } set {_isCorrect= value; /*print("set IsCorrect:" + _isCorrect)*/; } }
    public string IsDone { get; set; }
    public bool IsDebug = false;
    private List<List<string>> _questionsList = new();
    public List<List<string>> QuestionsList 
    {
        get => _questionsList; 
        set => _questionsList = value; 
    }
    private int _currentQuestionNumber = 0, _maxNumberLimit = 0;
    
    public void Debug()
    {
        if (IsDebug)
        for (int i = 0; i < QuestionsList.Count; i++)
        {

            for(int j = 0; j < QuestionsList[i].Count; j++)
            {
                print(QuestionsList[i][j]);
            }
        }
    }

    public void SetQuestionAndAnswersValue(int num)
    {
        for (int i = 1;i < 5; i++)
        {
            //print(QuestionsList[num][i]);
            ButtonHolder.Instance.AnswerButtons[i-1].SetAnswerText(QuestionsList[num][i]);

        }
        UIHolder.Instance.QuestionText.text = QuestionsList[num][0];
        Correct = QuestionsList[num][5];
        SetCountValues();
    }

    private IEnumerator OnAnswerButtonClickedTimed()
    {
        ButtonHolder.Instance.HighlightCorrectOption();
        //ButtonHolder.Instance.SwitchInteractionState();

        yield return new WaitForSeconds(QuestionPreloadTime);
        SpriteResetEvent?.Invoke();

        if (_isCorrect && _questionsList[_currentQuestionNumber][6] != "yes")
        {
            Statistics.Instance.UpdateRightAnswers();
            Statistics.Instance.UpdateExp();
            _questionsList[_currentQuestionNumber][6] = "yes";
        }

        _currentQuestionNumber++;
        SetCountValues();

        if(_currentQuestionNumber == _maxNumberLimit + 1) _maxNumberLimit++;

        if (_currentQuestionNumber < _questionsList.Count)
        {
            print("Is Done: " + _questionsList[_currentQuestionNumber][6] + " IsCorrect: " + _isCorrect);

            SetQuestionAndAnswersValue(_currentQuestionNumber);
            
        }
        else
        {
            GameFinished();
        }
    }

    public void OnAnswerButtonClicked()
    {
        StartCoroutine(OnAnswerButtonClickedTimed());
        OnAnswerButtonClickedEvent?.Invoke();
    }

    public void ArrowChoice(int dir)
    {
        if (_currentQuestionNumber + dir >= 0 && _currentQuestionNumber + dir <= _maxNumberLimit) 
        {
            _currentQuestionNumber += dir;
            SetQuestionAndAnswersValue(_currentQuestionNumber);
            SetCountValues();
        }

    }
    public void ResetPointer()
    {
        _maxNumberLimit = 0;
    }

    public void ActivateGamePanel()
    {
        UIHolder.Instance.PanelSelectable.SetActive(UIHolder.Instance.SelectableActive);
        UIHolder.Instance.PanelInput.SetActive(UIHolder.Instance.InputActive);
    }

    public void GameFinished()
    {
         for (int i = 0; i < UIHolder.Instance.UIObjects.Length; i++) 
         {
            UIHolder.Instance.UIObjects[i].SetActive(!UIHolder.Instance.UIObjects[i].activeSelf); 
         } 
        UIHolder.Instance.PanelSelectable.SetActive(false); 
        UIHolder.Instance.PanelInput.SetActive(false); 
        Statistics.Instance.UpdateMatchesPlayed();
        Statistics.Instance.CountPercentage(_questionsList.Count);
    }

    private List<string> RandomizeAnswers(List<string> originalList)
    {
        bool correctAnswerChosen = false;

        List<string> newList = new();

        for (int i = 0; i < ButtonHolder.Instance.AnswerButtons.Length; i++)
        {
            int random = UnityEngine.Random.Range(0, originalList.Count);

            if (random == 0 && !correctAnswerChosen)
            {
                _correctAnswerChoice = i;
                correctAnswerChosen = true;
            }

            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }

        return newList;
    }

    private void SetCountValues()
    {
        UIHolder.Instance.CountText.text = $"{_currentQuestionNumber + 1}/{_questionsList.Count}";   
    }

    public void ResetCount()
    {
        _currentQuestionNumber = 0;
    }
}


