using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    [SerializeField] Slime slime;
    bool start = true;
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( slime.transform.localScale.x == 1)
        {
            Debug.Log("One");
            start = false;
           // transform.localScale = new Vector2(1, 1);
        }
        else if (slime.transform.localScale.x == -1)
        {
            Debug.Log(-1);
            start = true;
           // transform.localScale = new Vector2(-1, 1);
        }
    }

    public bool GetExit()
    {
        
        return start;
    }
}
