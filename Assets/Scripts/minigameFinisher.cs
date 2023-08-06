using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameFinisher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "car") playersScript.minigame = false;
    }
}
