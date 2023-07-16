using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playersScript : MonoBehaviour
{
    [SerializeField] float[] hiz = { 5, 5 };
    Rigidbody rb;
    public Vector3 fingerDownPos,fingerUpPos;
    public float velocity,yatayV;
    private bool detectSwipe = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 1) * velocity * Time.deltaTime;
    }
    void Start()
    {
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            yatayV = (fingerUpPos.x - fingerDownPos.x) / 50;  //yatay hýz 50 sayýsýna baðlý;
        }
        if(Input.touchCount>0 && (Input.GetTouch(0).phase==TouchPhase.Began||Input.GetTouch(0).phase==TouchPhase.Ended)){ 
            yatayV = 0; 
        }
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
            print("saga");
            transform.Translate(transform.right * yatayV * Time.deltaTime);
        }
        else if (fingerDownPos.x > fingerUpPos.x)
        {
            print("sola");
            transform.Translate(transform.right * yatayV * Time.deltaTime);
        }
        else return;

    }
}
