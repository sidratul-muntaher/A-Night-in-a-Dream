
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] HealthBar health;
    [SerializeField] Transform[] bodyTransform;
    [SerializeField] GameObject bullet;
    [SerializeField] BulletObjectPuller bulletObjectPuller;


    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClip;


    bool rangeAreaInStart = true;
    int rebeRebeRealGood = 0;
    bool reduceHealth = true;
    bool die = true;


    bool start = true;
    bool walkingInRange = false;
    bool shotingAngle = false;
    bool fire = false;
    float globalScale = 1;

    Collider2D collider;
    // Start is called before the first frame update

    private void Awake()
    {
        health.GetComponent<HealthBar>();

        health.SetHealthBar(GetComponent<Health>().GetHealth());
    }

    IEnumerator Start()
    {
        while (true)
        {
            if (player == null)
            {

            }
            else
            {
                collider = GetComponent<Collider2D>();
                Vector2 pos = player.transform.position - transform.position;
                float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                

                if ((Mathf.Abs(Mathf.Round(angle)) < 190 && Mathf.Abs(Mathf.Round(angle)) > 170) ||
                    ((Mathf.Abs(Mathf.Round(angle)) > 0 && ((Mathf.Abs(Mathf.Round(angle)) < 15)))))
                {

                    transform.localScale = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 1);
                    globalScale = transform.localScale.x;
                    shotingAngle = true;
                }
                else
                {
                    shotingAngle = false;
                }


                if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 8  && shotingAngle)
                {

                    walkingInRange = false;
                    animator.SetBool("IsShooting", true);
                    animator.SetBool("IsRunning", false);
                    transform.localScale = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 1);
                    if (fire)
                    {
                        if (rangeAreaInStart)
                        {
                            audioSource.PlayOneShot(audioClip[0], 1f);
                            rangeAreaInStart = false;
                        }
                        StartCoroutine(Shoot());
                    }


                }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 10)
                {
                    walkingInRange = true;
                    animator.SetBool("IsShooting", false);
                    animator.SetBool("IsRunning", true);

                }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) > 8)
                {
                    walkingInRange = false;
                    animator.SetBool("IsRunning", false);
                }
                if (walkingInRange)
                {

                    if (start)
                    {
                        rigidbody.velocity = new Vector2(-2, 0);
                        transform.localScale = new Vector2(1, 1);
                    }
                    else
                    {
                        rigidbody.velocity = new Vector2(2, 0);
                        transform.localScale = new Vector2(-1, 1);
                    }

                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        health.DecreaseHealth(GetComponent<Health>().GetHealth());

        if (GetComponent<Health>().GetHealth() < 350 && reduceHealth)
        {
            audioSource.PlayOneShot(audioClip[1], 1f);
            reduceHealth = false;
        }
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            if (die)
            {
                AudioSource.PlayClipAtPoint(audioClip[2], Camera.main.transform.position, .8f);
                die = false;
            }
            Die();
        }
        
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Shoot()
    {

        Bullet b2 =   bulletObjectPuller.BulletSpawning(bodyTransform[0]);
        b2.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
        b2.GetComponent<Rigidbody2D>().velocity = new Vector2(-globalScale * 5, 0);
        yield return new WaitForSeconds(.2f);


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


    //todo change method name
    public void Xxx(int s)
    {
        if (s == 1)
        {
            fire = true;
        }
        else
        {
            fire = false;
        }

    }

    
}
