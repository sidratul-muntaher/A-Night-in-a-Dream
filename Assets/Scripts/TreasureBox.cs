using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    [SerializeField] GameObject prize;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Player>())
        {
            GetComponent<Animator>().SetBool("IsOpen", true);
        }
               
    }

    public void BoxOpen()
    {
        prize.SetActive(true);
    }

}
