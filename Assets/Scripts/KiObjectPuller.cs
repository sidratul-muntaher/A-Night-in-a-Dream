using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiObjectPuller : MonoBehaviour
{
    [SerializeField] Ki ki;
    [SerializeField] int size;
    Ki[] kis;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        kis = new Ki[size];
        for (int i = 0; i < size; i++)
        {
            kis[i] = Instantiate(ki, transform.position, Quaternion.identity);
            kis[i].GetComponent<SpriteRenderer>().enabled = false;
            kis[i].gameObject.SetActive(false);
        }    
    }

    public Ki SpwningKis(Transform pos)
    {
        Ki k = new Ki();
        
        kis[counter].transform.position = pos.position;
        k =  kis[counter];

        if (counter == size-1)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        
        return k;
    }
  
}
