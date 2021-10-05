using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pannel : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject pause;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResume()
    {
        
        pause.SetActive(true);
        game.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        pause.SetActive(true);
        
        SeceneMAnager.RestartCurrentScene();
        Time.timeScale = 1f;
    }

    public void StartMenu()
    {
        
        SeceneMAnager.RestartGame();
        Time.timeScale = 1f;
    }
}
