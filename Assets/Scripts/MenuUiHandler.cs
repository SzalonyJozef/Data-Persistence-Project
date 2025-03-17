using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro.EditorUtilities;
using TMPro;
using UnityEngine.UI;
using System.Drawing.Text;





#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUiHandler : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public static MenuUiHandler Instance;
    public string playerName;
    public int score;
    public TMP_Text highScoresList;
    private HighScoreHandler.HighScoreData[] highScoreDatas;
    public void Awake()
    {

        //if (Instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //Instance = this;
        //DontDestroyOnLoad(gameObject);
        highScoreDatas = HighScoreHandler.LoadHighScoreDataArray();
        Debug.Log(highScoreDatas.Length);
        HighScoreList();
    }

    public void StartNew()
    {
        if (playerNameField.text!="")
        {
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
        else
            Debug.Log("name length: "+playerNameField.text.Length);
         return;
        
    }

    public void SubmitName()
    {
        playerName = playerNameField.text;
        HighScoreHandler.playerName = playerName;

        print(playerName);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else

        Application.Quit(); //original code to quit Unity Player
#endif

        
    }
    private void HighScoreList()
    {
        
        string hsList = "Highscores:\n";
        for (int i = 0; i < highScoreDatas.Length; i++)
        {
            DateTime hsDate = DateTimeOffset.FromUnixTimeSeconds(highScoreDatas[i].highScoreDate).UtcDateTime;
            hsList = hsList + $"{i+1}.\t {highScoreDatas[i].playerName}\t {highScoreDatas[i].score}\t {hsDate.ToString("d")}\n";
        }

        highScoresList.SetText(hsList);

    }



}
