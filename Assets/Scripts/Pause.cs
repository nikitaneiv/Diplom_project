using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool gameIsPause = false;
    [SerializeField] private UIController UIController;

    public void ClickPause()
    {
        if (gameIsPause)
        {
            Resume();
        }
        else
        {
            PauseMenu();
        }
    }

    private void Resume()
    {
        UIController.ShowGameScreen();
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    
    private void PauseMenu()
    {
        UIController.ShowPauseScreen();
        Time.timeScale = 0f;
        gameIsPause = true;
    }
}
