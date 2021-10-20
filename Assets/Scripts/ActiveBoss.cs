using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;
    bool here = false;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.SetActive(true);
        gameObject.SetActive(false);
    }
}
