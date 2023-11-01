using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action onPressingEscape;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPressingEscape?.Invoke();
        }
    }

    public void RestartGame()
   {
        SceneManager.LoadScene(0);
   }

   public void GoToMainMenu()
   {
        SceneManager.LoadScene(1);
   }
}
