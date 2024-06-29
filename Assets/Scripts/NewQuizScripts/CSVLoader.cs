using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CSVLoader : MonoBehaviour
{
    private const string URL = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

    public void DownloadTable(string sheetID, Action<string> onSheetLoadedAction,int id)
    {
        string actualUrl = URL.Replace("*",sheetID);
        StartCoroutine(DownloadRawCSVTable(actualUrl,onSheetLoadedAction,id));
    }

    private IEnumerator DownloadRawCSVTable(string actualUrl,Action<string> callback,int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
        {
            yield return request.SendWebRequest();
            string fileName = "data" + id.ToString() + ".csv";
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                print("Error: " + request.result);
               
                string offlinePath = Path.Combine(Application.streamingAssetsPath, fileName);
                callback(File.ReadAllText(offlinePath));
            }
            else if(!File.Exists(Path.Combine(Application.persistentDataPath, fileName)))
            {
                print("Request result: " + request.downloadHandler.text);
                
                File.WriteAllText(path, request.downloadHandler.text, System.Text.Encoding.UTF8);
            }
            else
            {
                callback(File.ReadAllText(path));
            }

        }
        yield return null;
    }

    public void InitialFileDownloading(string[] sheetIDs)
    {      
        StartCoroutine(DownloadFiles(sheetIDs));
    }
    private IEnumerator DownloadFiles(string[] sheetIDs)
    {
        for (int i = 0; i < sheetIDs.Length; i++)
        {
            string actualUrl = URL.Replace("*", sheetIDs[i]);
            //print("actual URL : " + actualUrl);
            using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
            {
                yield return request.SendWebRequest();
                //print("request: " + request.downloadHandler.text);
                CheckAndWriteFiles(request.downloadHandler.text,i);
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    print("Error: " + request.result);
                }
            }
        }
        yield return null;
    }

    private void CheckAndWriteFiles(string content, int i)
    {
        string fileName = "data";
        string path = Path.Combine(Application.persistentDataPath, fileName + i.ToString() + ".csv");
        if(!File.Exists(path)) File.WriteAllText(path, content, System.Text.Encoding.UTF8);
    }
}
