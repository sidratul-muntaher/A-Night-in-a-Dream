using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    

    public void LoadReastart()
    {
        SceneManager.LoadScene(1);
    }
    public void startPage()
    {
        SceneManager.LoadScene(0);
    }

}
