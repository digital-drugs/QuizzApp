using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SheetProcessor),typeof(CSVLoader),typeof(QuestionManager))]
public class QuizSetup : MonoBehaviour
{
    [SerializeField] private string[] _sheetIds;


    private QuestionManager _questionManager;
    private SheetProcessor _sheetProcessor;
    private CSVLoader _csvLoader;

    private void Awake()
    {
        _questionManager = GetComponent<QuestionManager>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        _csvLoader = GetComponent<CSVLoader>();
        _csvLoader.InitialFileDownloading(_sheetIds);
    }

    public void DownloadTable(int number)
    {
        _csvLoader.DownloadTable(_sheetIds[number], GetExactQuestionList, number);
    }

    private void GetExactQuestionList(string rawCSVText)
    {
        _questionManager.QuestionsList = _sheetProcessor.ProcessData(rawCSVText);
        //foreach (var item in _questionManager.QuestionsList)
        //{
        //    foreach (var item1 in item)
        //    {
        //        print(item1);
        //    }
        //}
        Shuffle.ShuffleList(_questionManager.QuestionsList);
        _questionManager.Debug();
    }

}
