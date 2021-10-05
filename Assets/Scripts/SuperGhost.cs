using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGhost : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] AudioClip komokogi;
    [SerializeField] AudioClip hit1;
    [SerializeField] AudioClip hit2;
    [SerializeField] Transform positionx;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject bomb;
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 positions = player.transform.position - transform.position;
        if (positions.x > 0)
        {
            //Debug.Log(pos);
            transform.localScale = (new Vector2(-1, 1));
        }
        else if (positions.x < 0)
        {
            // Debug.Log(pos);
            transform.localScale = new Vector2(1, 1);
        }
        if (start)
        {
            
            transform.position = new Vector2(transform.position.x +.5f * Time.deltaTime, transform.position.y + .5f * Time.deltaTime);
        }
        
    }

    public void Sound()
    {
       source.PlayOneShot(komokogi, 1f);
    }

    public void Attack(float speed)
    {
        
        start = false;
        Vector2 positions = player.transform.position - transform.position;
        float angle = Mathf.Atan2(positions.y, positions.x) * Mathf.Rad2Deg - 90f;
        //ri = angle;
        rigidbody.velocity = new Vector2(positions.x * speed, positions.y * speed);
        //rigidbody.AddForce(player.transform.up * speed, ForceMode2D.Impulse);
        Debug.Log("On");
        Debug.DrawLine(positions, transform.position, Color.green);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody.simulated = false;
        if (collision.collider.GetComponentInParent<Health>())
        {
            
            source.PlayOneShot(hit1, 1f);
            int x = collision.collider.GetComponentInParent<Health>().GetHealth() - 50;
            collision.collider.GetComponentInParent<Health>().SetHealth(x);
        }
        else
        {
            Debug.Log("Enter");
            source.PlayOneShot(hit2, 1f);
        }
       
        Destroy(gameObject, .5f);
        StartCoroutine(boom());
    }

    IEnumerator boom()
    {
        yield return new WaitForSeconds(.4f);
        //todo: remove instantiate
        Destroy(Instantiate(bomb, transform.position, Quaternion.identity), 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
