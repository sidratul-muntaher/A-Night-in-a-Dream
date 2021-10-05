using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] General X;
    [SerializeField] float speedRunning = 2f;
    [SerializeField] Transform bodyTransform;
    [SerializeField] Transform swordTransform;
    [SerializeField] Ki ki;
    [SerializeField] KiObjectPuller kiObjectPuller;
    [SerializeField] GameObject kiPoint;
    [SerializeField] float attackRange = .5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] HealthBar health;
    [SerializeField] DimondBar dimond;
    [SerializeField] int damagePoint = 20;

    [SerializeField] GameObject WinnerX;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClip;


    Animator animator;
    Rigidbody2D rigidbody;
    CircleCollider2D collider;


    List<Vector2> transforms;
    bool finished = false;
    bool j = true;
    float nearestDistance;
    Coroutine coroutine;
    float globalMove = 1;
    string[] swordAttack = { "ISA", "ISR", "ISU" };
    string sword;
    
    int i = 0;
    void Start()
    {
        Debug.Log("-> " );
        transforms = new List<Vector2>();
        Debug.Log(FindObjectsOfType<AllEnemys>().Length);
        // FindObjectOfType<A
        foreach (AllEnemys item in FindObjectsOfType<AllEnemys>())
        {
            //Debug.Log(item.GetPosition() + " " + item.GetPosition().GetType());
            transforms.Add(item.GetPosition());

        }
        collider = GetComponentInChildren<CircleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dimond.GetComponent<DimondBar>().OnStartDimond(0);
        health.GetComponent<HealthBar>();
        
        health.SetHealthBar(GetComponent<Health>().GetHealth());
    }

    IEnumerator WeekLing()
    {
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(audioClip[4], .8f);
    }

    IEnumerator DeadX()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        SeceneMAnager.GameOver();
    }
    
    void Update() {

        if (finished)
        {
            animator.SetBool("IsDead", true);
            StartCoroutine(DeadX());
        }

        foreach (AllEnemys item in FindObjectsOfType<AllEnemys>())
        {
           var h =  item.GetComponent<Health>().GetHealth();
            if (h <= 0 && !item.GetComponent<Blues>())
            {
                audioSource.PlayDelayed(2);
                StartCoroutine(WeekLing());
                Debug.Log("CMS");
            }

        }


        if (X.IsDead() && GetComponent<CoinBank>().GetCoin() >= 200)
        {
            WinnerX.SetActive(true);
            Time.timeScale = 0f;
        }

        health.DecreaseHealth(GetComponent<Health>().GetHealth());
        dimond.GetComponent<DimondBar>().DimondOnUpdate(GetComponent<CoinBank>().GetCoin());

        if (GetComponent<Health>().GetHealth() <= 400 && j)
        {
            audioSource.PlayOneShot(audioClip[3], .8f);
            j = false;
        }

        if (GetComponent<Health>().GetHealth() <= 0)
        {
            finished = true;
            rigidbody.velocity = new Vector2(0f, 90);
           // StartCoroutine(GameOv());
        }

        if (i == 3)
        {
            i = 0;
        }
         sword = swordAttack[Random.Range(0, swordAttack.Length)];
        Move();
        float y =  Mathf.Sign(CrossPlatformInputManager.GetAxis("Vertical"));
        if (y == -1)
        {
            Debug.Log("A");
            animator.SetBool("IsDown", true);
        }
        else
        {
            animator.SetBool("IsDown", false);
        }
            
            
        Jump();
    }








    private void Jump()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            animator.SetBool("IsJumping", false);
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump") && collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            animator.SetBool("IsJumping", true);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 15);

        }
        
    }

    private void Animte()
    {
        float x = (CrossPlatformInputManager.GetAxis("Horizontal"));
        if (x < 0 || x > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        
       
        animator.SetBool("IsThrow", CrossPlatformInputManager.GetButtonDown("Fire1"));

        
    }

    private void Move()
    {
        float moveX = CrossPlatformInputManager.GetAxis("Horizontal");
        /*if ((Mathf.Abs(moveX) > 0 && Input.GetKey(KeyCode.Space)) && collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            rigidbody.velocity = new Vector2(moveX * 8, 15f);
            bodyTransform.localScale = new Vector2(Mathf.Sign(moveX), 1f);
        }
        else if (Mathf.Abs(moveX) > 0 && !collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            rigidbody.velocity = new Vector2(0f, 0f);
            bodyTransform.localScale = new Vector2(Mathf.Sign(moveX), 1f);
        }*/
        /*if (Input.GetKeyDown(KeyCode.S) && !collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
       {
           Debug.Log("Attack");
           rigidbody.velocity = new Vector2(moveX * speedRunning, rigidbody.velocity.y);
           animator.SetBool(swordAttack[i], true);
           bodyTransform.localScale = new Vector2(Mathf.Sign(moveX), 1f);
       }*/
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(swordTransform.position, attackRange, enemyLayer);
        if (Mathf.Abs(moveX) > 0 && CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            
            rigidbody.velocity = new Vector2(moveX * speedRunning, rigidbody.velocity.y);
            animator.SetBool(swordAttack[i], true);
            audioSource.PlayOneShot(audioClip[i], .6f);
            foreach (var item in hitEnemys)
            {
                
                item.GetComponent<Health>().SetHealth(item.GetComponent<Health>().GetHealth() - damagePoint);
                Debug.Log("Hit " + item.name);
            }
            
        }
        else if (CrossPlatformInputManager.GetButtonDown("Fire2") && !collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            audioSource.PlayOneShot(audioClip[i], .6f);
            Debug.Log("Attack");
            rigidbody.velocity = new Vector2(moveX * speedRunning, rigidbody.velocity.y);
            animator.SetBool(swordAttack[i], true);
            foreach (var item in hitEnemys)
            {
                audioSource.PlayOneShot(audioClip[i], .6f);
                item.GetComponent<Health>().SetHealth(item.GetComponent<Health>().GetHealth() - damagePoint);
                Debug.Log("Hit " + item.name);
            }
            bodyTransform.localScale = new Vector2(Mathf.Sign(moveX), 1f);
        }
        else if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            audioSource.PlayOneShot(audioClip[i], .6f);
            rigidbody.velocity = new Vector2(moveX * speedRunning, rigidbody.velocity.y);
            animator.SetBool(swordAttack[i], true);
            foreach (var item in hitEnemys)
            {
                
                item.GetComponent<Health>().SetHealth(item.GetComponent<Health>().GetHealth() - damagePoint);
                Debug.Log("Hit " + item.name);
            }

        }
        else if (Mathf.Abs(moveX) > 0)
        {
            rigidbody.velocity = new Vector2(moveX * speedRunning, rigidbody.velocity.y);
            animator.SetBool(swordAttack[i], false);
            bodyTransform.localScale = new Vector2(Mathf.Sign(moveX), 1f);
            globalMove = Mathf.Sign(moveX);
        }
        else if (Mathf.Abs(moveX) == 0)
        {
            
            animator.SetBool(swordAttack[i], false);
            i++;
           
        }

        Animte();
        KiBlust(4);
    }
    public void KiBlust(int kiSpeed)
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            coroutine =  StartCoroutine(KiBlustChild(kiSpeed));
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            StopCoroutine(coroutine);
        }

    }

    
    IEnumerator KiBlustChild(int kiSpeed)
    {

        audioSource.PlayOneShot(audioClip[1], .6f);
        Transform kiP = kiPoint.transform;
        Ki k = kiObjectPuller.SpwningKis(kiP);
        k.gameObject.SetActive(true);
            k.transform.localScale = new Vector2(globalMove, 1);
            k.GetComponent<Rigidbody2D>().velocity = new Vector2(globalMove * kiSpeed, 0f);
        yield return new WaitForSeconds(.1f);
    }

    private void OnDrawGizmosSelected()
    {
        if(swordTransform == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(swordTransform.position, attackRange);
    }
}
