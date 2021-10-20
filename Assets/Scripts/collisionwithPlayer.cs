using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionwithPlayer : MonoBehaviour
{
    Coroutine start;
   
    
    public void OnCollisionStay2D(Collision2D collision)
    {
        
        if (GetComponent<General>() && collision.collider.GetComponentInParent<Player>())
        {
            StartCoroutine(Damaging(collision));
        }
       else if (collision.collider.GetComponentInParent<Player>())
        {

           start =  StartCoroutine(DamagingX(collision));
        }
        
    }

    IEnumerator DamagingX(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.collider.GetComponentInParent<Health>().SetHealth(collision.collider.GetComponentInParent<Health>().GetHealth() - 10);
        
    }

    IEnumerator Damaging(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.collider.GetComponentInParent<Health>().SetHealth(collision.collider.GetComponentInParent<Health>().GetHealth() - 4);

    }
}
