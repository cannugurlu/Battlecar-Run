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

    public void SetFilled(bool param)
    {
        is_filled = param;
    }
}
