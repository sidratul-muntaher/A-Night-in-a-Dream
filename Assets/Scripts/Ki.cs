using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ki : MonoBehaviour
{
    [SerializeField] float firePoint;
    [SerializeField] GameObject game;
    

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Mathf.Abs(FindObjectOfType<Player>().transform.position.x) - Mathf.Abs(transform.position.x)) > 5)
        {
            gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
if (collision.gameObject.GetComponent<Health>())
        {

            collision.gameObject.GetComponent<Health>().SetHealth(collision.gameObject.GetComponent<Health>().GetHealth() - 40);

        }
        //todo: remove instantiate
        Destroy(Instantiate(game, transform.position, Quaternion.identity), .3f);
       gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;

    }
}
