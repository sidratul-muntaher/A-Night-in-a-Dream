using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject game;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!game)
        {
            return;
        }
        else if (collision.GetComponentInParent<Player>())
        {
            game.SetActive(true);
        }
    }
}
