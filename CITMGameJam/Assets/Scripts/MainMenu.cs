using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    string newGameScene = "SampleScene";

    void Start()
    {

    }
    public void StartNewGame()
    { 
        SceneManager.LoadScene(newGameScene);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();

#endif
    }
}
