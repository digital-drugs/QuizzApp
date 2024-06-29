using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReadWrite : MonoBehaviour
{/// <summary>
/// Debug
/// </summary>
    private void Awake()
    {
        ReadFromJson(out int level, out float e, out int mP, out float p);
        print($"lvl:{level} e:{e} matchesPlayed:{mP} %:{p}");
    }
    public static void SaveToJson(int level, float experience, int matchesPlayed, float percentage)
    {
        print($"level:{level} exp:{experience} mPlayed: {matchesPlayed} %:{percentage}");
        PlayerStats stats = new();
        stats.Level = level;
        stats.Experience = experience;
        stats.MatchesPlayed = matchesPlayed;
        stats.Percentage = percentage;

        string json = JsonUtility.ToJson(stats,true);
        File.WriteAllText(Application.persistentDataPath+"/PlayerStats.json", json);
    }

    public static void ReadFromJson(out int level, out float experience,out int matchesPlayed, out float percentage)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/PlayerStats.json");
        PlayerStats stats = JsonUtility.FromJson<PlayerStats>(json);
        level = stats.Level;
        experience = stats.Experience;
        matchesPlayed = stats.MatchesPlayed;
        percentage = stats.Percentage;
    }
}
