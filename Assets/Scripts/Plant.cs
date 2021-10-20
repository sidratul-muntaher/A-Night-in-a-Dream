using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Transform[] bodyTransform;
    [SerializeField] GameObject bullet;
    [SerializeField] BulletObjectPuller bulletObjectPuller;

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
            if (player == null)
            {

            }
            else
            {
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

                if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 8 && shotingAngle)
                {
                    walkingInRange = false;
                    animator.SetBool("IsAttacking", true);

                    transform.localScale = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x), 1);
                    if (fire)
                    {
                        StartCoroutine(Shoot());
                    }


                }
                else if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 8)
                {
                    walkingInRange = true;
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsHitten", false);

                }
            }
            yield return new WaitForSeconds(.1f);
        }
        
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.1f);

        Bullet b2 = bulletObjectPuller.BulletSpawning(bodyTransform[0]);
        b2.GetComponent<SpriteRenderer>().enabled = true;
        b2.gameObject.SetActive(true);
        b2.GetComponent<Rigidbody2D>().velocity = new Vector2(-globalScale * 5, 0);

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
    // Update is called once per frame
    private void Update()
    {
        if (GetComponent<Health>().GetHealth() <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
       gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        animator.SetBool("IsHitten", true);
        animator.SetBool("IsAttacking", false);
    }
}
