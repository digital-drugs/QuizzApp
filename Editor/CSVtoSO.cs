using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CSVtoSO
{
    private static string _path = "/Editor/CSVs/quizz_data.csv", _path1 = "/Editor/CSVs/quizz_data1.csv";
    private static int _numberOfAnswers = 4;

    [MenuItem("Utilities/Generate Questions")]
    public static void GeneratePhrases()
    {
        static void Generate(string path)
        {

            Debug.Log("Generated Questions");
            string[] allLines = File.ReadAllLines(Application.dataPath + path);

            foreach (string line in allLines)
            {
                Debug.Log(line);
                string[] splitData = line.Split(";");
                Debug.Log(splitData.Length);
                QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
                questionData.Question = splitData[0];
                Enum.TryParse(splitData[1], out Category category1);
                questionData.Category = category1;
                questionData.Answers = new string[4];

                for (int i = 0; i < _numberOfAnswers; i++)
                {
                    Debug.Log("i " + i);
                    questionData.Answers[i] = splitData[2 + i];
                }
                if (questionData.Question.Contains("?"))
                {
                    questionData.name = questionData.Question.Remove(questionData.Question.IndexOf("?"));
                }
                else
                {
                    questionData.name = questionData.Question;
                }
                AssetDatabase.CreateAsset(questionData, $"Assets/Resources/Questions/{questionData.name}.asset");
            }
            AssetDatabase.SaveAssets();
        }


    }
}
