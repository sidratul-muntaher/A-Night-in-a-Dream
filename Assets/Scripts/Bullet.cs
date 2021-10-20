using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] int damagePoint = 20;
    [SerializeField] GameObject healthBar;

    [SerializeField] AudioClip[] audioClips;

    [SerializeField] AudioSource audioSource;

    int x = 0;
    void Start()
    {
        AudioSource.PlayClipAtPoint(audioClips[0],Camera.main.transform.position,  .7f);
        Invoke("Active", 3);
    }

    public void Active()
    {
        gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponentInParent<Health>())
        {
            Debug.Log("Hit");
            x = collision.GetComponentInParent<Health>().GetHealth() - damagePoint;
            collision.GetComponentInParent<Health>().SetHealth(x);
            //todo: remove distroy
            Destroy(Instantiate(game, transform.position, Quaternion.identity), .2f);

        }
        gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
       

    }
}
