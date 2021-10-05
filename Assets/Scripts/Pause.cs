using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject mission;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator  wait()
    {
        
        yield return new WaitForSeconds(2f);
        mission.SetActive(true);
        yield return new WaitForSeconds(4f);
        mission.SetActive(false);
    }

    public void OnPause()
    {
        gameObject.SetActive(false);
        game.SetActive(true);
        Time.timeScale = 0f;
    }

}
