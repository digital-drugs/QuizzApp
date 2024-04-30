using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq;

namespace CSVtoGO
{
    public class CSVtoSO:MonoBehaviour
    {
        private static string _path = "/Scripts/CSV Stuff/CSVs/quizz_data.csv", _path1 = "/Scripts/CSV Stuff/CSVs/quizz_data1.csv";
        private static int _numberOfAnswers = 4;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("generated"))
            {
                PlayerPrefs.SetInt("generated", 1);
                GeneratePhrases();
            }

        }

        
        public static void GeneratePhrases()
        {
            static void Generate(string path, string outputPath)
            {

                //Debug.Log("Generated Questions");
                string[] allLines = File.ReadAllLines(Application.dataPath + path);
                allLines = allLines.Skip(1).ToArray();
                //Debug.Log(allLines.Length);

                int z = 0;
                foreach (string line in allLines)
                {
                   // Debug.Log(line);
                    string[] splitData = line.Split(";");
                    //Debug.Log(splitData.Length);
                    QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
                    questionData.Question = splitData[0];
                    Enum.TryParse(splitData[1], out Category category1);
                    questionData.Category = category1;
                    questionData.Answers = new string[4];

                    for (int i = 0; i < _numberOfAnswers; i++)
                    {
                        //Debug.Log("i " + i);
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
                    
                    AssetDatabase.CreateAsset(questionData, $"Assets/Resources/{outputPath}/{questionData.name + z}.asset");
                    z++;
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                print("DONE");
            }
         
            Generate(_path,"Questions0");
            Generate(_path1, "Questions1");
        }
         

    }
}