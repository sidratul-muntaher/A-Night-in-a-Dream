using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllStart : MonoBehaviour
{
    [SerializeField] GameObject pannel;
    [SerializeField] GameObject start;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject ab;
    public void About()
    {
        pannel.SetActive(true);
        start.SetActive(false);
        exit.SetActive(false);
        ab.SetActive(false);
    }
    public void PressClose()
    {
        pannel.SetActive(false);
        start.SetActive(true);
        exit.SetActive(true);
        ab.SetActive(true);
    }
}
