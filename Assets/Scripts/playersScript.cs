using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playersScript : MonoBehaviour
{
    Rigidbody rb;
    public float velocity;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float ClampX = 2.5f;
    private Touch touch;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 1) * velocity * Time.deltaTime;
    }
    
    void Update()
    {
        if (cameraController.cameraFollow)
        {
            float x =transform.position.x;
            float _x =Mathf.Clamp(x,-ClampX, ClampX) ;
            transform.position = new Vector3(_x,transform.position.y,transform.position.z);
            
            if (Input.touchCount >0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed,transform.position.y,transform.position.z);
                }
            }
        }
    }
}
