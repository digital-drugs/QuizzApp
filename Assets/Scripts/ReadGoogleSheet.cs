using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadGoogleSheet : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ObtainData());
    }


    private IEnumerator ObtainData()
    {
        UnityWebRequest request = UnityWebRequest.Get("");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
        {
            print("Error: " + request.result);
        }
        else
        {
            string json = request.downloadHandler.text;
            print(json);
        }

    }
}
