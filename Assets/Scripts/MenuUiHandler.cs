using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro.EditorUtilities;
using TMPro;
using UnityEngine.UI;




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

    public void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void SubmitName()
    {
        playerName = playerNameField.text;

        print(playerName);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else

        Application.Quit(); //original code to quit Unity Player
#endif
        MainManager.instance.SaveHighScore();
    }



}
