using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersScript : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 fingerDownPos,fingerUpPos;
    public float velocity;
    private bool detectSwipe = false;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            fingerDownPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            fingerUpPos = Input.GetTouch(0).position;
        }

        if (fingerDownPos.x < fingerUpPos.x)
        {
            print("saða");
        }
        else if (fingerDownPos.x > fingerUpPos.x)
        {
            print("sola");
        }
        else return;
    }
}
