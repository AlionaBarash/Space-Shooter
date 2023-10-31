using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void RestartGame()
   {
        SceneManager.LoadScene(0);
   }

   public void GoToMainMenu()
   {
        SceneManager.LoadScene(1);
   }
}
