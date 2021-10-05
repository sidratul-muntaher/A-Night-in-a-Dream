using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SeceneMAnager : MonoBehaviour
{
    // Start is called before the first frame update
    

    public static void RestartCurrentScene()
    {
        
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public static void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public static void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
