using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPlant : MonoBehaviour
{
    [SerializeField] Player player;

    bool bump = false;
    int count = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            return;
        }
        Vector2 pos = player.transform.position - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        //Debug.Log(pos.x+ " gameObject.name");
        if ((Mathf.Abs(pos.x) <= 2 && (Mathf.Abs(pos.y) <= 2)))
        {
            bump = true;
            Debug.DrawLine(player.transform.position, transform.position, Color.green);
        }
        else
        {
            bump = false;
        }


            if (bump ) 
           
        {
            
            GetComponent<Rigidbody2D>().gravityScale = -.1f;
            if (count == 0)
            {
                if (pos.x > 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, -4f), Random.Range(5.0f, 7f));
                }
                else if (pos.x < 0)
                {
                    // Debug.Log(pos);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(3f, 4f), Random.Range(5.0f, 7f));
                }
                
            }
            else
            {
                //GetComponent<Rigidbody2D>().constraints = 0;
            }
            

        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = .1f;
        }

        }

    
}
