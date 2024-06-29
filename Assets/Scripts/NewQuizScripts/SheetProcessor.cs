using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheetProcessor : MonoBehaviour
{
    private const char CELL_SEPARATOR = ',';

    public List<List<string>> ProcessData(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = cvsRawData.Split(lineEnding);
       
        int rowsNumber = rows.Length;
        List<List<string>> questionList = new(rowsNumber);
        int dataStartRawIndex = 1;
        for(int i = dataStartRawIndex; i < rowsNumber; i++)
        {
            List<string> row = rows[i].Split(CELL_SEPARATOR).ToList();
            //foreach (string item in row)
            //{
            //    print(item);
            //}
            questionList.Add(row);
        }

        return questionList;

    }

    private int ParseInt(string s)
    {
        int result = -1;
        if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't parse int, wrong text");
        }

        return result;
    }

    private float ParseFloat(string s)
    {
        float result = -1;
        if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't pars float,wrong text ");
        }

        return result;
    }

    private char GetPlatformSpecificLineEnd()
    {
        char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
        return lineEnding;
    }
}