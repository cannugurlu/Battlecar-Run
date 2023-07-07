using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    private bool is_filled = false;

    public bool IsFilled()
    {
        return is_filled;
    }

    private void Update() 
    {
        if (transform.childCount > 0 )
        {
            is_filled = true;
        }
        else
        {
            is_filled = false;
        }
    }
}
