using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

static class HighScoreHandler
{

    public static HighScoreData highScoreData;
    static int highScore;
    public static string playerName;    

    [Serializable]
    public class HighScoreWrapper
    {
        public List<HighScoreData> highScoresList;
    }

    [System.Serializable]

    public class HighScoreData
    {
        public string playerName;
        public int score;
        public long highScoreDate;
    }

    public static HighScoreData LoadHighScore()
    {
        
        string path = Application.persistentDataPath + "highScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
            return highScoreData;

        }
        else { return null; }

    }
    public static void SaveHighScore(string playerName,int score)
    {
        Debug.Log("game saved");
        highScore = LoadHighScore().score;
        if (score > highScore)
        {
            HighScoreData HSData = new HighScoreData();
            HSData.score = score;
            HSData.playerName = playerName;
            HSData.highScoreDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            string json = JsonUtility.ToJson(HSData);
            string path = Application.persistentDataPath + "highScore.json";
            File.WriteAllText(path, json);
            Debug.Log("game saved2");
            Debug.Log("save directory: " + Application.persistentDataPath + "highScore.json");
        }

    }
}
