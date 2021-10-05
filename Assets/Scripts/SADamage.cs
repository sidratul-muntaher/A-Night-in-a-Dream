using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SADamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("XXXXXXXXXXXXX");
        if (collision.GetComponentInParent<Player>() && GetComponentInParent<General>().GetSwordAttack())
        {
            collision.GetComponentInParent<Health>().SetHealth(collision.GetComponentInParent<Health>().GetHealth() - 20);
            
        }
    }
}
