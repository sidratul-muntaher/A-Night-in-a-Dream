using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] TriggerExit exit;
    //bool start = true;
    bool walkingInRange = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            if (player == null)
            {

            }
            else { 
            Vector2 pos = player.transform.position - transform.position;
            float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

             if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 10)
            {
                walkingInRange = true;
                animator.SetBool("IsRunning", true);

            }
            else if (Mathf.Abs(player.transform.position.x - transform.position.x) > 8)
            {
                walkingInRange = false;
                animator.SetBool("IsRunning", false);
            }
            if (walkingInRange)
            {

                if (exit.GetComponent<TriggerExit>().GetExit())
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

        if (GetComponent<Health>().GetHealth() <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }

    
}
