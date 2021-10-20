using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPuller : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] int size;
    Bullet[] bullets;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        bullets = new Bullet[size];
        for (int i = 0; i < size; i++)
        {
            bullets[i] = Instantiate(bullet, transform.position, Quaternion.identity);
            bullets[i].GetComponent<SpriteRenderer>().enabled = false;
            bullets[i].gameObject.SetActive(false);
        }
    }

    public Bullet BulletSpawning(Transform x)
    {
        Bullet b = new Bullet();
        bullets[counter].transform.position = x.position;
        b = bullets[counter];

        if (counter == size - 1)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        return b;
    }

    
}
