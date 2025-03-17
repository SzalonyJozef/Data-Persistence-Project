using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System.Drawing;

static class HighScoreHandler
{

    public static HighScoreData highScoreData;
    public static string playerName;
    public static HighScoreData[] highScores = new HighScoreData[10];
    static readonly string path = Application.persistentDataPath + "highScore.json";
   // static int currentScore = MainManager.instance.score;

    [System.Serializable]

    public class HighScoreData
    {
        public string playerName;
        public int score;
        public long highScoreDate;
    }

    public static HighScoreData[] LoadHighScoreDataArray()
    {
        string json = File.ReadAllText(path);
        highScores = JsonHelper.FromJson<HighScoreData>(json);
        return highScores;
    }
    

    public static void SaveHighScoresArray(string playerName, int score)
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            Debug.Log($"Place{i+1}: "+highScores[i].score);
        }
        if (score > highScores[9].score)
        {
            
            Predicate<HighScoreData> predicate = CheckHighScoreRank;
            int first = Array.FindIndex(highScores, predicate);
            Debug.Log("your place: " + (first+1));
            for (int i = 9; i>first; i--)
            {
                Debug.Log($"Place{i + 1}: " + highScores[i].score);
                highScores[i].score = highScores[i-1].score;
            }
            Debug.Log(JsonHelper.ToJson(highScores,true));
            highScores[first].score = score;
            highScores[first].playerName= playerName;
            highScores[first].highScoreDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            Debug.Log(JsonHelper.ToJson(highScores, true));
            File.WriteAllText(path, JsonHelper.ToJson(highScores, true));
        }
        else return;
        
    }
    private static bool CheckHighScoreRank(HighScoreData obj)
    {
        if (obj.score < MainManager.instance.score)
        {
            return true;
        }
        else return false;
       
    }

}

    

