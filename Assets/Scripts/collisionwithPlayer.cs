using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionwithPlayer : MonoBehaviour
{
    Coroutine start;
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    
    public void OnCollisionStay2D(Collision2D collision)
    {
        
        if (GetComponent<General>() && collision.collider.GetComponentInParent<Player>())
        {
            StartCoroutine(damagingG(collision));
        }
       else if (collision.collider.GetComponentInParent<Player>())
        {

           start =  StartCoroutine(damaging(collision));
        }
        else
        {
            //StopAllCoroutines();
        }
        
    }

    IEnumerator damaging(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.collider.GetComponentInParent<Health>().SetHealth(collision.collider.GetComponentInParent<Health>().GetHealth() - 10);
        
    }

    IEnumerator damagingG(Collision2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.collider.GetComponentInParent<Health>().SetHealth(collision.collider.GetComponentInParent<Health>().GetHealth() - 4);

    }
}
