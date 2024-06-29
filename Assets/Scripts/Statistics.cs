using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [SerializeField]
    private float _stepMultiplier = 1.5f, _limitBase = 100, _rightAnswerExpAmount = 10;
    [SerializeField] private bool _deleteJsonOnAwake;
    private int _level,_rightAnswers,_matchesPlayed;
    private float _experience, _limit, _percentage;

    private static Statistics _instance;
    public static Statistics Instance => _instance;

    void Awake()
    {
        if (_instance != null) Destroy(_instance);
        _instance = this;

        if (_deleteJsonOnAwake) System.IO.File.Delete(Application.persistentDataPath + "/PlayerStats.json");

        if (!System.IO.File.Exists(Application.persistentDataPath + "/PlayerStats.json"))
        {
            System.IO.File.Create(Application.persistentDataPath + "/PlayerStats.json");
        }
        Initialize();
    }

    private void Initialize()
    {
        JsonReadWrite.ReadFromJson(out _level, out _experience, out _matchesPlayed, out _percentage);
        UIHolder.Instance.LevelText.text = $"Lvl: {_level}";
        UIHolder.Instance.MatchesPlayedText.text = $"Matches Played: {_matchesPlayed}";
        UIHolder.Instance.RightAnswersText.text = $"Right Answers: {_percentage}%";

        _limit = _level * _limitBase * _stepMultiplier;
        UIHolder.Instance.ExperienceSlider.value = _experience / _limit;
    }

    public void UpdateExp()
    {
        _experience += _rightAnswerExpAmount;
        if(_experience > _limit)
        {
            _level += 1;
            UIHolder.Instance.LevelText.text = $"Level: {_level}";
            _limit = _level * _limitBase * _stepMultiplier;
            _experience = 0;
        }
        UIHolder.Instance.ExperienceSlider.value = _experience/_limit;

    }

    public void UpdateMatchesPlayed()
    {
        _matchesPlayed++;

        UIHolder.Instance.MatchesPlayedText.text = $"Matches Played: {_matchesPlayed}";
        //print($"Matches Played: {_level},{_experience}, {_matchesPlayed}, {_percentage}");
        JsonReadWrite.SaveToJson(_level, _experience, _matchesPlayed, _percentage);
    }

    public void UpdateRightAnswers()
    {
        _rightAnswers++;
        print("right answers: "+_rightAnswers);
    }

    private void OnApplicationQuit()
    {
        JsonReadWrite.SaveToJson(_level,_experience,_matchesPlayed,_percentage);
    }

    public void CountPercentage(int total)
    {
        print("total: " + total);
        _percentage = (_percentage +(float)_rightAnswers / total * 100) / 2;
        print($"%:{_percentage}  RightAnswers:{(float)_rightAnswers} Total:{total} result: {_percentage}");
        UIHolder.Instance.RightAnswersText.text = $"Right Answers: {_percentage}%";
    }

    public void ResetRightAnswers()
    {
        _rightAnswers = 0;
    }
}
