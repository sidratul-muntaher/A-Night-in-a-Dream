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
            start = false;
        }
        else if (slime.transform.localScale.x == -1)
        {
            start = true;
        }
    }

    public bool GetExit()
    {
        
        return start;
    }
}
