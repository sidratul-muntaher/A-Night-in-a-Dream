using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBank : MonoBehaviour
{
    [SerializeField] int coin = 0;
    
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
    public int GetCoin()
    {
        return coin;
    }
}
