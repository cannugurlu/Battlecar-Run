using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameFinisher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "car")
        {
            playersScript.minigame = false;
            playersScript.minigameFinished = true;
        }
    }
}
