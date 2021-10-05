using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMan : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] GameObject o;
    [SerializeField] int damage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClip;
    [SerializeField] Collider2D collider;
    [SerializeField] HealthBar health;


    // Corutins

    Coroutine hits;

    // For Audio Clip

    bool rangeAreaInStart = true;
    bool imMad = true;
    bool hit = true;


    bool isWalking = true;
    bool isAttacking = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        health.GetComponent<HealthBar>();
        health.SetHealthBar(GetComponent<Health>().GetHealth());
    }

    // Update is called once per frame
    void Update()
    {
        health.DecreaseHealth(GetComponent<Health>().GetHealth());

        if (GetComponent<Health>().GetHealth() <= 150 && imMad)
        {
            StopAllCoroutines();

            audioSource.PlayOneShot(audioClip[Random.Range(9, 11)], .9f);//audioSource.PlayOneShot(audioClip[10], .6f);
            imMad = false;
        }

        if (GetComponent<Health>().GetHealth() <= 0)
        {
            die();

        }
        else
        {
            Activity();
        }

        

    }

    private void die()
    {
        AudioSource.PlayClipAtPoint(audioClip[12], Camera.main.transform.position, .8f);
        Destroy(gameObject);
    }

    private void Activity()
    {
        if (player == null)
        {
            animator.SetBool("IsChaging", false);
            return;
        }
        Vector2 pos = FindObjectOfType<Player>().transform.position - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        Vector2 pP = FindObjectOfType<Player>().transform.position;
          
         if (Mathf.Abs(pos.x) <= 8  )
        {

           

            if (pos.x > 0)
            {
                //Debug.Log(pos);
                transform.localScale = (new Vector2(1, 1));
            }
            else if (pos.x < 0)
            {
                // Debug.Log(pos);
                transform.localScale = new Vector2(-1, 1);
            }
            //Debug.Log((Mathf.Abs(Mathf.Round(angle))));
            if ((Mathf.Abs(Mathf.Round(angle)) < 190 && Mathf.Abs(Mathf.Round(angle)) > 170) ||
                    ((Mathf.Abs(Mathf.Round(angle)) > 0 && ((Mathf.Abs(Mathf.Round(angle)) < 15)))))
            {
                if (rangeAreaInStart)
                {
                    audioSource.PlayOneShot(audioClip[0], .8f);
                    rangeAreaInStart = false;
                }

                if (isAttacking )
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsChaging", true);
                }
                

            }
            

        }
        else
        {
           
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsChaging", false);
        }
    }

    public void Charge()
    {
        Vector2 pos = FindObjectOfType<Player>().transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(pos.x, 4.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponentInParent<Player>())
        {
            if (hit)
            {
                //;
                audioSource.PlayOneShot(audioClip[Random.Range(2, 5)], .8f);
                hit = false;
            }
          // hits =  StartCoroutine(HitSound());
            FindObjectOfType<Player>().GetComponent<Health>().SetHealth(player.GetComponent<Health>().GetHealth() - damage);
           // StopCoroutine(hits);
        }
    }

    

    IEnumerator HitSound()
    {
        
        yield return new WaitForSeconds(.2f);
    }

}
