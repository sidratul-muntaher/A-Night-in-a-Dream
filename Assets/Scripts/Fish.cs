using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbody;

    bool start = true;
    bool walkingInRange = false;
    bool shotingAngle = false;
    bool fire = false;
    float globalScale = 1;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {



            if (start)
            {
                rigidbody.velocity = new Vector2(.5f, 0);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rigidbody.velocity = new Vector2(-.5f, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            yield return new WaitForSeconds(.1f);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.localScale.x == 1)
        {
            start = false;
        }
        else if (transform.localScale.x == -1)
        {
            start = true;
        }
    }


}
