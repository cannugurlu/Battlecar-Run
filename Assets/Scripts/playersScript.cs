using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersScript : MonoBehaviour
{
    Rigidbody rb;
    public float velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rb.velocity = new Vector3(0,0,1)*velocity*Time.deltaTime;
    }
    void Update()
    {
        
    }
}
