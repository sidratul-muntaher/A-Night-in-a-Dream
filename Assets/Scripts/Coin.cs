using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponentInParent<Player>())
        {
            collision.GetComponentInParent<CoinBank>().SetCoin(collision.GetComponentInParent<CoinBank>().GetCoin() + 1);
            gameObject.SetActive(false);
        }
    }
}
