using System;
using UnityEngine;

[RequireComponent(typeof(CSVLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    //public event Action<CubesData> OnProcessData;

    [SerializeField] private string _sheetId;
    //[SerializeField] private CubesData _data;

    private CSVLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Start()
    {
        _cvsLoader = GetComponent<CSVLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        DownloadTable();
    }

    private void DownloadTable()
    {
        //_cvsLoader.DownloadTable(_sheetId, OnRawCVSLoaded);
    }

    private void OnRawCVSLoaded(string rawCVSText)
    {
       // _data = _sheetProcessor.ProcessData(rawCVSText);
        //OnProcessData?.Invoke(_data);
    }
}