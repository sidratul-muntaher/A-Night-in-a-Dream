using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>())
        {
            collision.GetComponentInParent<Health>().SetHealth(collision.GetComponentInParent<Health>().GetHealth() + 100);
            gameObject.SetActive(false);
        }
    }
}
