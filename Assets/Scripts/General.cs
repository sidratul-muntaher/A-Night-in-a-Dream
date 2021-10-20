using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    [SerializeField] float attackRange = .5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform swordTransform;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullets;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Player player;
    [SerializeField] BulletObjectPuller bulletObjectPuller;

    [SerializeField] HealthBar health;
    [SerializeField]
    Collider2D boxCollider2D;
    [SerializeField] AudioClip[] audioClips;
        
    [SerializeField] AudioSource audioSource;

    bool onStart = true;
    bool onHealth = true;


    bool Idiot = false;

    bool closeCombat = false;
    bool swap = true;
    int globalScale = 0;
    Collider2D[] hitEnemys;
    bool swordAttack = false;
    
    // Start is called before the first frame update

    private void Awake()
    {
        health.GetComponent<HealthBar>();

        health.SetHealthBar(GetComponent<Health>().GetHealth());
    }

    void Start()
    {
        StartCoroutine(Idle());

    }

    IEnumerator Idle()
    {
        
        yield return new WaitForSeconds(3);
        animator.SetBool("IsMoving", true);
        Debug.Log("Called");

    }
   

    public void SetSwordAttack(int x)
    {
        if (x == 1)
        {
            foreach (var item in hitEnemys)
            {

                item.GetComponentInParent<Health>().SetHealth(item.GetComponentInParent<Health>().GetHealth() - 50);
                
            }
        }
        else
        {
            swordAttack = false;
        }
        
    }

    public bool IsDead()
    {
        return Idiot;
    }

    public bool GetSwordAttack()
    {
        return swordAttack;
    }

    void Update()
    {

        health.DecreaseHealth(GetComponent<Health>().GetHealth());
        hitEnemys = Physics2D.OverlapCircleAll(swordTransform.position, attackRange, enemyLayer);
        Debug.Log(GetComponent<Health>().GetHealth());

        if (GetComponent<Health>().GetHealth() <= 0)
        {
            Idiot = true;
            gameObject.SetActive(false);
        }

        if (GetComponent<Health>().GetHealth() < 2000 && onHealth)
        {
            audioSource.PlayOneShot(audioClips[1], .8f);
            onHealth = false;
        }


        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Hit");
        }
        
        Vector2 pos = FindObjectOfType<Player>().transform.position - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;




        

        //Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x) + " -> " + angle + " -> " + GLOBAL_SCALE);
            if ((Mathf.Abs(Mathf.Round(angle)) < 190 && Mathf.Abs(Mathf.Round(angle)) > 170) ||
                        ((Mathf.Abs(Mathf.Round(angle)) > 0 && ((Mathf.Abs(Mathf.Round(angle)) < 30)))))
            {
            if (pos.x < 5 && onStart)
            {
                audioSource.PlayOneShot(audioClips[0], .8f);
                onStart = false;
            }
                if (pos.x > 0)
                {
                    //Debug.Log(pos);

                    transform.localScale = (new Vector2(1, 1));
                    if (animator.GetBool("IsMoving"))
                    {

                        rigidbody.velocity = new Vector2(2, rigidbody.velocity.y);
                    }
                }
                else if (pos.x < 0)
                {

                    // Debug.Log(pos);
                    transform.localScale = new Vector2(-1, 1);
                    if (animator.GetBool("IsMoving"))
                    {
                        
                        rigidbody.velocity = new Vector2(-2, rigidbody.velocity.y);
                    }
                }

            
            
            
           
                   if(globalScale == 1 && Mathf.Abs(player.transform.position.x - transform.position.x) <= 1.4f && transform.localScale.x == 1)
                    {
                    //Debug.Log((Mathf.Abs(Mathf.Round(angle))));
                    animator.SetBool("ISFiring", false);
                        //animator.SetBool("IsMoving", false);
                        rigidbody.velocity = new Vector2(0, 0);
                
                        animator.SetBool("IsSpacialAttack", true);
                //foreach (var item in hitEnemys)
                //{

                //   // item.GetComponent<Health>().SetHealth(item.GetComponent<Health>().GetHealth() - 20);
                //    Debug.Log("Hit " + item.name);
                //}
            }
                    else if (globalScale == 0 && Mathf.Abs(player.transform.position.x - transform.position.x) < 3)
                    {
                        animator.SetBool("ISFiring", true);
                        animator.SetBool("IsSpacialAttack", false);
                        rigidbody.velocity = new Vector2(0, 0);
                    }
            //else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3)
            //{
            //    animator.SetBool("ISFiring", true);
            //    animator.SetBool("IsSpacialAttack", false);
            //    rigidbody.velocity = new Vector2(0, 0);
            //}
            //        {
                
            //}
                



            
            
            }
            else
            {
                closeCombat = false;
                animator.SetBool("IsMoving", true);
                animator.SetBool("ISFiring", false);
                animator.SetBool("IsSpacialAttack", false);
                rigidbody.velocity = new Vector2(1 * transform.localScale.x, rigidbody.velocity.y);
                globalScale = Random.Range(0, 2);

            }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.localScale.x == -1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        if (swordTransform == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(swordTransform.position, attackRange);
    }

    public void Firing()
    {
        
        
        Bullet b1  = bulletObjectPuller.BulletSpawning(firePoint);
        b1.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
        b1.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 5, 0);
       // 
    }
}
