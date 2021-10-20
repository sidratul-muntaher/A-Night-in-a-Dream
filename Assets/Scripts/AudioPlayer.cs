using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<AudioPlayer>().Length > 1)
        {
            gameObject.SetActive(false);
            
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
