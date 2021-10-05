using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blues : MonoBehaviour
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

    // Audio
    bool rangeAreaInStart = true;
    int rebeRebeRealGood = 0;
    bool reduceHealth = true;
    bool die = true;


    int startHealth;
    bool start = true;
    bool dead = true; 
    bool walkingInRange = false;
    bool shotingAngle = false;
    bool fire = false;
    float globalScale = 1;
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        health.GetComponent<HealthBar>();

        health.SetHealthBar(GetComponent<Health>().GetHealth());
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        while (dead)
        {
            if (player == null)
            {
                animator.SetBool("IsShooting", false);
                animator.SetBool("IsRunning", false);
            }
            else
            {
                Vector2 pos = player.transform.position - transform.position;
                float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

                //Debug.Log(Mathf.Abs(Mathf.Round(angle)));
                if ((Mathf.Abs(Mathf.Round(angle)) < 190 && Mathf.Abs(Mathf.Round(angle)) > 170) ||
                    ((Mathf.Abs(Mathf.Round(angle)) > -15 && ((Mathf.Abs(Mathf.Round(angle)) < 15)))))
                {
                    

                    transform.localScale = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 1);
                    globalScale = transform.localScale.x;
                    shotingAngle = true;
                }
                else
                {
                    shotingAngle = false;
                }


                if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 5 && shotingAngle)
                {

                    /*if ((startHealth - player.GetComponentInParent<Health>().GetHealth()) > 200 && reduceHealth)
                    {
                        audioSource.PlayOneShot(audioClip[3], .5f);
                        reduceHealth = false;
                    }*/

                    Debug.DrawLine(player.transform.position, transform.position, Color.green);
                    walkingInRange = false;
                    animator.SetBool("IsShooting", true);
                    animator.SetBool("IsRunning", false);
                    transform.localScale = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 1);
                    if (fire)
                    {

                        if (rangeAreaInStart)
                        {
                            startHealth = player.GetComponentInParent<Health>().GetHealth();
                            audioSource.PlayOneShot(audioClip[Random.Range(0, 2)], .8f);
                            rangeAreaInStart = false;
                        }
                        StartCoroutine(Shoot());
                    }


                }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 6)
                {
                    walkingInRange = true;
                    animator.SetBool("IsShooting", false);
                    animator.SetBool("IsRunning", true);

                }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) >= 9)
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
        
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            dead = false;
            if (die)
            {
                audioSource.PlayOneShot(audioClip[2], 1f);
                die = false;
            }
           
            Die();
        }
        // Debug.Log();
    }
    private void Die()
    {
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Dead");
    }
    IEnumerator Shoot()
    {

        if (rebeRebeRealGood == 4)
        {
            audioSource.PlayOneShot(audioClip[Random.Range(4, 6)],  .8f);
        }
        

        Bullet b1 = bulletObjectPuller.BulletSpawning(bodyTransform[0]);
        b1.gameObject.SetActive(true);
        //b1.transform.localScale = new Vector2(globalScale, 1);
        b1.GetComponent<Rigidbody2D>().velocity = new Vector2(-globalScale * 5, 0);

        yield return new WaitForSeconds(.1f);

        
        Bullet b2 = bulletObjectPuller.BulletSpawning(bodyTransform[1]);
        b2.gameObject.SetActive(true);
        //b2.transform.localScale = new Vector2(globalScale, 1);
        b2.GetComponent<Rigidbody2D>().velocity = new Vector2(-globalScale * 5, 0);

        rebeRebeRealGood += 1;


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
